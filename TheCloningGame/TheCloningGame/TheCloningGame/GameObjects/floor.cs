using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheCloningGame.CollisionSystem;

namespace TheCloningGame.GameObjects
{
    public class Floor : GameObjectCollidable
    {
        protected SpriteBatch spriteBatch;
        protected Game game;

        protected Texture2D Art;

        private const int SpriteWidth = 24;
        private const int SpriteHeight = 32;
        protected Vector2 _size;
        protected Rectangle _sourceRectangle;

        protected ObjectManager objectManager;
        protected IManageCollisionsService collisionManager;

        private int DrawSpriteWidth = 0;
        private int DrawSpriteHeight = 0;

        int _character = 0;

        public Floor(Game baseGame, SpriteBatch spriteBatchToUse, Vector2 position)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            game = baseGame;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            _size = new Vector2(64, 72);
        }

        public void LoadContent()
        {
            Art = game.Content.Load<Texture2D>(@"Pictures/beam_floor");
            collisionBox.Width = 1024;
            collisionBox.Height = 20;
            _sourceRectangle = new Rectangle(0, 0, collisionBox.Width, collisionBox.Height);
            objectManager = (ObjectManager)game.Services.GetService(typeof(ObjectManager));
            collisionManager = (IManageCollisionsService)game.Services.GetService((typeof(IManageCollisionsService)));
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(Art, collisionBox, _sourceRectangle, Color.White);
            base.Draw(gameTime);
        }
    }
}
