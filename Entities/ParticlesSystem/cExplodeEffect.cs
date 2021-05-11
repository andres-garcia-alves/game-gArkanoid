using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Configuration;

using Garkanoid.Miscelaneous;

namespace Garkanoid.Aux
{
    public class cExplodeEffect : cEffectBase
    {
        private const int FRAMES_PER_STEP = 5;
        private const int MAX_STEP = 16;

        private const int IMG_WIDTH = 32;
        private const int IMG_HEIGHT = 32;

        private static Image[] arrAnimation;

        public cExplodeEffect(Point oLocation)
        {
            try
            {
                base.m_oLocation = oLocation;

                if (arrAnimation == null)
                {
                    arrAnimation = new Image[MAX_STEP];
                    string sPath = ConfigurationManager.AppSettings["pathAnimations"];

                    for (int i = 0; i < MAX_STEP; i++)
                        arrAnimation[i] = Image.FromFile(sPath + @"Explode\" + (i).ToString().PadLeft(3, '0') + ".png");
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
