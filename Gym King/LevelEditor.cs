using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gym_King
{
    internal class LevelEditor
    {
        Vector2 position;
        Vector2 nextPosition;
        LevelScene levelScene;
        public bool IsSet = false;
        public bool Ready = false;
        public bool Confirm = false;

        public LevelEditor(LevelScene levelScene)
        {
            this.levelScene = levelScene;
        }
        public void Update()
        {
            if(!IsSet)
            {
                position = Mouse.GetState().Position.ToVector2();
                IsSet= true;
            }
            else
            {
                nextPosition = Mouse.GetState().Position.ToVector2();
                Ready = true;
            }

        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, Math.Abs((int)(nextPosition.X - position.X)), Math.Abs((int)(nextPosition.Y - position.Y)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

           if(Ready)
           {
                spriteBatch.Draw(GymKing.BlankTexture, GetRectangle(), new Color(Color.Green, 0.5f));
           }
           if(Confirm)
           {
                //Pridat do souboru
               //spriteBatch.Draw(GymKing.BlankTexture, GetRectangle(), Color.Green);
                Add();
                levelScene.Reload();
                Confirm = false;
           }
            spriteBatch.End();
        }

        public void Add()
        {
            Rectangle rectangle = GetRectangle();
            string text = rectangle.X+"|"+rectangle.Y+"|"+rectangle.Width+"|"+rectangle.Height+ "|Content/Grass.png";
            try
            {
                using (StreamWriter sw = File.AppendText("Levels/level1.txt"))
                {
                    sw.WriteLine(text);
                    sw.Close();
                }
            }
            catch
            {
                // Nakej hezkej catch
            }
        }

    }
}
