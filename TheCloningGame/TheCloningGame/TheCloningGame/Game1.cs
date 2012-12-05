using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheCloningGame.GameObjects;
using TheCloningGame.Input;
using TheCloningGame.CollisionSystem;
using TheCloningGame.LevelManager;

namespace TheCloningGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Color clearColor;
        private ILevel curLevel;
        private int level;

        private ObjectManager objectManager;
        private IManageCollisionsService collisionDetectionService;
        private InputManager _input;
        private IManageLevels levelManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Services
            objectManager = new ObjectManager(this);
            collisionDetectionService = new CollisionDetectionService(this);
            _input = new InputManager(this);

            Services.AddService(typeof(ObjectManager), objectManager);
            Services.AddService(typeof(IManageCollisionsService), collisionDetectionService);
            Services.AddService(typeof(IInputService), _input);

            objectManager.SetSpritebatch(spriteBatch);

            levelManager = new LevelManager.LevelManager();
            levelManager.Init(this, spriteBatch);
            curLevel = levelManager.NextLevel();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            curLevel.LoadContent();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _input.Update(gameTime);

            if (curLevel != null)
            {
                if (!curLevel.IsLevelLoaded())
                {
                    curLevel.LoadContent();
                }
                collisionDetectionService.Update(gameTime);
                objectManager.Update(gameTime);
                curLevel.Update(gameTime);

                if (curLevel.LevelDone())
                {
                    curLevel.UnLoadContent();
                    curLevel = levelManager.NextLevel();
                } 
                
                else if (curLevel.GameOver())
                {
                    curLevel.UnLoadContent();
                    
                }

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            objectManager.Draw(gameTime);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
