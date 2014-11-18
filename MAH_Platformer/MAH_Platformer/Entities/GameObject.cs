using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Entities
{
    public abstract class GameObject
    {
        protected Sprite sprite;
        protected Rectangle bounds;
        protected Vector2 position;

        public GameObject(TextureRegion region, float x, float y, float width, float height)
        {
            this.sprite = new Sprite(region, x, y, width, height);
            this.position = new Vector2(x, y);
            this.bounds = new Rectangle((int)x, (int)y, (int)width, (int)height);
        }

        public virtual void Update(float delta)
        {
            sprite.SetPosition(position);
            UpdateBounds();
            sprite.Update(delta);
        }

        public virtual void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch);
        }

        public void UpdateBounds()
        {
            bounds.X = (int)(position.X - (bounds.Width / 2));
            bounds.Y = (int)(position.Y - (bounds.Height / 2));
        }

        public void SetPosition(Vector2 pos)
        {
            position.X = pos.X;
            position.Y = pos.Y;
        }

        public Rectangle GetBounds()
        {
            return bounds;
        }
    }
}
