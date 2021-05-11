using System;
using System.Configuration;
using System.Drawing;

using gArkanoid.Base;
using gArkanoid.Interfaces;
using gArkanoid.Miscelaneous;

namespace gArkanoid.Aux
{
    public class ScoreBoard : iDrawable
    {
        public const int DEFAULT_X = 0;
        public const int DEFAULT_Y = 0;

        public const int SCORE_WIDTH = CollisionBase.SCREEN_WIDTH;
        public const int SCORE_HEIGHT = 40;

        private readonly Image image;
        private Rectangle rectangle;

        public ScoreBoard()
        {
            try
            {
                string path = ConfigurationManager.AppSettings["pathImages"];

                this.rectangle = new Rectangle(DEFAULT_X, DEFAULT_Y, SCORE_WIDTH, SCORE_HEIGHT);
                this.image = Image.FromFile(path + "Arkanoid.png", false);
            }

            catch (Exception ex) { throw ex; }
        }

        public void Draw(Graphics graphics)
        {
            throw new NotSupportedException("Invalid method.");
        }

        public void Draw(Graphics graphics, int lives, int level, int score)
        {
            for (int i = 0; i < lives; i++)
                graphics.DrawImage(image, new Point(10 + i*35, 10));

            string sLevel = Languaje.GetFrmGameLevelString() + " " + level.ToString();
            graphics.DrawString(sLevel, new Font("Bookman Old Style", 12, FontStyle.Bold), Brushes.White, new PointF(300, 17));

            string sScore = Languaje.GetFrmGameScoreString() + score.ToString().PadLeft(5, '0');
            graphics.DrawString(sScore, new Font("Bookman Old Style", 12, FontStyle.Bold), Brushes.White, new RectangleF(500, 17, 140, 20));
        }

        public Rectangle GetPositionRectangle()
        {
            throw new NotSupportedException("Invalid method.");
        }
    }
}
