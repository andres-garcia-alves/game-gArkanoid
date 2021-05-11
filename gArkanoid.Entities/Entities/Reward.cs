using System;
using System.Configuration;
using System.Drawing;

using gArkanoid.Base;
using gArkanoid.Interfaces;
using gArkanoid.Aux;

namespace gArkanoid.Entities
{
    public class Reward : CollisionBase, iDrawable
    {
        #region Constants

        public const int REWARD_WIDTH = 40;
        public const int REWARD_HEIGHT = 20;

        private const int FRAMES_PER_STEP = 5;
        private const int CONST_VERTICAL_MOVEMENT = 4;

        #endregion

        public enum eRewardType { None, WidePad, FirePad, SlowBall, DemolitionBall, DoubleBall, TripleBall, WinLevel }

        public eRewardType RewardType;
        private int currentFrame;
        private readonly int verticalMovement;

        public Reward(eRewardType reward, Point position)
        {
            this.RewardType = reward;
            base.location = position;

            Random random = new Random(DateTime.Now.Millisecond);
            this.verticalMovement = CONST_VERTICAL_MOVEMENT + random.Next(4);

            string path = ConfigurationManager.AppSettings["pathImages"];
            switch (reward)
            {
                case eRewardType.WidePad:       base.image = Image.FromFile(path + "Reward_WidePad.png", false); break;
                case eRewardType.FirePad:       base.image = Image.FromFile(path + "Reward_FirePad.png", false); break;
                case eRewardType.SlowBall:      base.image = Image.FromFile(path + "Reward_SlowBall.png", false); break;
                case eRewardType.DemolitionBall: base.image = Image.FromFile(path + "Reward_DemolitionBall.png", false); break;
                case eRewardType.DoubleBall:    base.image = Image.FromFile(path + "Reward_DoubleBall.png", false); break;
                case eRewardType.TripleBall:    base.image = Image.FromFile(path + "Reward_TipleBall.png", false); break;
                case eRewardType.WinLevel:      base.image = Image.FromFile(path + "Reward_WinLevel.png", false); break;
            }

            CollisionsSystem.RegisterItemForCollision(this);
        }

        #region Properties

        public int X
        {
            get { return this.location.X; }
            set {
                if (value > 0 && value < (SCREEN_WIDTH - REWARD_WIDTH))
                    this.location.X = value;
            }
        }

        public int Y
        {
            get { return this.location.Y; }
            set {
                if (value > 0 && value < (SCREEN_HEIGHT - REWARD_HEIGHT))
                    this.location.Y = value;
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
            this.Y += verticalMovement;
        }

        public void Draw(Graphics graphics)
        {
            this.currentFrame++;

            if (this.currentFrame == FRAMES_PER_STEP) {
                this.currentFrame = 0;
                this.Move();
            }

            graphics.DrawImage(base.image, base.location);
        }

        public Rectangle GetPositionRectangle()
        {
            return new Rectangle(location.X - 4, location.Y - 4, REWARD_WIDTH + 8, REWARD_HEIGHT + 8);
        }

        public override void CollisionedBy(CollisionBase collision)
        {
            // reward out of screen
            if (collision.GetType() == Type.GetType("gArkanoid.Entities.OutputLine"))
            {
                Board.RemoveReward(this);
            }
        }
    }
}
