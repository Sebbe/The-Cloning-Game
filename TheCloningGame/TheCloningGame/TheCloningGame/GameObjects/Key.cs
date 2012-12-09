using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheCloningGame.CollisionSystem;

namespace TheCloningGame.GameObjects
{
    public class Key : GameObjectCollidable
    {
        protected SpriteBatch spriteBatch;
        protected Game game;

        protected Texture2D Art;

        private const int SpriteWidth = 20;
        private const int SpriteHeight = 40;
        protected Vector2 _size;
        protected Rectangle _sourceRectangle;

        protected ObjectManager objectManager;
        protected IManageCollisionsService collisionManager;

        int _character = 0;

        public Key(Game baseGame, SpriteBatch spriteBatchToUse, Vector2 position)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            game = baseGame;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            _size = new Vector2(SpriteWidth, SpriteHeight);
        }

        public void LoadContent()
        {
            Art = game.Content.Load<Texture2D>(@"Pictures/key");
            collisionBox.Width = (int)_size.X;
            collisionBox.Height = (int)_size.Y;
            _sourceRectangle = new Rectangle(0, 0, (int)_size.X, (int)_size.Y);
            objectManager = (ObjectManager)game.Services.GetService(typeof(ObjectManager));
            collisionManager = (IManageCollisionsService)game.Services.GetService((typeof(IManageCollisionsService)));
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(Art, collisionBox, _sourceRectangle, Color.White);
            base.Draw(gameTime);
        }

        public override void Collision(GameObjectCollidable goc)
        {
            if(goc as Player != null)
            {
                DeleteFlag = true;
                objectManager.UnregisterObject(this);
            }
            base.Collision(goc);
        }
    }
}
