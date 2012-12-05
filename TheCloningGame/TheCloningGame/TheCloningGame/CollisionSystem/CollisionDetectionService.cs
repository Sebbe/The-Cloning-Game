/**
 * This code is made by Marius Brendmoe
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TheCloningGame.CollisionSystem
{
    /// <summary>
    /// An instance of this gamecomponent can be used to manage collisions between 2D objects in a gameworld. 
    /// The collisionDetectionService implements IManageCollisionsService interface to make it possible to 
    /// create other collisionsystems and still have the game function as intended, because the interface function
    /// is still the same, meaning that all objects making use of the interface will notice no change in the system.
    /// </summary>
    public class CollisionDetectionService : Microsoft.Xna.Framework.GameComponent, IManageCollisionsService
    {

        protected List<GameObjectCollidable> collidables;

        public CollisionDetectionService(Game game)
            : base(game)
        {
            collidables = new List<GameObjectCollidable>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Performs maintenance of the collision system as well as a collision check on all objects
        /// currently registered.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            //Maintenance pass
            for (int i = 0; i < collidables.Count; i++)
            {
                if (collidables[i].DeleteFlag)
                {
                    collidables.Remove(collidables[i]);
                    i--;
                    continue;
                }
            }
            //Match all objects against each other and check for collisions
            for (int i = 0; i < collidables.Count; i++)
            {
                for (int j = 0; j < collidables.Count; j++)
                {   //Don't match an object with itself
                    if(i == j)
                    {
                        continue;
                    }
                    if (collidables[i].CollisionBox.Intersects(collidables[j].CollisionBox))
                    {
                        collidables[i].Collision(collidables[j]);
                    }
                }
            }
        }

        /// <summary>
        /// Register an object for collision detection.
        /// </summary>
        /// <param name="goc">The object to register..</param>
        public void RegisterObject(GameObjectCollidable goc)
        {
            collidables.Add(goc);
        }

        /// <summary>
        /// Removes every objects in the collidables
        /// </summary>
        public void RemoveAllObjects()
        {
            collidables.Clear();
        }
    }
}