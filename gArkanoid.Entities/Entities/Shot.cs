using System;
using System.Configuration;
using System.Drawing;

using gArkanoid.Aux;
using gArkanoid.Base;
using gArkanoid.Interfaces;

namespace gArkanoid.Entities
{
    public class Shot : CollisionBase, iDrawable
    {
        public const int SHOT_WIDTH = 8;
        public const int SHOT_HEIGHT = 8;

        private const int FRAMES_PER_STEP = 1;
        private const int CONST_VERTICAL_MOVEMENT = 7;

        private int currentFrame;

        public Shot(Point position)
        {
            base.location = position;

            string path = ConfigurationManager.AppSettings["pathImages"];
            base.image = Image.FromFile(path + "Shot.png", false);
        }

        public int X
        {
            get { return this.location.X; }
            set {
                if (value > 0 && value < (SCREEN_WIDTH - SHOT_WIDTH))
                    this.location.X = value;
            }
        }

        public int Y
        {
            get { return this.location.Y; }
            set {
                if (value > 0 && value < (SCREEN_HEIGHT - SHOT_HEIGHT)) {
                    this.location.Y = value;
                } else {
                    CollisionsSystem.RemoveItemForCollision(this);
                    Board.RemoveShot(this);
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

        public void Draw(Graphics graphics)
        {
            currentFrame++;

            if (currentFrame == FRAMES_PER_STEP) {
                currentFrame = 0;
                this.Move();
            }

            graphics.DrawImage(image, location);
        }
        
        public Rectangle GetPositionRectangle()
        {
            throw new NotSupportedException("Invalid method.");
        }

        public override void CollisionedBy(CollisionBase oCollision)
        {
            // no nothnig
        }
    }
}
