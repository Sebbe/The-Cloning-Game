using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TheCloningGame.CollisionSystem;
using TheCloningGame.Input;

namespace TheCloningGame.GameObjects
{
    public class Player : GameObjectCollidable
    {
        protected float movementPerSecond = (float)360;
        protected SpriteBatch spriteBatch;
        protected Game game;

        private Texture2D pixel;
        protected Texture2D PlayerArt;

        private const int SpriteWidth = 24;
        private const int SpriteHeight = 32;

        protected Vector2 _size;
        //protected Vector2 _colPos;
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

        //sound

        private SoundEffect _foot_ambience;
        private SoundEffectInstance _foot_ambienceInstance;
        private double _foot_ambienceTimer;
        private bool _footPlay;

        private SoundEffect _woosh_ambience;
        private SoundEffectInstance _woosh_ambienceInstance;
        private double _woosh_ambienceTimer;
        private bool _wooshPlay;

        //key
        private bool haveKey;
        private bool doorOpen;

        public Player(Game baseGame, SpriteBatch spriteBatchToUse, Vector2 position)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            
            pixel = new Texture2D(baseGame.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            gravity = 981f;
            rotateTo = 0;
            game = baseGame;
           // _colPos = new Vector2((int) position.X - SpriteWidth, (int)position.Y-SpriteHeight);
            collisionBox = new Rectangle((int)position.X - SpriteWidth, (int)position.Y - SpriteHeight, 0, 0);
            _drawBox = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            _size = new Vector2(64, 72);
            rotation = 0;
            inAir = true;
        }
        
        /// <summary>
        /// Decides which character in the spritesheet to use
        /// </summary>
        /// <param name="charact"></param>
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

        /// <summary>
        /// Load content and other things
        /// </summary>
        public void LoadContent()
        {
            PlayerArt = game.Content.Load<Texture2D>(@"Pictures/player_tileset");
            collisionBox.Width = (int)_size.X/2;
            collisionBox.Height = (int)_size.Y/2;
            origo = new Vector2(SpriteWidth / 2, SpriteHeight/ 2);
            objectManager = (ObjectManager) game.Services.GetService(typeof (ObjectManager));
            collisionManager = (IManageCollisionsService) game.Services.GetService((typeof (IManageCollisionsService)));
            _input = (IInputService) game.Services.GetService(typeof (IInputService));


            _foot_ambience = game.Content.Load<SoundEffect>(
               @"Audio\Footsteps/Concrete/Footstep_Concrete_05");
            _foot_ambienceInstance = _foot_ambience.CreateInstance();
            _foot_ambienceInstance.IsLooped = true;
            _foot_ambienceTimer = _foot_ambience.Duration.TotalSeconds;
            _foot_ambience.Play();
            _footPlay = false;
        

            _woosh_ambience = game.Content.Load<SoundEffect>(
               @"Audio\Whoosh/short_whoosh_02");
            _woosh_ambienceInstance = _foot_ambience.CreateInstance();
            _woosh_ambienceInstance.IsLooped = true;
            _woosh_ambienceTimer = _foot_ambience.Duration.TotalSeconds;

            haveKey = false;
            doorOpen = false;
        }

        /// <summary>
        /// Update function which decides what happens each frame
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                _animationTimer += (float) gameTime.ElapsedGameTime.TotalSeconds;
                
                if (!inAir && _input.IsKeyPressed(Keys.Space))
                {
                    inAir = true;
                    _woosh_ambience.Play();
                    gravity = -gravity;
                    rotateTo += 3.14f;
                    if (_footPlay)
                    {
                        _foot_ambienceInstance.Pause();
                        _footPlay = false;
                    }
                }

                if (rotateTo > rotation)
                {
                    rotation += 9*(float) gameTime.ElapsedGameTime.TotalSeconds;
                    if (rotation > rotateTo)
                    {
                        rotation = rotateTo;
                    }
                }

                if (inAir)
                {

                    position.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //_colPos.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //position.Y += gravity*(float) gameTime.ElapsedGameTime.TotalSeconds;
                } else {
                    if (_input.IsKeyDown(Keys.A))
                    {
                        _animationWalk = gravity > 0 ? 3 : 1;
                        Animate();
                        if(!_footPlay)
                        {
                            _foot_ambienceInstance.Play();
                            _footPlay = true;
                        }
                        position.X -= movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                        //_colPos.X -= movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        List<GameObjectCollidable> test;
                        test = collisionManager.CheckCollision(this);
                        if (test.Count == 0)
                        {

                            _woosh_ambience.Play();
                            inAir = true;
                            if (_footPlay)
                            {
                                _foot_ambienceInstance.Pause();
                                _footPlay = false;
                            }
                        }
                        else
                        {
                            foreach (GameObjectCollidable t in test)
                            {
                                if (t as Wall != null)
                                {
                                    position.X = t.CollisionBox.Right + 22;
                                }
                            }
                        }
                    }
                    else if (_input.IsKeyDown(Keys.D))
                    {
                        _animationWalk = gravity < 0 ? 3 : 1;
                        Animate();
                        if (!_footPlay)
                        {
                            _foot_ambienceInstance.Play();
                            _footPlay = true;
                        }
                        position.X += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                        //_colPos.X += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;

                        List<GameObjectCollidable> test;
                        test = collisionManager.CheckCollision(this);
                        if (test.Count == 0)
                        {
                            _woosh_ambience.Play();
                            inAir = true;
                            if (_footPlay)
                            {
                                _foot_ambienceInstance.Pause();
                                _footPlay = false;
                            }
                        }
                        else
                        {
                            foreach (GameObjectCollidable t in test)
                            {
                                if (t as Wall != null)
                                {
                                    position.X = t.CollisionBox.Left - 22;
                                }
                            }
                        }
                    } else
                    {
                        _footPlay = false;
                        _foot_ambienceInstance.Pause();
                    }
                }
                //rotation += 1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                CollisionBox = new Rectangle((int)position.X - SpriteWidth + 4, (int)position.Y - SpriteHeight, (int)_size.X - 24, (int)_size.Y);
                _drawBox = new Rectangle((int) position.X, (int) position.Y, (int) _size.X, (int) _size.Y);
                _sourceRectangle = new Rectangle(DrawSpriteWidth + SpriteWidth*_animationFrame,
                                                 DrawSpriteHeight + SpriteHeight*_animationWalk, SpriteWidth,
                                                 SpriteHeight);
                //base.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw function
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            //spriteBatch.Draw(pixel, collisionBox, null, Color.DarkGreen); //Draws a rectangle with the collbox
            spriteBatch.Draw(PlayerArt, _drawBox, _sourceRectangle, Color.White, rotation, origo, SpriteEffects.None, 0);
           
            //base.Draw(gameTime);
        }

        /// <summary>
        /// Decides which sprite in the spritesheet to be used
        /// </summary>
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

        /// <summary>
        /// Called each frame to decide if the level is done
        /// </summary>
        /// <returns></returns>
        public bool LevelDone()
        {
            return doorOpen;
        }


        /// <summary>
        /// Collisions handling if it collidades with something
        /// </summary>
        /// <param name="goc"></param>
        public override void Collision(GameObjectCollidable goc)
        {
            if(goc as Key != null)
            {
                haveKey = true;
            }
            if(goc as Door != null)
            {
                if(haveKey)
                {
                    doorOpen = true;
                }
            }
            if (goc as Floor != null)
            {
                if (gravity > 0)
                {
                    position.Y = goc.CollisionBox.Top - goc.CollisionBox.Height + 4;
                } else
                {
                    position.Y = goc.CollisionBox.Bottom + goc.CollisionBox.Height - 9;
                }
                inAir = false;
            }
            if (goc as Wall != null)
            {
                if (gravity > 0)
                {
                    if(_animationWalk == 3)
                    {
                        position.X = goc.CollisionBox.Right + 22;
                    } else
                    {
                        position.X = goc.CollisionBox.Left - 22;
                    }
                }
                else
                {
                    if (_animationWalk == 3)
                    {
                        position.X = goc.CollisionBox.Left - 22;
                    }
                    else
                    {
                        position.X = goc.CollisionBox.Right + 22;
                    }
                }
            }
        }
    }
}
