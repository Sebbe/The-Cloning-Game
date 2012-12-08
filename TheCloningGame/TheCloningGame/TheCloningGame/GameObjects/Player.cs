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

        protected Texture2D PlayerArt;

        private const int SpriteWidth = 24;
        private const int SpriteHeight = 32;
        protected Vector2 _size;
        protected Rectangle _sourceRectangle;
        protected Rectangle _drawBox;

        protected float rotation;
        protected float rotateTo;
        protected float gravity;

        protected ObjectManager objectManager;
        protected IManageCollisionsService collisionManager;
        protected IInputService _input;

        private int DrawSpriteWidth = 0;
        private int DrawSpriteHeight = 0;

        private Vector2 origo;
        private bool inAir;

        int _animationFrame;
        float _animationTimer;
        int _animationWalk;
        int _startFrame = 0;
        int _endFrame = 3;
        private const float animationStepTime = 1f / 10;

        int _character = 0;

        public Player(Game baseGame, SpriteBatch spriteBatchToUse, Vector2 position)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            gravity = 981f;
            rotateTo = 0;
            game = baseGame;
            collisionBox = new Rectangle((int) position.X, (int) position.Y, 0, 0);
            _drawBox = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            _size = new Vector2(64, 72);
            rotation = 0;
            inAir = true;
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
            PlayerArt = game.Content.Load<Texture2D>(@"Pictures/player_tileset");
            collisionBox.Width = SpriteWidth;
            collisionBox.Height = SpriteHeight;
            origo = new Vector2(SpriteWidth / 2, SpriteHeight / 2);
            objectManager = (ObjectManager) game.Services.GetService(typeof (ObjectManager));
            collisionManager = (IManageCollisionsService) game.Services.GetService((typeof (IManageCollisionsService)));
            _input = (IInputService) game.Services.GetService(typeof (IInputService));
        }

        public override void Update(GameTime gameTime)
        {
            _animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            /*
           if(_input.IsKeyDown(Keys.W))
            {
                _animationWalk = 0;
                Animate();
                position.Y -= movementPerSecond*(float)gameTime.ElapsedGameTime.TotalSeconds;
            } else if(_input.IsKeyDown(Keys.S))
            {
                _animationWalk = 2;
                Animate();
                position.Y += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
            }*/
            
            if(!inAir && _input.IsKeyPressed(Keys.Space))
            {
                inAir = true;
                gravity = -gravity;
                rotateTo += 3.14f;
            }

            if(rotateTo > rotation)
            {
                rotation += 9*(float)gameTime.ElapsedGameTime.TotalSeconds;
                if(rotation > rotateTo)
                {
                    rotation = rotateTo;
                }
            }

            if(inAir)
            {
                position.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if(_input.IsKeyDown(Keys.A))
            {
                _animationWalk = 3;
                Animate();
                position.X -= movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (_input.IsKeyDown(Keys.D))
            {
                _animationWalk = 1;
                Animate();
                position.X += movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            //rotation += 1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            CollisionBox = new Rectangle((int)position.X, (int)position.Y, (int)_size.X, (int)_size.Y / 2);
            _drawBox = new Rectangle((int)position.X, (int)position.Y, (int)_size.X, (int)_size.Y);
            _sourceRectangle = new Rectangle(DrawSpriteWidth + SpriteWidth * _animationFrame, DrawSpriteHeight + SpriteHeight * _animationWalk, SpriteWidth, SpriteHeight);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(PlayerArt, _drawBox, _sourceRectangle, Color.White, rotation, origo, SpriteEffects.None, 0);
            base.Draw(gameTime);
        }

        private void Animate()
        {
            
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

        public override void Collision(GameObjectCollidable goc)
        {
            if (goc as Floor != null)
            {
                if (gravity > 0)
                {
                    position.Y = goc.CollisionBox.Top - _size.X/2;
                } else
                {
                    position.Y = goc.CollisionBox.Bottom + _size.X/2;
                }
                inAir = false;
            }
        }
    }
}
