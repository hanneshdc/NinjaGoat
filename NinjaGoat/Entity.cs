using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaGoat
{
    class Entity : IEnumerable<Entity>
    {
        public const float DT = 1 / 60f;

        public Vector2 Position = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
        public float Rotation = 0;

        internal Entity next;
        internal Entity prev;

        public readonly MyGameWindow Window;

        public Entity(MyGameWindow window)
        {
            this.Window = window;
            Window.AddEntity(this);
        }

        public Entity(MyGameWindow gameWindow, Vector2 position, Vector2 scale, float rotation)
        {
            this.Window = gameWindow;
            this.Position = position;
            this.Scale = scale;
            this.Rotation = rotation;
            Window.AddEntity(this);
        }

        public virtual void Update() { }

        public void Delete()
        {
            if (prev != null)
                prev.next = next;
            if (next != null)
                next.prev = prev;
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return new EntityEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class EntityEnumerator : IEnumerator<Entity>
        {
            Entity first;
            public Entity Current { get; private set; }

            public EntityEnumerator(Entity first)
            {
                this.first = first;
            }

            public void Dispose()
            { }

            public bool MoveNext()
            {
                if (Current == null)
                {
                    Current = first;
                    return true;
                }
                if (Current.next == null)
                    return false;
                Current = Current.next;
                return true;
            }

            public void Reset()
            {
                Current = null;
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}
