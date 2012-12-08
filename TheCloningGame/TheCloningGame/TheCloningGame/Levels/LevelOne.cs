using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheCloningGame.GameObjects;
using TheCloningGame.LevelManager;

namespace TheCloningGame.Levels
{
    class LevelOne : BaseLevel
    {
        private Player[] _players;
        public override void LoadContent()
        {
            // Players
            _players = new Player[4];
            for (int i = 0; i < 4; i++)
            {
                _players[i] = new Player(TheGame, TheSpriteBatch, new Vector2(100 * i, 100));
                _players[i].LoadContent();
                _players[i].SetActive(false);
                _players[i].SetCharacter(i);
                objectManager.RegisterObject(_players[i]);
                collisionManager.RegisterObject(_players[i]);
            }

            _players[0].SetActive(true);
            Floor temp = new Floor(TheGame, TheSpriteBatch, new Vector2(0, 880));
            temp.LoadContent();

            objectManager.RegisterObject(temp);
            collisionManager.RegisterObject(temp);

            temp = new Floor(TheGame, TheSpriteBatch, new Vector2(0, 0));
            temp.LoadContent();

            objectManager.RegisterObject(temp);
            collisionManager.RegisterObject(temp);
            // the level is loaded
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
