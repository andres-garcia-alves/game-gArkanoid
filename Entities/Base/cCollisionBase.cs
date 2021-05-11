using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Garkanoid.Base
{
    [Serializable]
    public abstract class cCollisionBase
    {
        public const int SCREEN_WIDTH = 640;
        public const int SCREEN_HEIGHT = 480;

        protected bool m_bCollisionCheck = true;
        protected Image m_oImage;
        protected Point m_oLocation;

        public Point Location
        {
            get { return m_oLocation; }
        }

        public bool CollisionCheck
        {
            get { return m_bCollisionCheck; }
            set { m_bCollisionCheck = value; }
        }

        public abstract int GetWidth();
        public abstract int GetHeight();

        public abstract void CollisionedBy(cCollisionBase oCollision);
    }
}
