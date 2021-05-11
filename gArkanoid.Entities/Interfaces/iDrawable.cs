using System.Drawing;

namespace gArkanoid.Interfaces
{
    public interface iDrawable
    {
        void Draw(Graphics graphics);
        Rectangle GetPositionRectangle();
    }
}
