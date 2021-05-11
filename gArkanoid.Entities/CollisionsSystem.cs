using System.Collections.Generic;

using gArkanoid.Base;
using gArkanoid.Entities;

namespace gArkanoid.Aux
{
    public class CollisionsSystem
    {
        static List<CollisionBase> collisionObjects = new List<CollisionBase>();

        public static void RegisterItemForCollision(CollisionBase collision)
        {
            collisionObjects.Add(collision);
        }

        public static void RegisterItemForCollision(CollisionBase collision, int position)
        {
            collisionObjects.Insert(position, collision);
        }

        public static void RegisterListForCollision(List<Brick> bricks)
        {
            foreach (CollisionBase brick in bricks)
                collisionObjects.Add(brick);
        }

        public static void RegisterListForCollision(List<Ball> balls)
        {
            foreach (CollisionBase brick in balls)
                collisionObjects.Add(brick);
        }

        public static void RemoveItemForCollision(CollisionBase collision)
        {
            collisionObjects.Remove(collision);
        }

        public void CheckCollisions()
        {
            for (int i = 0; i < collisionObjects.Count; i++)
                for (int j = 0; j < collisionObjects.Count; j++)

                    // exclude collision check agains himself
                    if (i < collisionObjects.Count && collisionObjects[i].CollisionCheck && collisionObjects[j].CollisionCheck &&
                        collisionObjects[i].GetHashCode() != collisionObjects[j].GetHashCode())
                    {
                        if (!(collisionObjects[i].Location.X + collisionObjects[i].GetWidth() < collisionObjects[j].Location.X) &&    // object A at left of object B
                            !(collisionObjects[i].Location.X > collisionObjects[j].Location.X + collisionObjects[j].GetWidth()) &&    // object A at right of object B
                            !(collisionObjects[i].Location.Y + collisionObjects[i].GetHeight() < collisionObjects[j].Location.Y) &&   // object A at top of object B
                            !(collisionObjects[i].Location.Y > collisionObjects[j].Location.Y + collisionObjects[j].GetHeight()))     // object A at bottom of object B
                        {
                            // if reach until here, there is superposition
                            collisionObjects[i].CollisionedBy(collisionObjects[j]);
                        }
                    }
        }
    }
}
