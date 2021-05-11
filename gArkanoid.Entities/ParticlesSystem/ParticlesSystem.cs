using System;
using System.Collections.Generic;
using System.Drawing;

using gArkanoid.Interfaces;

namespace gArkanoid.Aux
{
    public class ParticlesSystem : iDrawable
    {
        static readonly List<EffectBase> efects = new List<EffectBase>();

        public static void RegisterEfect(EffectBase efect)
        {
            efects.Add(efect);
        }

        public static void RemoveEfect(EffectBase efect)
        {
            efects.Remove(efect);
        }

        public void ResetEfects()
        {
            efects.Clear();
        }

        public void Draw(Graphics graphics)
        {
            for (int i = 0; i < efects.Count; i++)
                efects[i].Draw(graphics);
        }

        public Rectangle GetPositionRectangle()
        {
            throw new NotSupportedException("Invalid method.");
        }
    }
}
