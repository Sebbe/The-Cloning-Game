using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheCloningGame.CollisionSystem;
using TheCloningGame.Input;

namespace TheCloningGame.GameObjects
{
    public class Player : GameObjectCollidable
    {
        protected float movementPerSecond = (float)360;
        protected SpriteBatch spriteBatch;
        protected Game game;

        protected Texture2D playerArt;

        private const int SpriteWidth = 24;
        private const int SpriteHeight = 32;
        protected Vector2 _size;
        protected Rectangle _sourceRectangle;
        protected float rotation;

        protected ObjectManager objectManager;
        protected IManageCollisionsService collisionManager;
        protected IInputService _input;

        private int DrawSpriteWidth = 0;
        private int DrawSpriteHeight = 0;

        int _animationFrame;
        float _animationTimer;
        int _animationWalk;
        int _startFrame = 0;
        int _endFrame = 3;
        private const float animationStepTime = 1f / 3;

        int _character = 0;

        private bool isActive;
        public Player(Game baseGame, SpriteBatch spriteBatchToUse, Vector2 position)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            game = baseGame;
            collisionBox = new Rectangle((int) position.X, (int) position.Y, 0, 0);
            _size = new Vector2(64, 72);
            isActive = true;
        }

        public bool Active() { return isActive; }
        public void SetActive(bool active)
        {
            isActive = active; 
        }
        public void SetCharacter(int charact)
        {
            _character = charact;
            if (_character <= 3)
            {
                DrawSpriteWidth = (SpriteWidth * (_character * 3));
            }
            else
            {
                DrawSpriteWidth = (SpriteWidth * ((_character - 4) * 3));
                DrawSpriteHeight = SpriteHeight * 4;
            }
        }

        public void LoadContent()
        {
            playerArt = game.Content.Load<Texture2D>(@"Pictures/player_tileset");
            collisionBox.Width = SpriteWidth;
            collisionBox.Height = SpriteHeight;
            objectManager = (ObjectManager) game.Services.GetService(typeof (ObjectManager));
            collisionManager = (IManageCollisionsService) game.Services.GetService((typeof (IManageCollisionsService)));
            _input = (IInputService) game.Services.GetService(typeof (IInputService));
        }

        public override void Update(GameTime gameTime)
        {
            if(_input.IsKeyDown(Keys.W))
            {
                _animationWalk = 0;
                Animate((float)gameTime.ElapsedGameTime.TotalSeconds);
                position.Y -= movementPerSecond*(float)gameTime.ElapsedGameTime.TotalSeconds;
            } else if(_input.IsKeyDown(Keys.S))
            {
                _animationWalk = 2;
                Animate((float)gameTime.ElapsedGameTime.TotalSeconds);
                position.Y += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(_input.IsKeyDown(Keys.A))
            {
                _animationWalk = 3;
                Animate((float)gameTime.ElapsedGameTime.TotalSeconds);
                position.X -= movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (_input.IsKeyDown(Keys.D))
            {
                _animationWalk = 1;
                Animate((float)gameTime.ElapsedGameTime.TotalSeconds);
                position.X += movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            rotation += 1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            CollisionBox = new Rectangle((int)position.X, (int)position.Y, (int)_size.X, (int)_size.Y);
            _sourceRectangle = new Rectangle(DrawSpriteWidth + SpriteWidth * _animationFrame, DrawSpriteHeight + SpriteHeight * _animationWalk, SpriteWidth, SpriteHeight);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(playerArt, collisionBox, _sourceRectangle, Color.White);
            base.Draw(gameTime);
        }

        private void Animate(float deltaTime)
        {
            _animationTimer += deltaTime;
            if (_animationTimer >= animationStepTime)
            {
                _animationFrame++;
                _animationTimer = 0f;
                if (_animationFrame == _endFrame)
                {
                    _animationFrame = _startFrame;
                }
            }
        }
    }
}
