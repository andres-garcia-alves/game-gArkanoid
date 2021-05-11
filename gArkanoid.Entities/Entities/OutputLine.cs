using System.Drawing;

using gArkanoid.Aux;
using gArkanoid.Base;
using gArkanoid.Interfaces;

namespace gArkanoid.Entities
{
    public class OutputLine : CollisionBase, iDrawable
    {
        public const int LINE_WIDTH = SCREEN_WIDTH;
        public const int LINE_HEIGHT = 15;

        public OutputLine()
        {
            base.location = new Point(0, SCREEN_HEIGHT - LINE_HEIGHT);

            // register for collisions checks
            CollisionsSystem.RegisterItemForCollision(this);
        }

        public override int GetWidth()
        {
            return LINE_WIDTH;
        }

        public override int GetHeight()
        {
            return LINE_HEIGHT;
        }

        public override void CollisionedBy(CollisionBase collision)
        {
            // do nothing
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Transparent, GetPositionRectangle());
        }

        public Rectangle GetPositionRectangle()
        {
            return new Rectangle(location.X, location.Y, LINE_WIDTH, LINE_HEIGHT);
        }
    }
}
