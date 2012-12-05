using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheCloningGame.LevelManager
{
    public interface IManageLevels
    {
        void Init(Game theGame, SpriteBatch spriteBatchToUse);
        ILevel NextLevel();
        ILevel PrevLevel();
        ILevel GetLevel(int levelID);
    }
}
