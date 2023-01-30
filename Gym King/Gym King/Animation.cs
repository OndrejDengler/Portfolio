using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_King
{
    internal class Animation
    {
        public const float MaxTime = 0.5f;
        public int Start { get; private set; }
        public int End { get; private set; }
        public int Current { get; private set; } = 0;
        public float CurrentTime { get; private set; } = 0.1f;

        private int width = 64;
        private int heigth = 64;
        private int columns = 8;
        private int row = 0;
        private int column = 0;
        private int corX = 0;
        private int corY = 0;
        public Animation()
        {
        }

        public void CalculateCurrentFrame(PlayerState state,GameTime gameTime)
        {
            if (state == PlayerState.Running)
            {
                Start = 4;
                End = 11;
                if (Current > 6)
                {
                    Current = 0;
                }
            }
            else if (state == PlayerState.Jump)
            {
                /*Start = 45;
                End = 47;
                if (Current > 0) // TODO: Mozna budu muset odebrat cislo -1
                {
                    Current = 0;
                }*/
                Current = 45;
            }
            else
            {
                Start = 0;
                End = 0;
            }

            CurrentTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (CurrentTime <= 0)
            {
                Current++;
                CurrentTime = 0.1f;
            }

            if (state == PlayerState.Idle)
            {
                Current = 64;
            }
        }


        public Rectangle CalculateRectangle()
        {
            Current += Start;

            row = Current / columns;
            column = Current % columns;
            corX = column * 64;
            corY = row * 64;
            Current -= Start;
            return new Rectangle(corX, corY, width, heigth);
        }
    }
}
