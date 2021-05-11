using System;
using System.Configuration;
using System.Drawing;

namespace gArkanoid.Aux
{
    public class ExplodeEffect : EffectBase
    {
        private const int FRAMES_PER_STEP = 5;
        private const int MAX_STEP = 16;

        private const int IMG_WIDTH = 32;
        private const int IMG_HEIGHT = 32;

        private static Image[] animation;

        public ExplodeEffect(Point location)
        {
            try
            {
                base.location = location;

                if (animation == null)
                {
                    animation = new Image[MAX_STEP];
                    string path = ConfigurationManager.AppSettings["pathAnimations"];

                    for (int i = 0; i < MAX_STEP; i++)
                        animation[i] = Image.FromFile(path + @"Explode\" + (i).ToString().PadLeft(3, '0') + ".png");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawImage(animation[currentStep], base.location.X, base.location.Y, IMG_WIDTH, IMG_HEIGHT);
            base.currentFrame++;

            if (currentFrame == FRAMES_PER_STEP) {
                base.currentFrame = 0;
                base.currentStep++;
            }

            if (base.currentStep == MAX_STEP)
                ParticlesSystem.RemoveEfect(this);
        }
    }
}
