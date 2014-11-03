using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaGoat
{
    class Renderer
    {
        public Vector2 cameraPosition;
        float worldHeight = 5;
        int vertexBuffer, indexBuffer;
        GameWindow GameWindow;

        public Renderer(GameWindow window)
        {
            GameWindow = window;

            Vertex[] vertices = new Vertex[4];
            vertices[0].Position = new Vector3(-.5f, -.5f, 0);
            vertices[0].Color = new Vector3(1, 1, 0);
            vertices[1].Position = new Vector3(-.5f, .5f, 0);
            vertices[2].Position = new Vector3(.5f, .5f, 0);
            vertices[3].Position = new Vector3(.5f, -.5f, 0);

            int[] indices = new int[] { 0, 1, 2, 0, 2, 3 };

            vertexBuffer = GL.GenBuffer();
            indexBuffer = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(4 * 6 * vertices.Length), vertices, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(4 * indices.Length), indices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Draw(Entity entities)
        {
            GL.LoadIdentity();
            float aspect = (float)GameWindow.Width / GameWindow.Height;
            GL.Scale(1 / (aspect * worldHeight), 1 / worldHeight, 0);
            GL.Translate(-cameraPosition.X, -cameraPosition.Y, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
            GL.InterleavedArrays(InterleavedArrayFormat.C3fV3f, 4 * 6, (IntPtr)0);

            foreach (Entity entity in entities)
            {
                GL.PushMatrix();
                GL.Translate(entity.Position.X, entity.Position.Y, 0);
                GL.Scale(entity.Scale.X, entity.Scale.Y, 0);
                GL.Rotate(entity.Rotation, 0, 0, 1);
                GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);
                GL.PopMatrix();
            }
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
    }
}
