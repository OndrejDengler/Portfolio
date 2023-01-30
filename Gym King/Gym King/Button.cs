using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteFontPlus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gym_King
{
    internal class Button
    {
        Vector2 Position { get; set; }
        Vector2 TextPosition { get; set; }
        public event Clicked OnClick;
        int Width { get { return BoundingBox.Width; } }
        int Height { get { return BoundingBox.Height; } }
        public Rectangle BoundingBox;
        Vector2 textRectangle;
        MouseState previousState;
        MouseState currentState;
        string path;
        public string Text { get; set; }
        SpriteFont font;

        public Button(Vector2 position, int width, int height, string text)
        {
            BoundingBox = new Rectangle(position.ToPoint(), new Point(width, height));
            Text = text;
        }

        public Button(int x , int y, int width, int height, string text)
        {
            BoundingBox = new Rectangle(x,y, width, height);
            Text = text;
        }
        public void LoadFont(string name,int height,GraphicsDevice graphics)
        {
            path = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
                 $"{name}.ttf"
                 );

            font = TtfFontBaker.Bake(
                File.ReadAllBytes(path),
                height,
                1024,
                1024,
                new[] { CharacterRange.BasicLatin }
                ).CreateSpriteFont(graphics);
        }
        public void Update()
        {
            currentState = Mouse.GetState();
            if (BoundingBox.Contains(currentState.Position)&& currentState.LeftButton==ButtonState.Released&&
                previousState.LeftButton==ButtonState.Pressed)
            {
                OnClick?.Invoke(this, new EventArgs());

            }
            previousState = Mouse.GetState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            textRectangle = font.MeasureString(Text);
            TextPosition = new Vector2((BoundingBox.Width-textRectangle.X)/2+BoundingBox.X, (BoundingBox.Height-textRectangle.Y)/2+BoundingBox.Y);
            spriteBatch.Begin();
            spriteBatch.Draw(GymKing.BlankTexture, BoundingBox, Color.Red);
            spriteBatch.DrawString(font, Text, TextPosition, Color.White);
            spriteBatch.End();
        }
    }
}
