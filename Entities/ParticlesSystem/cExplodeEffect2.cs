using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Configuration;

namespace Garkanoid.Aux
{
    public class cExplodeEffect2 : cEffectBase
    {
        private const int FRAMES_PER_STEP = 4;
        private const int MAX_STEP = 27;

        private const int IMG_WIDTH = 40;
        private const int IMG_HEIGHT = 60;

        private static Image[] arrAnimation;

        public cExplodeEffect2(Point oLocation)
        {
            try
            {
                base.m_oLocation = oLocation;

                if (arrAnimation == null)
                {
                    arrAnimation = new Image[MAX_STEP];
                    string sPath = ConfigurationManager.AppSettings["pathAnimations"];

                    for (int i = 0; i < MAX_STEP; i++)
                        arrAnimation[i] = Image.FromFile(sPath + @"Explode2\" + (i).ToString().PadLeft(3, '0') + ".png");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public override void Draw(Graphics oGraphics)
        {
            oGraphics.DrawImage(arrAnimation[iCurrentStep], base.m_oLocation.X, base.m_oLocation.Y, IMG_WIDTH, IMG_HEIGHT);
            base.iCurrentFrame++;

            if (iCurrentFrame == FRAMES_PER_STEP) {
                base.iCurrentFrame = 0;
                base.iCurrentStep++;
            }

            if (base.iCurrentStep == MAX_STEP)
                cParticlesSystem.RemoveEfect(this);
        }
    }
}
