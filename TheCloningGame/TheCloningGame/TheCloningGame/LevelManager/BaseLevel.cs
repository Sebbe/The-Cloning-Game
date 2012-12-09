using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheCloningGame.CollisionSystem;
using TheCloningGame.GameObjects;

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

        public virtual void BuildLevel()
        {
            Floor temp = new Floor(TheGame, TheSpriteBatch, new Vector2(0, 860), new Vector2(1024, 40));
            temp.LoadContent();

            objectManager.RegisterObject(temp);
            collisionManager.RegisterObject(temp);



            temp = new Floor(TheGame, TheSpriteBatch, new Vector2(0, 0), new Vector2(1024, 40));
            temp.LoadContent();

            objectManager.RegisterObject(temp);
            collisionManager.RegisterObject(temp);


            Wall tempWall = new Wall(TheGame, TheSpriteBatch, new Vector2(0, 40), new Vector2(40, 824));
            tempWall.LoadContent();
            objectManager.RegisterObject(tempWall);
            collisionManager.RegisterObject(tempWall);

            tempWall = new Wall(TheGame, TheSpriteBatch, new Vector2(984, 40), new Vector2(40, 824));
            tempWall.LoadContent();
            objectManager.RegisterObject(tempWall);
            collisionManager.RegisterObject(tempWall);
        }
    }
}