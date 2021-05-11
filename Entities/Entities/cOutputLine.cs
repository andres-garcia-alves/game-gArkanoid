using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Garkanoid.Aux;
using Garkanoid.Base;
using Garkanoid.Interfaces;

namespace Garkanoid.Entities
{
    public class cOutputLine : cCollisionBase, iDrawable
    {
        public const int LINE_WIDTH = SCREEN_WIDTH;
        public const int LINE_HEIGHT = 15;

        public cOutputLine()
        {
            base.m_oLocation = new Point(0, SCREEN_HEIGHT - LINE_HEIGHT);

            // register for collisions checks
            cCollisionsSystem.RegisterItemForCollision(this);
        }

        public override int GetWidth()
        {
            return LINE_WIDTH;
        }

        public override int GetHeight()
        {
            return LINE_HEIGHT;
        }

        public override void CollisionedBy(cCollisionBase oCollision)
        {
            // do nothing
        }

        public void Draw(Graphics oGraphics)
        {
            oGraphics.FillRectangle(Brushes.Transparent, GetPositionRectangle());
        }

        public Rectangle GetPositionRectangle()
        {
            return new Rectangle(m_oLocation.X, m_oLocation.Y, LINE_WIDTH, LINE_HEIGHT);
        }
    }
}
