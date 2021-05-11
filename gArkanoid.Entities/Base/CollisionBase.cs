using System;
using System.Drawing;

namespace gArkanoid.Base
{
    [Serializable]
    public abstract class CollisionBase
    {
        public const int SCREEN_WIDTH = 640;
        public const int SCREEN_HEIGHT = 480;

        protected bool collisionCheck = true;
        protected Image image;
        protected Point location;

        public Point Location
        {
            get { return this.location; }
        }

        public bool CollisionCheck
        {
            get { return this.collisionCheck; }
            set { this.collisionCheck = value; }
        }

        public abstract int GetWidth();
        public abstract int GetHeight();
        public abstract void CollisionedBy(CollisionBase collision);
    }
}
