using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using gArkanoid.Aux;
using gArkanoid.Base;
using gArkanoid.Interfaces;
using gArkanoid.Miscelaneous;

namespace gArkanoid.Entities
{
    [Serializable]
    public class Brick : CollisionBase, iDrawable
    {
        #region Constants

        public const int BRICK_WIDTH = 40;
        public const int BRICK_HEIGHT = 20;

        #endregion

        #region Enumerations

        public enum eColor { Black = 35, Blue = 37, Green = 79, Gray = 78, Pink = 137, Red = 141, White = 164, Yellow = 166 }
        public enum eBrickType { Normal = 1, DoubleHit = 2, Indestructible = 10 }

        #endregion

        private int lives;
        private readonly eColor color = eColor.Blue;
        private readonly eBrickType brickType = eBrickType.Normal;
        private readonly Reward.eRewardType rewardType = Reward.eRewardType.None;

        private static Random random = new Random(DateTime.Now.Millisecond);

        #region Constructors

        public Brick()
        {
            try
            {
                this.lives = 1;
                base.location = new Point(0, 0);

                string path = ConfigurationManager.AppSettings["pathImages"];
                base.image = Image.FromFile(path + "Brick_Default.png", false);

                random = new Random(DateTime.Now.Millisecond);
            }

            catch (FileNotFoundException ex1) { MessageBox.Show("File not found: " + ex1.FileName, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); throw ex1; }
            catch (Exception ex2) { throw ex2; }
        }

        public Brick(int x, int y)
        {
            try
            {
                this.lives = 1;
                base.location = new Point(x, y);

                string path = ConfigurationManager.AppSettings["pathImages"];
                base.image = Image.FromFile(path + "Brick_Default.png", false);

                random = new Random(DateTime.Now.Millisecond);
            }

            catch (FileNotFoundException ex1) { MessageBox.Show("File not found: " + ex1.FileName, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); throw ex1; }
            catch (Exception ex2) { throw ex2; }
        }

        public Brick(int x, int y, eBrickType brickType, Reward.eRewardType rewardType, eColor color)
        {
            try
            {
                base.location = new Point(x, y);
                this.color = color;
                this.brickType = brickType;
                this.rewardType = rewardType;

                switch (brickType)
                {
                    case eBrickType.Normal:         this.lives = 1; break;
                    case eBrickType.DoubleHit:      this.lives = 2; break;
                    case eBrickType.Indestructible: this.lives = 10000; break;
                    default:                        this.lives = 1; break;
                }

                string path = Application.StartupPath + "\\" + ConfigurationManager.AppSettings["pathImages"];
                switch (color)
                {
                    case eColor.Black:  base.image = Image.FromFile(path + "Brick_Black.png", false); break;
                    case eColor.Blue:   base.image = Image.FromFile(path + "Brick_Blue.png", false); break;
                    case eColor.Green:  base.image = Image.FromFile(path + "Brick_Green.png", false); break;
                    case eColor.Gray:   base.image = Image.FromFile(path + "Brick_Grey.png", false); break;
                    case eColor.Pink:   base.image = Image.FromFile(path + "Brick_Pink.png", false); break;
                    case eColor.Red:    base.image = Image.FromFile(path + "Brick_Red.png", false); break;
                    case eColor.White:  base.image = Image.FromFile(path + "Brick_White.png", false); break;
                    case eColor.Yellow: base.image = Image.FromFile(path + "Brick_Yellow.png", false); break;
                    default:            base.image = Image.FromFile(path + "Brick_Default.png", false); break;
                }

                random = new Random(DateTime.Now.Millisecond);
            }

            catch (FileNotFoundException ex1) { MessageBox.Show("File not found: " + ex1.FileName, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); throw ex1; }
            catch (Exception ex2) { throw ex2; }
        }

        #endregion

        #region Properties

        public int X
        {
            get { return this.location.X; }
            set { if (value > 0 && value < (SCREEN_WIDTH - BRICK_WIDTH)) this.location.X = value; }
        }

        public int Y
        {
            get { return this.location.Y; }
            set { if (value > 0 && value < (SCREEN_HEIGHT - BRICK_HEIGHT)) this.location.Y = value; }
        }

        public eColor Color
        {
            get { return this.color; }
        }

        public Reward.eRewardType RewardType
        {
            get { return this.rewardType; }
        }

        public eBrickType BrickType
        {
            get { return this.brickType; }
        }

        #endregion

        public override int GetWidth()
        {
            return BRICK_WIDTH;
        }

        public override int GetHeight()
        {
            return BRICK_HEIGHT;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(image, location);
        }

        public Rectangle GetPositionRectangle()
        {
            return new Rectangle(location.X, location.Y, BRICK_WIDTH, BRICK_HEIGHT);
        }

        public override void CollisionedBy(CollisionBase collision)
        {
            // brick hit by ball
            if (collision.GetType() == Type.GetType("gArkanoid.Entities.Ball"))
            {
                lives--;

                if (lives == 0) {
                    Board.RemoveBrick(this);
                    GameControl.AddScorePoints((int)this.brickType);

                    // reward & explosion
                    this.GenerateReward();
                    this.GenerateExplosion();

                    // play de 'hit' sound from a separate thread (performance problems)
                    // Thread thread = new Thread(new ThreadStart(PlaySound));
                    // thread.Start();
                }
            }

            // brick hit by shot
            if (collision.GetType() == Type.GetType("gArkanoid.Entities.Shot"))
            {
                // destroy the brick & shot
                Board.RemoveBrick(this);
                Board.RemoveShot((Shot)collision);
                GameControl.AddScorePoints((int)this.brickType);

                // reward & explosion
                this.GenerateReward();
                this.GenerateExplosion();

                // play de 'hit' sound from a separate thread (performance problems)
                // Thread oThread = new Thread(new ThreadStart(PlaySound));
                // oThread.Start();
            }
        }

        private void GenerateReward()
        {
            if (this.RewardType != Reward.eRewardType.None)
            {
                Reward reward = new Reward(this.RewardType, this.Location);
                Board.AddReward(reward);
            }
        }

        private void GenerateExplosion()
        {
            int num = random.Next(1, 4);
            switch (num)
            {
                case 1:
                    ExplodeEffect explodeEfect = new ExplodeEffect(this.Location);
                    ParticlesSystem.RegisterEfect(explodeEfect);
                    break;
                case 2:
                    ExplodeEffectAlt explodeEfectAlt = new ExplodeEffectAlt(this.Location);
                    ParticlesSystem.RegisterEfect(explodeEfectAlt);
                    break;
                case 3:
                    RainEffect rainEfect = new RainEffect(this.Location);
                    ParticlesSystem.RegisterEfect(rainEfect);
                    break;
            }
        }

        private void PlaySound()
        {
            Sound sound = new Sound();
            sound.Play();
        }
    }
}
