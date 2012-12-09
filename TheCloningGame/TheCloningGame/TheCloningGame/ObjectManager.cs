using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheCloningGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ObjectManager : DrawableGameComponent
    {

        protected List<GameObject> objects;

        protected Queue<GameObject> objectsToRemove;


        protected SpriteBatch spriteBatch;

        public ObjectManager(Game game)
            : base(game)
        {
            objects = new List<GameObject>();
            objectsToRemove = new Queue<GameObject>();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //First remove all the objects queued for removal.
            while (objectsToRemove.Count > 0)
            {
                objects.Remove(objectsToRemove.Dequeue());
            }
            //Then update all the objects.
            for (int i = 0; i < objects.Count; i++)
            {
                GameObject go = objects[i];
                go.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //Draw all the objects in the objectlist);
            foreach (GameObject go in objects)
            {
                if(go.IsActive())
                    go.Draw(gameTime);
            }
            base.Draw(gameTime);
            spriteBatch.End();
        }

        /// <summary>
        /// Registers an object in the objectmanager so that it will be 
        /// updated and drawn.
        /// </summary>
        /// <param name="obj">The object you want to register.</param>
        public void RegisterObject(GameObject obj)
        {
            objects.Add(obj);
        }

        /// <summary>
        /// Queue an object for removal during the next update phase.
        /// </summary>
        /// <param name="obj">The object you want removed.</param>
        public void UnregisterObject(GameObject obj)
        {
            objectsToRemove.Enqueue(obj);
        }

        public void RemoveAllObjects()
        {
            objectsToRemove.Clear();
            objects.Clear();
        }

        public void SetSpritebatch(SpriteBatch spriteBatcToUse)
        {
            spriteBatch = spriteBatcToUse;
        }
    }
}