using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_King
{
    internal class Player : Character
    {
        float speed = 200;
        float gravitationForce = 300;
        float jumpForce = 0;
        float maxJumpForce = 10;
        float friction = 8;
        public int Width { get; set; } = 64; //ZMEN TOTO TY KOKOT POKUD ZMENIS POSTAAVICKU
        public int Height { get; set; } = 64;

        bool airborne = true;
        float lastPositionY;
        LevelManager levelManager;
        PlayerState state;
        PlayerState facing;
        public Player(LevelManager levelManager) 
        {
                this.levelManager = levelManager;
        }

        public PlayerState GetPlayerState()
        {
            return state;
        }

        public PlayerState GetPlayerFacing()
        {
            return facing;
        }

        public override PlayerState Move(GameTime gameTime)
        {
            Vector2 movement = Vector2.Zero;
            Vector2 gravitation = Vector2.Zero;
            gravitation.Y = gravitationForce * (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();
            if(keyboardState.IsKeyDown(Keys.A))
            {
                movement.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                state = PlayerState.Running;
                facing = PlayerState.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                movement.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                state = PlayerState.Running;
                facing = PlayerState.Right;
            }
            else
            {
                state = PlayerState.Idle;
            }
            ApplyMovement(movement);
            ApplyMovement(gravitation);

            if (lastPositionY == Position.Y)
            {
                airborne = false;
            }
            else
            {
                airborne = true;
            }
            if (jumpForce != 0)
            {
                jumpForce -= friction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (jumpForce< 0) jumpForce = 0;
            }
            if (keyboardState.IsKeyDown(Keys.Space)&&jumpForce==0&&!airborne)
            {
                jumpForce = maxJumpForce;
            }
            Vector2 jump = new Vector2(0, -jumpForce);
            ApplyMovement(jump);

            lastPositionY = Position.Y;
            return state;
        }
        
        public void ApplyMovement(Vector2 movement)
        {
            Position += movement;
            if (levelManager.CheckCollision(this))
            {
                Position -= movement;
            }
        }
    }
}
