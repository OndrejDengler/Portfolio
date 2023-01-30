using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gym_King
{
    internal class LevelScene : Scene
    {
        LevelManager levelManager;
        LevelEditor levelEditor;
        Player player;
        KeyboardState keyboard;
        KeyboardState oldKeyboard;
        MouseState mouse;
        Button button;
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;

        public LevelScene()
        {

        }

        public override void Load(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            levelManager = new LevelManager(1, spriteBatch, graphicsDevice);
            levelManager.Initialize();
            player = new Player(levelManager);
            player.Load("Content/player.png", graphicsDevice);
            button = new Button(690, 50, 50, 50,"Test");
            button.LoadFont("arial", 16, graphicsDevice);
            button.OnClick += Button_OnClick;
        }

        private void Button_OnClick(object sender, EventArgs e)
        {
            levelEditor = new LevelEditor(this); 
            button.Text = "Build";
        }

        public override void Update(GameTime gameTime)
        {
            PlayerState state = player.Move(gameTime);
            PlayerState facing = player.GetPlayerFacing();
            player.Update(state, facing, gameTime);
            mouse = Mouse.GetState();
            if(levelEditor!= null)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    levelEditor.Update();
                }
            }
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                levelEditor.IsSet = false;
                levelEditor.Ready = false;
            }
            if (oldKeyboard.IsKeyDown(Keys.Enter)&&keyboard.IsKeyUp(Keys.Enter)&&levelEditor.Ready)
            {
                levelEditor.IsSet = false;
                levelEditor.Ready = false;
                levelEditor.Confirm = true;
                //Reload();
            }
            oldKeyboard = keyboard;
            button.Update();
        }

        public void Reload()
        {
            //levelManager = new LevelManager(1, spriteBatch, graphicsDevice);
            levelManager.Initialize();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            levelManager.DrawLevel();
            player.Draw(spriteBatch);
            button.Draw(spriteBatch);
            if (levelEditor != null)
            {
                levelEditor.Draw(spriteBatch);
            }
        }
    }
}
