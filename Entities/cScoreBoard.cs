using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Configuration;

using Garkanoid.Base;
using Garkanoid.Interfaces;
using Garkanoid.Miscelaneous;

namespace Garkanoid.Aux
{
    public class cScoreBoard : iDrawable
    {
        public const int DEFAULT_X = 0;
        public const int DEFAULT_Y = 0;

        public const int SCORE_WIDTH = cCollisionBase.SCREEN_WIDTH;
        public const int SCORE_HEIGHT = 40;

        private Image oImage;
        private Rectangle oRectangle;

        public cScoreBoard()
        {
            try
            {
                string sPath = ConfigurationManager.AppSettings["pathImages"];

                oRectangle = new Rectangle(DEFAULT_X, DEFAULT_Y, SCORE_WIDTH, SCORE_HEIGHT);
                oImage = Image.FromFile(sPath + "Arkanoid.png", false);
            }

            catch (Exception ex) { throw ex; }
        }

        public void Draw(Graphics oGraphics)
        {
            throw new NotSupportedException("Invalid method.");
        }

        public void Draw(Graphics oGraphics, int iLives, int iLevel, int iScore)
        {
            for (int i = 0; i < iLives; i++)
                oGraphics.DrawImage(oImage, new Point(10 + i*35, 10));

            string sLevel = cLanguaje.GetFrmGameLevelString() + " " + iLevel.ToString();
            oGraphics.DrawString(sLevel, new Font("Bookman Old Style", 12, FontStyle.Bold), Brushes.White, new PointF(300, 17));

            string sScore = cLanguaje.GetFrmGameScoreString() + iScore.ToString().PadLeft(5, '0');
            oGraphics.DrawString(sScore, new Font("Bookman Old Style", 12, FontStyle.Bold), Brushes.White, new RectangleF(500, 17, 140, 20));
        }

        public Rectangle GetPositionRectangle()
        {
            throw new NotSupportedException("Invalid method.");
        }
    }
}
