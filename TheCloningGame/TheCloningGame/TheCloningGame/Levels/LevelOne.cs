﻿using Microsoft.Xna.Framework;
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
                _players[i] = new Player(TheGame, TheSpriteBatch, new Vector2(100 + (100 * i), 100));
                _players[i].LoadContent();
                _players[i].SetActive(false);
                _players[i].SetCharacter(i);
                objectManager.RegisterObject(_players[i]);
                collisionManager.RegisterObject(_players[i]);
            }

            _players[0].SetActive(true);
            BuildLevel();
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
            return _players[0].LevelDone();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override bool GameOver()
        {
            return false;
        }

        public override void BuildLevel()
        {
            
            var temp = new Floor(TheGame, TheSpriteBatch, new Vector2(40, 400), new Vector2(400, 40));
            temp.LoadContent();

            objectManager.RegisterObject(temp);
            collisionManager.RegisterObject(temp);

            temp = new Floor(TheGame, TheSpriteBatch, new Vector2(400, 190), new Vector2(400, 40));
            temp.LoadContent();

            objectManager.RegisterObject(temp);
            collisionManager.RegisterObject(temp);

            temp = new Floor(TheGame, TheSpriteBatch, new Vector2(550, 380), new Vector2(450, 40));
            temp.LoadContent();

            objectManager.RegisterObject(temp);
            collisionManager.RegisterObject(temp);

            var tempWall = new Wall(TheGame, TheSpriteBatch, new Vector2(400, 200), new Vector2(40, 400));
            tempWall.LoadContent();
            objectManager.RegisterObject(tempWall);
            collisionManager.RegisterObject(tempWall);

            var door = new Door(TheGame, TheSpriteBatch, new Vector2(350, 320));
            door.LoadContent();
            objectManager.RegisterObject(door);
            collisionManager.RegisterObject(door);

            var key = new Key(TheGame, TheSpriteBatch, new Vector2(350, 440));
            key.LoadContent();
            objectManager.RegisterObject(key);
            collisionManager.RegisterObject(key);

            base.BuildLevel();
        }
    }
}
