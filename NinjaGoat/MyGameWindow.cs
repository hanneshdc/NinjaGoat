using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Input;

namespace NinjaGoat
{
    class MyGameWindow : GameWindow
    {
        Entity entityList = null;
        public Renderer Renderer;

        public Entity player;
        public Level currentLevel;

        public const float DT = 1 / 60f;

        

        public void AddEntity(Entity entity)
        {
            if (entityList != null)
                entityList.prev = entity;
            entity.next = entityList;
            entityList = entity;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color4.CornflowerBlue);
            Renderer = new Renderer(this);

            player = new Player(this);
            currentLevel = new Level(this, "level1.bmp");
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            foreach (Entity entity in entityList)
                entity.Update();

            if (Keyboard[Key.Escape])
                Exit();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Renderer.Draw(entityList);

            SwapBuffers();
        }
    }
}
