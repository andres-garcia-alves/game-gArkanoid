using System;
using System.Configuration;
using System.Drawing;
using System.Timers;

using gArkanoid.Base;
using gArkanoid.Interfaces;
using gArkanoid.Aux;

namespace gArkanoid.Entities
{
    public class PlayerPad : CollisionBase, iDrawable
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

        private int plusWidth = 0;
        private int widePadTime = 0;
        private int shotingPadTime = 0;

        private Image shotingImage;
        private Timer tmrWidePad, tmrShotingPad;

        public PlayerPad()
        {
            try
            {
                base.location = new Point(DEFAULT_X, DEFAULT_Y);

                string sPath = ConfigurationManager.AppSettings["pathImages"];
                base.image = Image.FromFile(@sPath + "PlayerPad.png", false);
                this.shotingImage = Image.FromFile(@sPath + "PlayerPadShoting.png", false);

                // register for collisions checks
                CollisionsSystem.RegisterItemForCollision(this);
            }

            catch (Exception ex) { throw ex; }
        }

        public int X
        {
            get { return this.location.X; }
            set
            {
                if (value > 0 && value < (SCREEN_WIDTH - GetWidth()))
                    location.X = value;
                else if (value > 0 && value < this.location.X)
                    location.X = value;
                else if (value < (SCREEN_WIDTH - GetWidth()) && value > this.location.X)
                    location.X = value;
            }
        }

        public override int GetWidth()
        {
            return PAD_WIDTH + plusWidth;
        }

        public override int GetHeight()
        {
            return PAD_HEIGHT;
        }

        public void Reset()
        {
            location.X = DEFAULT_X;
            plusWidth = 0;
            widePadTime = 0;
            shotingPadTime = 0;
        }

        public void MoveLeft(int? move)
        {
            if (move == null) move = PAD_MOVEMENT;
            X -= (int)move;
        }

        public void MoveRight(int? move)
        {
            if (move == null) move = PAD_MOVEMENT;
            X += (int)move;
        }

        public void SetPosition(int pos)
        {
            X = pos;
        }

        public void Fire()
        {
            if (shotingPadTime > 0) {

                if (Board.shots.Count == 0) {
                    Shot oShot = new Shot(new Point(this.Location.X + (this.GetWidth() / 2) - (Shot.SHOT_WIDTH / 2), this.Location.Y));
                    Board.AddShot(oShot);
                    CollisionsSystem.RegisterItemForCollision(oShot);
                }
            }
        }

        public void Draw(Graphics graphics)
        {
            if (shotingPadTime <= 0)
                graphics.DrawImage(image, location.X, location.Y, this.GetWidth(), GetHeight());
            else
                graphics.DrawImage(shotingImage, location.X, location.Y, this.GetWidth(), GetHeight());
        }

        public Rectangle GetPositionRectangle() 
        {
            return new Rectangle(location.X - 4, location.Y, PAD_WIDTH + 8, PAD_HEIGHT);
        }

        public override void CollisionedBy(CollisionBase collision)
        {
            // playerpad hit a reward
            if (collision.GetType() == Type.GetType("gArkanoid.Entities.Reward"))
            {
                Board.RemoveReward((Reward)collision);
                GameControl.AddScorePoints((int)((Reward)collision).RewardType);

                switch (((Reward)collision).RewardType)
                {
                    case Reward.eRewardType.None: break;

                    case Reward.eRewardType.WidePad:
                        RewardWidePad();
                        break;

                    case Reward.eRewardType.FirePad:
                        RewardShotingPad();
                        break;

                    case Reward.eRewardType.SlowBall:
                        SlowBallEventHandler oSlowBallEventHandler = SlowBallRewardEvent;

                        if (oSlowBallEventHandler != null) // invoke the delegates
                            oSlowBallEventHandler(this, new EventArgs());
                        break;

                    case Reward.eRewardType.DemolitionBall:
                        DemolitionBallEventHandler oDemolitionBallEventHandler = DemolitionBallRewardEvent;

                        if (oDemolitionBallEventHandler != null) // invoke the delegates
                            oDemolitionBallEventHandler(this, new EventArgs());
                        break;

                    case Reward.eRewardType.DoubleBall:
                        DoubleBallEventHandler oDoubleBallEventHandler = DoubleBallRewardEvent;

                        if (oDoubleBallEventHandler != null) // invoke the delegates
                            oDoubleBallEventHandler(this, new EventArgs());
                        break;

                    case Reward.eRewardType.TripleBall:
                        TripleBallEventHandler oTripleBallEventHandler = TripleBallRewardEvent;

                        if (oTripleBallEventHandler != null) // invoke the delegates
                            oTripleBallEventHandler(this, new EventArgs()); 
                        break;

                    case Reward.eRewardType.WinLevel:
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
            if (widePadTime == 0) {
                tmrWidePad = new Timer(1000);
                tmrWidePad.Elapsed += new ElapsedEventHandler(UndoWidePad);
                tmrWidePad.Start();
            }

            X -= 25;
            plusWidth = 50;
            widePadTime += WIDE_PAD_TIME;
        }

        private void UndoWidePad(object sender, ElapsedEventArgs e)
        {
            if (widePadTime <= 0) {
                tmrWidePad.Stop();
                tmrWidePad.Close();
                X += 25;
                plusWidth = 0;
            }

            widePadTime -= 1000;
        }

        private void RewardShotingPad()
        {
            if (shotingPadTime == 0) {
                tmrShotingPad = new Timer(1000);
                tmrShotingPad.Elapsed += new ElapsedEventHandler(UndoShotingPad);
                tmrShotingPad.Start();
            }

            shotingPadTime += FIRE_PAD_TIME;
        }

        private void UndoShotingPad(object sender, ElapsedEventArgs e)
        {
            if (shotingPadTime <= 0) {
                tmrShotingPad.Stop();
                tmrShotingPad.Close();
            }

            shotingPadTime -= 1000;
        }
    }
}
