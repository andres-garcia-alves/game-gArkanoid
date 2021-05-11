using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Garkanoid.Interfaces;

namespace Garkanoid.Aux
{
    public class cParticlesSystem : iDrawable
    {
        static List<cEffectBase> lstEfects = new List<cEffectBase>();

        public static void RegisterEfect(cEffectBase oEfect)
        {
            lstEfects.Add(oEfect);
        }

        public static void RemoveEfect(cEffectBase oEfect)
        {
            lstEfects.Remove(oEfect);
        }

        public void ResetEfects()
        {
            lstEfects.Clear();
        }

        public void Draw(Graphics oGraphics)
        {
            for (int i = 0; i < lstEfects.Count; i++)
                lstEfects[i].Draw(oGraphics);
        }

        public Rectangle GetPositionRectangle()
        {
            throw new NotSupportedException("Invalid method.");
        }
    }
}
