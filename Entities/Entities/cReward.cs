using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Configuration;

using Garkanoid.Base;
using Garkanoid.Interfaces;
using Garkanoid.Aux;

namespace Garkanoid.Entities
{
    public class cReward : cCollisionBase, iDrawable
    {
        #region Constants

        public const int REWARD_WIDTH = 40;
        public const int REWARD_HEIGHT = 20;

        private const int FRAMES_PER_STEP = 5;
        private const int CONST_VERTICAL_MOVEMENT = 4;

        #endregion

        public enum eRewardType { None, WidePad, FirePad, SlowBall, DemolitionBall, DoubleBall, TripleBall, WinLevel }

        public eRewardType RewardType;
        private int iCurrentFrame;
        private int iVerticalMovement;

        public cReward(eRewardType eReward, Point oPosition)
        {
            RewardType = eReward;
            base.m_oLocation = oPosition;

            Random oRandom = new Random(DateTime.Now.Millisecond);
            iVerticalMovement = CONST_VERTICAL_MOVEMENT + oRandom.Next(4);

            string sPath = ConfigurationManager.AppSettings["pathImages"];
            switch (eReward)
            {
                case eRewardType.WidePad:       base.m_oImage = Image.FromFile(@sPath + "Reward_WidePad.png", false); break;
                case eRewardType.FirePad:       base.m_oImage = Image.FromFile(@sPath + "Reward_FirePad.png", false); break;
                case eRewardType.SlowBall:      base.m_oImage = Image.FromFile(@sPath + "Reward_SlowBall.png", false); break;
                case eRewardType.DemolitionBall: base.m_oImage = Image.FromFile(@sPath + "Reward_DemolitionBall.png", false); break;
                case eRewardType.DoubleBall:    base.m_oImage = Image.FromFile(@sPath + "Reward_DoubleBall.png", false); break;
                case eRewardType.TripleBall:    base.m_oImage = Image.FromFile(@sPath + "Reward_TipleBall.png", false); break;
                case eRewardType.WinLevel:      base.m_oImage = Image.FromFile(@sPath + "Reward_WinLevel.png", false); break;
            }

            cCollisionsSystem.RegisterItemForCollision(this);
        }

        #region Properties

        public int X
        {
            get { return this.m_oLocation.X; }
            set {
                if (value > 0 && value < (SCREEN_WIDTH - REWARD_WIDTH))
                    this.m_oLocation.X = value;
            }
        }

        public int Y
        {
            get { return this.m_oLocation.Y; }
            set {
                if (value > 0 && value < (SCREEN_HEIGHT - REWARD_HEIGHT))
                    this.m_oLocation.Y = value;
            }
        }

        #endregion

        public override int GetWidth()
        {
            return REWARD_WIDTH;
        }

        public override int GetHeight()
        {
            return REWARD_HEIGHT;
        }

        public void Move()
        {
            this.Y += iVerticalMovement;
        }

        public void Draw(Graphics oGraphics)
        {
            iCurrentFrame++;

            if (iCurrentFrame == FRAMES_PER_STEP) {
                iCurrentFrame = 0;
                Move();
            }

            oGraphics.DrawImage(m_oImage, m_oLocation);
        }

        public Rectangle GetPositionRectangle()
        {
            return new Rectangle(m_oLocation.X - 4, m_oLocation.Y - 4, REWARD_WIDTH + 8, REWARD_HEIGHT + 8);
        }

        public override void CollisionedBy(cCollisionBase oCollision)
        {
            // reward out of screen
            if (oCollision.GetType() == Type.GetType("Garkanoid.Entities.cOutputLine"))
            {
                cBoard.RemoveReward(this);
            }
        }
    }
}
