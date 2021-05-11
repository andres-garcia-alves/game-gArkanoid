using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Garkanoid.Interfaces
{
    public interface iDrawable
    {
        void Draw(Graphics oGraphics);
        Rectangle GetPositionRectangle();
    }
}
