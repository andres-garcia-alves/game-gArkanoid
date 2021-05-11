using System.Drawing;

namespace gArkanoid.Aux
{
    public abstract class EffectBase
    {
        protected int currentFrame;
        protected int currentStep;
        protected Point location;

        public EffectBase()
        {
            this.currentFrame = 0;
            this.currentStep = 0;
            this.location = new Point(0, 0);
        }

        public abstract void Draw(Graphics graphics);
    }
}
