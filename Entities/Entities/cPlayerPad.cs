using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Configuration;
using System.Timers;

using Garkanoid.Base;
using Garkanoid.Interfaces;
using Garkanoid.Aux;

namespace Garkanoid.Entities
{
    public class cPlayerPad : cCollisionBase, iDrawable
    {
        #region Constants

        public const int DEFAULT_X = (SCREEN_WIDTH - PAD_WIDTH) / 2;
        public const int DEFAULT_Y = SCREEN_HEIGHT - 50;

        private const int PAD_WIDTH = 100;
        private const int PAD_HEIGHT = 30;
        private const int PAD_MOVEMENT = 5;

        private const int WIDE_PAD_TIME = 12000;
        private const int FIRE_PAD_TIME = 12000;

        #endregion

        #region Public Events

        public delegate void SlowBallEventHandler(object sender, EventArgs e);
        public event SlowBallEventHandler SlowBallRewardEvent;

        public delegate void DemolitionBallEventHandler(object sender, EventArgs e);
        public event DemolitionBallEventHandler DemolitionBallRewardEvent;

        public delegate void DoubleBallEventHandler(object sender, EventArgs e);
        public event DoubleBallEventHandler DoubleBallRewardEvent;

        public delegate void TripleBallEventHandler(object sender, EventArgs e);
        public event TripleBallEventHandler TripleBallRewardEvent;

        public delegate void WinLevelEventHandler(object sender, EventArgs e);
        public event WinLevelEventHandler WinLevelRewardEvent;

        #endregion

        private int iPlusWidth = 0;
        private int iWidePadTime = 0;
        private int iShotingPadTime = 0;

        private Image oShotingImage;
        private Timer tmrWidePad, tmrShotingPad;

        public cPlayerPad()
        {
            try
            {
                base.m_oLocation = new Point(DEFAULT_X, DEFAULT_Y);

                string sPath = ConfigurationManager.AppSettings["pathImages"];
                base.m_oImage = Image.FromFile(@sPath + "PlayerPad.png", false);
                oShotingImage = Image.FromFile(@sPath + "PlayerPadShoting.png", false);

                // register for collisions checks
                cCollisionsSystem.RegisterItemForCollision(this);
            }

            catch (Exception ex) { throw ex; }
        }

        public int X
        {
            get { return this.m_oLocation.X; }
            set
            {
                if (value > 0 && value < (SCREEN_WIDTH - GetWidth()))
                    m_oLocation.X = value;
                else if (value > 0 && value < this.m_oLocation.X)
                    m_oLocation.X = value;
                else if (value < (SCREEN_WIDTH - GetWidth()) && value > this.m_oLocation.X)
                    m_oLocation.X = value;
            }
        }

        public override int GetWidth()
        {
            return PAD_WIDTH + iPlusWidth;
        }

        public override int GetHeight()
        {
            return PAD_HEIGHT;
        }

        public void Reset()
        {
            m_oLocation.X = DEFAULT_X;
            iPlusWidth = 0;
            iWidePadTime = 0;
            iShotingPadTime = 0;
        }

        public void MoveLeft(int? iMove)
        {
            if (iMove == null) iMove = PAD_MOVEMENT;
            X -= (int)iMove;
        }

        public void MoveRight(int? iMove)
        {
            if (iMove == null) iMove = PAD_MOVEMENT;
            X += (int)iMove;
        }

        public void SetPosition(int iPos)
        {
            X = iPos;
        }

        public void Fire()
        {
            if (iShotingPadTime > 0) {

                if (cBoard.lstShots.Count == 0) {
                    cShot oShot = new cShot(new Point(this.Location.X + (this.GetWidth() / 2) - (cShot.SHOT_WIDTH / 2), this.Location.Y));
                    cBoard.AddShot(oShot);
                    cCollisionsSystem.RegisterItemForCollision(oShot);
                }
            }
        }

        public void Draw(Graphics oGraphics)
        {
            if (iShotingPadTime <= 0)
                oGraphics.DrawImage(m_oImage, m_oLocation.X, m_oLocation.Y, this.GetWidth(), GetHeight());
            else
                oGraphics.DrawImage(oShotingImage, m_oLocation.X, m_oLocation.Y, this.GetWidth(), GetHeight());
        }

        public Rectangle GetPositionRectangle() 
        {
            return new Rectangle(m_oLocation.X - 4, m_oLocation.Y, PAD_WIDTH + 8, PAD_HEIGHT);
        }

        public override void CollisionedBy(cCollisionBase oCollision)
        {
            // playerpad hit a reward
            if (oCollision.GetType() == Type.GetType("Garkanoid.Entities.cReward"))
            {
                cBoard.RemoveReward((cReward)oCollision);
                cGameControl.AddScorePoints((int)((cReward)oCollision).RewardType);

                switch (((cReward)oCollision).RewardType)
                {
                    case cReward.eRewardType.None: break;

                    case cReward.eRewardType.WidePad:
                        RewardWidePad();
                        break;

                    case cReward.eRewardType.FirePad:
                        RewardShotingPad();
                        break;

                    case cReward.eRewardType.SlowBall:
                        SlowBallEventHandler oSlowBallEventHandler = SlowBallRewardEvent;

                        if (oSlowBallEventHandler != null) // invoke the delegates
                            oSlowBallEventHandler(this, new EventArgs());
                        break;

                    case cReward.eRewardType.DemolitionBall:
                        DemolitionBallEventHandler oDemolitionBallEventHandler = DemolitionBallRewardEvent;

                        if (oDemolitionBallEventHandler != null) // invoke the delegates
                            oDemolitionBallEventHandler(this, new EventArgs());
                        break;

                    case cReward.eRewardType.DoubleBall:
                        DoubleBallEventHandler oDoubleBallEventHandler = DoubleBallRewardEvent;

                        if (oDoubleBallEventHandler != null) // invoke the delegates
                            oDoubleBallEventHandler(this, new EventArgs());
                        break;

                    case cReward.eRewardType.TripleBall:
                        TripleBallEventHandler oTripleBallEventHandler = TripleBallRewardEvent;

                        if (oTripleBallEventHandler != null) // invoke the delegates
                            oTripleBallEventHandler(this, new EventArgs()); 
                        break;

                    case cReward.eRewardType.WinLevel:
                        WinLevelEventHandler oWinLevelEventHandler = WinLevelRewardEvent;

                        if (oWinLevelEventHandler != null) // invoke the delegates
                            oWinLevelEventHandler(this, new EventArgs()); 
                        break;

                    default: break;
                }
            }
        }

        private void RewardWidePad()
        {
            if (iWidePadTime == 0) {
                tmrWidePad = new Timer(1000);
                tmrWidePad.Elapsed += new System.Timers.ElapsedEventHandler(UndoWidePad);
                tmrWidePad.Start();
            }

            X -= 25;
            iPlusWidth = 50;
            iWidePadTime += WIDE_PAD_TIME;
        }

        private void UndoWidePad(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (iWidePadTime <= 0) {
                tmrWidePad.Stop();
                tmrWidePad.Close();
                X += 25;
                iPlusWidth = 0;
            }

            iWidePadTime -= 1000;
        }

        private void RewardShotingPad()
        {
            if (iShotingPadTime == 0) {
                tmrShotingPad = new Timer(1000);
                tmrShotingPad.Elapsed += new System.Timers.ElapsedEventHandler(UndoShotingPad);
                tmrShotingPad.Start();
            }

            iShotingPadTime += FIRE_PAD_TIME;
        }

        private void UndoShotingPad(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (iShotingPadTime <= 0) {
                tmrShotingPad.Stop();
                tmrShotingPad.Close();
            }

            iShotingPadTime -= 1000;
        }
    }
}
