using Microsoft.Xna.Framework;

namespace TheCloningGame
{
    public class GameObject
    {
        protected bool Active = true;

        public virtual void Draw(GameTime gameTime)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public bool IsActive()
        {
            return Active;
        }

        public void SetActive(bool active)
        {
            Active = active;
        }
    }
}
