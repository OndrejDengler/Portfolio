using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_King
{
    internal static class LevelLoader
    {
        public static List<LevelElement> LoadFromFile(string path,GraphicsDevice graphicsDevice)
        {
            try
            {
                List<LevelElement> list = new List<LevelElement>();
                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    LevelElement levelElement;
                    if (line == "")
                    {
                        continue;
                    }
                    string[] data = line.Split('|');
                    levelElement.CorX = int.Parse(data[0]);
                    levelElement.CorY = int.Parse(data[1]);
                    levelElement.Width = int.Parse(data[2]);
                    levelElement.Height = int.Parse(data[3]);
                    levelElement.Texture = Texture2D.FromFile(graphicsDevice, data[4]);
                    list.Add(levelElement);
                }
                return list;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

    }
}
