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
        Vector2 velocity;

        public Player(MyGameWindow window)
            : base(window)
        {
            Scale = Vector2.One;
            Position = new Vector2(5, 5);
        }

        public override void Update()
        {
            if (Window.Keyboard[Key.Left])
                velocity.X -= DT * 100;

            if (Window.Keyboard[Key.Right])
                velocity.X += DT * 100;

            if (Window.Keyboard[Key.Up])
                velocity.Y += DT * 100;

            if (Window.Keyboard[Key.Down])
                velocity.Y -= DT * 100;

            velocity.Y -= DT * 10f;

            Position.X += velocity.X * DT;
            if (Window.currentLevel.Touching(this))
            {
                Position.X -= velocity.X * DT;
                velocity.X = 0;
            }

            Position.Y += velocity.Y * DT;
            if (Window.currentLevel.Touching(this))
            {
                Position.Y -= velocity.Y * DT;
                velocity.Y = 0;
            }

            velocity *= 0.999f;

            Window.Renderer.cameraPosition = Position;
        }
    }
}
