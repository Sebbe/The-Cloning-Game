using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheCloningGame.CollisionSystem;

namespace TheCloningGame.LevelManager
{
    class BaseLevel : ILevel
    {
        protected Game TheGame;
        protected SpriteBatch TheSpriteBatch;
        protected ObjectManager objectManager;
        protected IManageCollisionsService collisionManager;
        protected bool levelLoaded;

        public void Init(Game game, SpriteBatch spriteBatchToUse)
        {
            TheSpriteBatch = spriteBatchToUse;
            TheGame = game;
            objectManager = (ObjectManager)TheGame.Services.GetService(typeof(ObjectManager));
            collisionManager =
                (IManageCollisionsService)game.Services.GetService((typeof(IManageCollisionsService)));
            objectManager.RemoveAllObjects();
            collisionManager.RemoveAllObjects();
            levelLoaded = false;
        }

        public virtual void LoadContent()
        {
            levelLoaded = true;
        }

        public virtual void UnLoadContent()
        {
            objectManager.RemoveAllObjects();
            collisionManager.RemoveAllObjects();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual bool LevelDone()
        {
            return false;
        }

        public virtual bool GameOver()
        {
            return false;
        }

        public virtual bool IsLevelLoaded()
        {
            return levelLoaded;
        }
    }
}