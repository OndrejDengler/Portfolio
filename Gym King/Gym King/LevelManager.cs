using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gym_King
{
    internal class LevelManager
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        List<LevelElement> list;
        public int Level { get; set; }
        public LevelManager(int level,SpriteBatch spriteBatch,GraphicsDevice graphicsDevice) 
        { 
            Level = level;
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
        }

        public void Initialize()
        {
            list = LevelLoader.LoadFromFile("Levels/level1.txt", graphicsDevice);

        }
        public void DrawLevel()
        {
            spriteBatch.Begin();
            foreach (LevelElement element in list)
            {
                 Draw(element.Texture,element.CorX,element.CorY,element.Width,element.Height);
            }
            spriteBatch.End();
        }

        private void Draw(Texture2D texture,int corX, int corY, int width, int height)
        {
            spriteBatch.Draw(texture,new Rectangle(corX,corY,width,height),Color.White);
        }

        public bool CheckCollision(Player player)
        {
            foreach (LevelElement element in list)
            {
                Rectangle rectangle = new Rectangle(element.CorX, element.CorY, element.Width, element.Height);
                Rectangle rectanglePlayer = new Rectangle((int)player.Position.X+16, (int)player.Position.Y, player.Width-32, player.Height);
                if (rectanglePlayer.Intersects(rectangle))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
