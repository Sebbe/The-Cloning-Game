/**
 * This code is made by Marius Brendmoe
 */

using Microsoft.Xna.Framework;

namespace TheCloningGame.CollisionSystem
{
    /// <summary>
    /// Defines functions that all collisionsystems developed for this
    /// game must implement.
    /// </summary>
    public interface IManageCollisionsService
    {
        /// <summary>
        /// Register an object that does not move. These objects are never checked
        /// against other static objects for collision, reducing the number of checks per frame.
        /// </summary>
        /// <param name="goc">The gameobject to check for collision</param>
        void RegisterObject(GameObjectCollidable goc);


        /// <summary>
        ///  Removes everything from the array that have collidabless
        /// </summary>
        void RemoveAllObjects();

        void Update(GameTime gameTime);
    }
}