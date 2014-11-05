using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework.Scene2D
{
    public interface TouchListener
    {
        void TouchDown(Vector2 mouse);

        void TouchUp(Vector2 mouse);

        void TouchLeave(Vector2 mouse);

        void KeyDown(Keys key);

        void KeyReleased(Keys key);
    }


    public enum Events
    {
        TouchDown, TouchUp, TouchLeave, KeyDown, KeyUp
    }

    public interface EventListener
    {
        void EventCalled(Events e, Actor actor);
    }

    public class Actor : TouchListener
    {
        public TouchListener listener;
        public bool isTouched;
        public string name;

        private Dictionary<string, Actor> actors;
        private Scene scene;
        private Rectangle bounds;
        private TextureRegion region;
        private Actor parent;

        public Actor(float x, float y, float width, float height)
        {
            this.bounds = new Rectangle((int)x, (int)y, (int)width, (int)height);
            this.actors = new Dictionary<string, Actor>();
            this.isTouched = false;
        }

        public Actor()
            : this(0, 0, 0, 0)
        {
        }

        public virtual void Init() { }

        public void Update(float delta)
        {
            UpdateInput();

           
            for (int i = 0; i < actors.Count; i++)
            {
                actors.Values.ToArray()[i].Update(delta);
            }
        }

        private void UpdateInput()
        {
            Vector2 mouse = scene.Unproject(Mouse.GetState().X, Mouse.GetState().Y);
            if (InputHandler.Clicked())
            {
                if (bounds.Contains((int)mouse.X, (int)mouse.Y))
                    TouchUp(mouse);
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (bounds.Contains((int)mouse.X, (int)mouse.Y))
                    TouchDown(mouse);
            }

            if (isTouched)
            {
                if (!bounds.Contains((int)mouse.X, (int)mouse.Y))
                    TouchLeave(mouse);
            }

            foreach (var key in Keyboard.GetState().GetPressedKeys())
            {
                if (InputHandler.KeyDown(key))
                    KeyDown(key);
            }

        }

        public virtual void Draw(SpriteBatch batch)
        {
            foreach (var actor in actors)
            {
                actor.Value.Draw(batch);
            }
        }

        public void Add(string name, Actor actor)
        {
            actor.SetScene(GetScene());
            actor.name = name;
            actor.SetParent(this);
            actor.Init();
            actors.Add(name, actor);
        }

        public void SetParent(Actor parent)
        {
            this.parent = parent;
        }

        public Actor GetParent()
        {
            return parent;
        }

        public void SetSize(float width, float height)
        {
            this.bounds.Width = (int)width;
            this.bounds.Height = (int)height;
        }

        public void SetPosition(float x, float y)
        {
            this.bounds.X = (int)x;
            this.bounds.Y = (int)y;
        }

        public float GetX()
        {
            return bounds.X;
        }

        public float GetY()
        {
            return bounds.Y;
        }

        public Rectangle GetBounds()
        {
            return bounds;
        }

        public void SetScene(Scene scene)
        {
            this.scene = scene;
        }

        public Scene GetScene()
        {
            return scene;
        }

        public void SetRegion(TextureRegion region)
        {
            this.region = region;
        }

        public TextureRegion GetRegion()
        {
            return region;
        }

        // INPUT
        public virtual void TouchDown(Vector2 mouse)
        {
            isTouched = true;
            scene.CallEvent(Events.TouchDown, this);
        }

        public virtual void TouchUp(Vector2 mouse)
        {
            isTouched = false;
            scene.CallEvent(Events.TouchUp, this);
        }

        public virtual void TouchLeave(Vector2 mouse)
        {
            isTouched = false;
            scene.CallEvent(Events.TouchLeave, this);
        }

        public virtual void KeyDown(Keys key)
        {
            scene.CallEvent(Events.TouchDown, this);
        }

        public virtual void KeyReleased(Keys key)
        {
            scene.CallEvent(Events.KeyUp, this);
        }

        public Actor Find(string name)
        {
            return actors[name];
        }

        public void Remove(string p)
        {
            actors.Remove(name);
        }
    }
}
