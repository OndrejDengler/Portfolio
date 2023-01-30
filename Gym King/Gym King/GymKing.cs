using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_King
{
    internal class GymKing : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        SpriteBatch spriteBatch;
        Scene activeScene;
        public static Texture2D BlankTexture { get; private set; }
        public GymKing()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 854,
                PreferredBackBufferHeight = 480
            };
            IsMouseVisible = true;
            activeScene = new LevelScene();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            BlankTexture = new Texture2D(GraphicsDevice, 1, 1);
            BlankTexture.SetData(new Color[1] { Color.White });
            activeScene.Load(spriteBatch, GraphicsDevice);
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            activeScene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            activeScene.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }

        public void SwitchScene(Scene scene)
        {
            activeScene = scene;
        }

    }
}
