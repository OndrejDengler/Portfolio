using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_King
{
    internal abstract class Character
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; } = new Vector2(400, 100);
        PlayerState playerState;
        SpriteEffects spriteEffects = SpriteEffects.None;
        PlayerState facing;
        Animation animation;

        public void Load(string path, GraphicsDevice graphicsDevice)
        {
            Texture = Texture2D.FromFile(graphicsDevice, path);
            animation = new Animation();
        }
        public abstract PlayerState Move(GameTime gameTime);

        public void Update(PlayerState state,PlayerState facing,GameTime gameTime)
        {
            playerState = state;
            this.facing = facing;
            animation.CalculateCurrentFrame(playerState,gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = animation.CalculateRectangle();
            spriteBatch.Begin();
            if(facing == PlayerState.Left)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else if(facing == PlayerState.Right)
            {
                spriteEffects = SpriteEffects.None;
            }
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0,Vector2.Zero,1, spriteEffects, 1);
            spriteBatch.End();
        }

    }
}
