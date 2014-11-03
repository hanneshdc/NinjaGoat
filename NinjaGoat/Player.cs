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
            WalledLeft,
            WalledRight,
        }

        Vector2 velocity;
        State state;

        KeyboardState oldKeyState;

        // Player characteristics
        const float MAX_SPEED = 15;
        const float JUMP_HEIGHT = 5;
        const float GRAVITY = 70;
        const float RESPONSE_TIME_GROUND = 0.1f;
        const float RESPONSE_TIME_AIR = 0.1f;

        // Computed parameters
        static float groundDrag = (float)Math.Exp(-MyGameWindow.DT / RESPONSE_TIME_GROUND);
        static float airDrag = (float)Math.Exp(-MyGameWindow.DT / RESPONSE_TIME_AIR);
        static float groundAcc = MAX_SPEED * (1 - groundDrag) / groundDrag;
        static float airAcc = MAX_SPEED * (1 - airDrag) / airDrag;
        static float jumpVel = (float)Math.Sqrt(2 * GRAVITY * JUMP_HEIGHT);

        public Player(MyGameWindow window)
            : base(window)
        {
            Scale = Vector2.One;
            Position = new Vector2(5, 5);
        }

        public override void Update()
        {
            
            var keystate = OpenTK.Input.Keyboard.GetState();
            if (oldKeyState == null)
                oldKeyState = keystate;

            Vector2 p00 = Position;
            Vector2 p10 = Position + Scale * Vector2.UnitX;
            Vector2 p01 = Position + Scale * Vector2.UnitY;
            Vector2 p11 = Position + Scale;

            float dVX = 0;
            if (keystate[Key.Left]) dVX -= 1;
            if (keystate[Key.Right]) dVX += 1;

            velocity.X += dVX * (state == State.Grounded ? groundAcc : airAcc);


            if (keystate[Key.A] && !oldKeyState[Key.A])
            {
                if(state != State.Flying)
                    velocity.Y = jumpVel;

                if (state == State.WalledLeft)
                    velocity.X = jumpVel;

                if (state == State.WalledRight)
                    velocity.X = -jumpVel;
            }
                

            velocity.Y -= DT * GRAVITY;

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
                {
                    Position.X = (int)Position.X + 1;
                    if (state == State.Flying)
                        state = State.WalledLeft;
                }
                if (velocity.X > 0)
                {
                    Position.X = (int)Position.X;
                    if (state == State.Flying)
                        state = State.WalledRight;
                }
                velocity.X = 0;
            }

            if(state == State.Grounded)
                velocity.X *= groundDrag;
            else
                velocity.X *= airDrag;

            Window.Renderer.cameraPosition = Position;
            oldKeyState = keystate;
        }
    }
}
