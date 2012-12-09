/**
 * This code is made by Marius Brendmoe
 */

using Microsoft.Xna.Framework;

namespace TheCloningGame.CollisionSystem
{
    public class GameObjectCollidable : GameObject
    {
        /// <summary>
        /// Set this flag to have the collision system (and any other systems for that matter)
        /// remove the object from it's lists during it's maintenance phase.
        /// </summary>
        public bool DeleteFlag { get; protected set; }

        /// <summary>
        /// The collisionbox is used by the collisionsystem to determine if a collision
        /// has occured. 
        /// </summary>
        public Rectangle CollisionBox
        {
            get
            {
                return collisionBox;
            }
            protected set
            {
                collisionBox = value;
                //position.X = collisionBox.X;
                //position.Y = collisionBox.Y;
            }
        }
        /// <summary>
        /// Sets the position of this gameobject.
        /// If the position is set using this parameter,
        /// the collisionbox is also updated.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                collisionBox.X = (int) value.X;
                collisionBox.Y = (int) value.Y;
                position = value;
            }
        }


        protected Rectangle collisionBox;
        protected Vector2 position;

        /// <summary>
        /// Initializes the gameobject, sets it's position and creates a collision box
        /// with width and height as 0.
        /// </summary>
        /// <param name="position">The position of the object</param>
        public GameObjectCollidable(Vector2 position)
        {
            //CollisionBox = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            this.position = position;
        }

        /// <summary>
        /// This function does not implement any collision data, as it wouldn't know
        /// what to do if it crashed with something!
        /// </summary>
        /// <param name="goc">The object the called object was hit by</param>
        public virtual void Collision(GameObjectCollidable goc)
        {
           
        }


    }
}
