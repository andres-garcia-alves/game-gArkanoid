using System;
using System.Collections.Generic;
using System.Text;

using Garkanoid.Base;
using Garkanoid.Entities;

namespace Garkanoid.Aux
{
    public class cCollisionsSystem
    {
        static List<cCollisionBase> lstCollisionObjects = new List<cCollisionBase>();

        public static void RegisterItemForCollision(cCollisionBase oCollision)
        {
            lstCollisionObjects.Add(oCollision);
        }

        public static void RegisterItemForCollision(cCollisionBase oCollision, int iPosition)
        {
            lstCollisionObjects.Insert(iPosition, oCollision);
        }

        public static void RegisterListForCollision(List<cBrick> lstBricks)
        {
            foreach (cCollisionBase o in lstBricks)
                lstCollisionObjects.Add(o);
        }

        public static void RegisterListForCollision(List<cBall> lstBalls)
        {
            foreach (cCollisionBase o in lstBalls)
                lstCollisionObjects.Add(o);
        }

        public static void RemoveItemForCollision(cCollisionBase oCollision)
        {
            lstCollisionObjects.Remove(oCollision);
        }

        public void CheckCollisions()
        {
            for (int i = 0; i < lstCollisionObjects.Count; i++)
                for (int j = 0; j < lstCollisionObjects.Count; j++)

                    // exclude collision check agains himself
                    if (i < lstCollisionObjects.Count && lstCollisionObjects[i].CollisionCheck && lstCollisionObjects[j].CollisionCheck &&
                        lstCollisionObjects[i].GetHashCode() != lstCollisionObjects[j].GetHashCode())
                    {
                        if (!(lstCollisionObjects[i].Location.X + lstCollisionObjects[i].GetWidth() < lstCollisionObjects[j].Location.X) &&    // object A at left of object B
                            !(lstCollisionObjects[i].Location.X > lstCollisionObjects[j].Location.X + lstCollisionObjects[j].GetWidth()) &&    // object A at right of object B
                            !(lstCollisionObjects[i].Location.Y + lstCollisionObjects[i].GetHeight() < lstCollisionObjects[j].Location.Y) &&   // object A at top of object B
                            !(lstCollisionObjects[i].Location.Y > lstCollisionObjects[j].Location.Y + lstCollisionObjects[j].GetHeight()))     // object A at bottom of object B
                        {
                            // if reach until here, there is superposition
                            lstCollisionObjects[i].CollisionedBy(lstCollisionObjects[j]);
                        }
                    }
        }
    }
}
