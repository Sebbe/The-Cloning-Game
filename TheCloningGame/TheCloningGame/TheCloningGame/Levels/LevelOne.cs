using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheCloningGame.GameObjects;
using TheCloningGame.LevelManager;

namespace TheCloningGame.Levels
{
    class LevelOne : BaseLevel
    {

        public override void LoadContent()
        {
            

            levelLoaded = true;
        }

        public override void UnLoadContent()
        {
            levelLoaded = false;
            base.UnLoadContent();
        }

        public override bool LevelDone()
        {
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override bool GameOver()
        {
            return false;
        }
    }
}
