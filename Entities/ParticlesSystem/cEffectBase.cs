using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Garkanoid.Aux
{
    public abstract class cEffectBase
    {
        protected int iCurrentFrame;
        protected int iCurrentStep;
        protected Point m_oLocation;

        public cEffectBase()
        {
            iCurrentFrame = 0;
            iCurrentStep = 0;
            m_oLocation = new Point(0, 0);
        }

        public abstract void Draw(Graphics oGraphics);
    }
}
