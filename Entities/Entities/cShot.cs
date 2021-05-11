using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Configuration;

using Garkanoid.Base;
using Garkanoid.Interfaces;
using Garkanoid.Aux;

namespace Garkanoid.Entities
{
    public class cShot : cCollisionBase, iDrawable
    {
        public const int SHOT_WIDTH = 8;
        public const int SHOT_HEIGHT = 8;

        private const int FRAMES_PER_STEP = 1;
        private const int CONST_VERTICAL_MOVEMENT = 7;

        private int iCurrentFrame;

        public cShot(Point oPosition)
        {
            base.m_oLocation = oPosition;

            string sPath = ConfigurationManager.AppSettings["pathImages"];
            base.m_oImage = Image.FromFile(@sPath + "Shot.png", false);
        }

        public int X
        {
            get { return this.m_oLocation.X; }
            set {
                if (value > 0 && value < (SCREEN_WIDTH - SHOT_WIDTH))
                    this.m_oLocation.X = value;
            }
        }

        public int Y
        {
            get { return this.m_oLocation.Y; }
            set {
                if (value > 0 && value < (SCREEN_HEIGHT - SHOT_HEIGHT)) {
                    this.m_oLocation.Y = value;
                } else {
                    cCollisionsSystem.RemoveItemForCollision(this);
                    cBoard.RemoveShot(this);
                }
            }
        }

        public override int GetWidth()
        {
            return SHOT_WIDTH;
        }

        public override int GetHeight()
        {
            return SHOT_HEIGHT;
        }

        public void Move()
        {
            this.Y -= CONST_VERTICAL_MOVEMENT;
        }

        public void Draw(Graphics oGraphics)
        {
            iCurrentFrame++;

            if (iCurrentFrame == FRAMES_PER_STEP) {
                iCurrentFrame = 0;
                Move();
            }

            oGraphics.DrawImage(m_oImage, m_oLocation);
        }
        
        public Rectangle GetPositionRectangle()
        {
            throw new NotSupportedException("Invalid method.");
        }

        public override void CollisionedBy(cCollisionBase oCollision)
        {
            // no nothnig
        }
    }
}
