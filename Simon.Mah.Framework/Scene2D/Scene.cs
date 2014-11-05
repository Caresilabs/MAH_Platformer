using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework.Scene2D
{
    public class Scene
    {
        private Actor root;
        private Camera2D camera;
        private EventListener listener;

        public Scene(Camera2D camera, EventListener listener = null)
        {
            this.root = new Actor();
            this.root.SetScene(this);
            this.camera = camera;
            this.listener = listener;
        }

        public void Update(float delta)
        {
            root.Update(delta);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.Deferred,
                      BlendState.AlphaBlend,
                      SamplerState.LinearClamp,
                      null,
                      null,
                      null,
                      camera.GetMatrix());

            root.Draw(batch);

            batch.End();
        }

        public float GetWidth()
        {
            return camera.GetWidth();
        }

        public float GetHeight()
        {
            return camera.GetHeight();
        }

        public void Add(string name, Actor actor)
        {
            root.Add(name, actor);
        }

        public Actor Find(string name)
        {
            return root.Find(name);
        }

        public void Remove(Actor actor)
        {
            root.Remove(actor.name);
        }

        public Vector2 Unproject(int x, int y)
        {
            return camera.Unproject(x, y);
        }

        public void CallEvent(Events e, Actor actor)
        {
            if (listener != null)
                listener.EventCalled(e, actor);
        }
    }
}
