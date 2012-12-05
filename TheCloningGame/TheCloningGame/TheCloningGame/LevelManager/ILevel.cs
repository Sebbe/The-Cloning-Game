using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheCloningGame.LevelManager
{
    public interface ILevel
    {
        void Init(Game game, SpriteBatch spriteBatchToUse);
        void LoadContent();
        void UnLoadContent();
        void Update(GameTime gameTime);
        bool LevelDone();
        bool GameOver();
        bool IsLevelLoaded();
    }
}