using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;
using System.Threading;

using Garkanoid.Aux;
using Garkanoid.Base;
using Garkanoid.Interfaces;
using Garkanoid.Miscelaneous;

namespace Garkanoid.Entities
{
    [Serializable]
    public class cBrick : cCollisionBase, iDrawable
    {
        #region Constants

        public const int BRICK_WIDTH = 40;
        public const int BRICK_HEIGHT = 20;

        #endregion

        #region Enumerations

        public enum eColor { Black = 35, Blue = 37, Green = 79, Gray = 78, Pink = 137, Red = 141, White = 164, Yellow = 166 }
        public enum eBrickType { Normal = 1, DoubleHit = 2, Indestructible = 10 }

        #endregion

        private int iLives;
        private eColor m_eColor = eColor.Blue;
        private eBrickType m_eBrickType = eBrickType.Normal;
        private cReward.eRewardType m_eRewardType = cReward.eRewardType.None;

        private static Random oRandom = new Random(DateTime.Now.Millisecond);

        #region Constructors

        public cBrick()
        {
            try
            {
                iLives = 1;

                base.m_oLocation = new Point(0, 0);

                string sPath = ConfigurationManager.AppSettings["pathImages"];
                base.m_oImage = Image.FromFile(sPath + "Brick_Default.png", false);

                oRandom = new Random(DateTime.Now.Millisecond);
            }

            catch (FileNotFoundException ex1) { MessageBox.Show("File not found: " + ex1.FileName, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); throw ex1; }
            catch (Exception ex2) { throw ex2; }
        }

        public cBrick(int X, int Y)
        {
            try
            {
                iLives = 1;

                base.m_oLocation = new Point(X, Y);

                string sPath = ConfigurationManager.AppSettings["pathImages"];
                base.m_oImage = Image.FromFile(sPath + "Brick_Default.png", false);

                oRandom = new Random(DateTime.Now.Millisecond);
            }

            catch (FileNotFoundException ex1) { MessageBox.Show("File not found: " + ex1.FileName, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); throw ex1; }
            catch (Exception ex2) { throw ex2; }
        }

        public cBrick(int X, int Y, eBrickType oBrickType, cReward.eRewardType oRewardType, eColor oColor)
        {
            try
            {
                base.m_oLocation = new Point(X, Y);
                this.m_eColor = oColor;
                this.m_eBrickType = oBrickType;
                this.m_eRewardType = oRewardType;

                switch (oBrickType)
                {
                    case eBrickType.Normal: iLives = 1; break;
                    case eBrickType.DoubleHit: iLives = 2; break;
                    case eBrickType.Indestructible: iLives = 10000; break;
                    default: iLives = 1; break;
                }

                string sPath = Application.StartupPath + "\\" + ConfigurationManager.AppSettings["pathImages"];
                switch (oColor)
                {
                    case eColor.Black: base.m_oImage = Image.FromFile(@sPath + "Brick_Black.png", false); break;
                    case eColor.Blue: base.m_oImage = Image.FromFile(@sPath + "Brick_Blue.png", false); break;
                    case eColor.Green: base.m_oImage = Image.FromFile(@sPath + "Brick_Green.png", false); break;
                    case eColor.Gray: base.m_oImage = Image.FromFile(@sPath + "Brick_Grey.png", false); break;
                    case eColor.Pink: base.m_oImage = Image.FromFile(@sPath + "Brick_Pink.png", false); break;
                    case eColor.Red: base.m_oImage = Image.FromFile(@sPath + "Brick_Red.png", false); break;
                    case eColor.White: base.m_oImage = Image.FromFile(@sPath + "Brick_White.png", false); break;
                    case eColor.Yellow: base.m_oImage = Image.FromFile(@sPath + "Brick_Yellow.png", false); break;
                    default: base.m_oImage = Image.FromFile(@sPath + "Brick_Default.png", false); break;
                }

                oRandom = new Random(DateTime.Now.Millisecond);
            }

            catch (FileNotFoundException ex1) { MessageBox.Show("File not found: " + ex1.FileName, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); throw ex1; }
            catch (Exception ex2) { throw ex2; }
        }

        #endregion

        #region Properties

        public int X
        {
            get { return this.m_oLocation.X; }
            set { if (value > 0 && value < (SCREEN_WIDTH - BRICK_WIDTH)) this.m_oLocation.X = value; }
        }

        public int Y
        {
            get { return this.m_oLocation.Y; }
            set { if (value > 0 && value < (SCREEN_HEIGHT - BRICK_HEIGHT)) this.m_oLocation.Y = value; }
        }

        public eColor Color
        {
            get { return this.m_eColor; }
        }

        public cReward.eRewardType RewardType
        {
            get { return this.m_eRewardType; }
        }

        public eBrickType BrickType
        {
            get { return this.m_eBrickType; }
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

        public void Draw(Graphics oGraphics)
        {
            oGraphics.DrawImage(m_oImage, m_oLocation);
        }

        public Rectangle GetPositionRectangle()
        {
            return new Rectangle(m_oLocation.X, m_oLocation.Y, BRICK_WIDTH, BRICK_HEIGHT);
        }

        public override void CollisionedBy(cCollisionBase oCollision)
        {
            // brick hit by ball
            if (oCollision.GetType() == Type.GetType("Garkanoid.Entities.cBall"))
            {
                iLives--;

                if (iLives == 0) {
                    cBoard.RemoveBrick(this);
                    cGameControl.AddScorePoints((int)this.m_eBrickType);

                    // reward & explosion
                    GenerateReward();
                    GenerateExplosion();

                    // play de 'hit' sound from a separate thread (performance problems)
                    // Thread oThread = new Thread(new ThreadStart(PlaySound));
                    // oThread.Start();
                }
            }

            // brick hit by shot
            if (oCollision.GetType() == Type.GetType("Garkanoid.Entities.cShot"))
            {
                // destroy the brick & shot
                cBoard.RemoveBrick(this);
                cBoard.RemoveShot((cShot)oCollision);
                cGameControl.AddScorePoints((int)this.m_eBrickType);

                // reward & explosion
                GenerateReward();
                GenerateExplosion();

                // play de 'hit' sound from a separate thread (performance problems)
                // Thread oThread = new Thread(new ThreadStart(PlaySound));
                // oThread.Start();
            }
        }

        private void GenerateReward()
        {
            if (this.RewardType != cReward.eRewardType.None)
            {
                cReward oReward = new cReward(this.RewardType, this.Location);
                cBoard.AddReward(oReward);
            }
        }

        private void GenerateExplosion()
        {
            int iRandom = oRandom.Next(1, 4);
            switch (iRandom)
            {
                case 1:
                    cExplodeEffect oExplodeEfect = new cExplodeEffect(this.Location);
                    cParticlesSystem.RegisterEfect(oExplodeEfect);
                    break;
                case 2:
                    cExplodeEffect2 oExplodeEfect2 = new cExplodeEffect2(this.Location);
                    cParticlesSystem.RegisterEfect(oExplodeEfect2);
                    break;
                case 3:
                    cRainEffect oRainEfect = new cRainEffect(this.Location);
                    cParticlesSystem.RegisterEfect(oRainEfect);
                    break;
            }
        }

        private void PlaySound()
        {
            cSound oSound = new cSound();
            oSound.Play();
        }
    }
}
