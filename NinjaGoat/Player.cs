using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaGoat
{
    class Player : Entity
    {
        enum State
        {
            Flying,
            Grounded,
            Walled
        }

        Vector2 velocity;
        State state;

        const float MAX_SPEED = 10;
        const float JUMP_HEIGHT = 4;
        const float GRAVITY = 30;
        const float AIR_CONTROL = 0.1f;

        

        public Player(MyGameWindow window)
            : base(window)
        {
            Scale = Vector2.One;
            Position = new Vector2(5, 5);
        }

        public override void Update()
        {
            Vector2 p00 = Position;
            Vector2 p10 = Position + Scale * Vector2.UnitX;
            Vector2 p01 = Position + Scale * Vector2.UnitY;
            Vector2 p11 = Position + Scale;

            if (Window.Keyboard[Key.Left])
                velocity.X -= DT * 100;

            if (Window.Keyboard[Key.Right])
                velocity.X += DT * 100;

            if (Window.Keyboard[Key.A] && state == State.Grounded)
                velocity.Y += 15;

            velocity.Y -= DT * 40f;

            if (Window.currentLevel.Touching(this))
                throw new Exception();

            state = State.Flying;
            Position.Y += velocity.Y * DT;
            if (Window.currentLevel.Touching(this))
            {
                if (velocity.Y < 0)
                {
                    Position.Y = (int)Position.Y + 1;
                    state = State.Grounded;
                }
                if (velocity.Y > 0)
                    Position.Y = (int)Position.Y;
                velocity.Y = 0;
            }

            Position.X += velocity.X * DT;
            if (Window.currentLevel.Touching(this))
            {
                if (velocity.X < 0)
                    Position.X = (int)Position.X + 1;
                if (velocity.X > 0)
                    Position.X = (int)Position.X;
                velocity.X = 0;
                if (state == State.Flying)
                    state = State.Walled;
            }

            if(state == State.Grounded)
                velocity *= 0.95f;
            else
                velocity *= 0.999f;

            Window.Renderer.cameraPosition = Position;
        }
    }
}
