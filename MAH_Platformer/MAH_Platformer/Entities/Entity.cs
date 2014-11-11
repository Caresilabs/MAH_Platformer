using MAH_Platformer.Levels;
using MAH_Platformer.Levels.Blocks;
using Microsoft.Xna.Framework;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Entities
{
    public abstract class Entity : GameObject
    {
        public Level Level { get; set; }

        public Vector2 Velocity { get; set; }

        public Vector2 Gravity { get; set; }

        public Block CurrentBlock { get; set; }

        public bool Flying { get; set; }

        public bool Alive { get; set; }

        public Entity(TextureRegion region, float x, float y, float width, float height) : base(region, x, y, width, height)
        {
            this.Flying = false;
            this.Alive = true;
        }

        public void Update(float delta, bool processGravity = true) // TODO hmm check if virtual is working
        {
            if (processGravity)
                Velocity += Gravity * delta;

            if (CurrentBlock != null)
                Velocity *= CurrentBlock.GetFriction(this) * delta;

            position += Velocity * delta;

            base.Update(delta);
        }

        private bool IsFree(float x, float y)
        {
            return true;
        }

        public virtual void Collide(Entity entity) {}

        public virtual bool Blocks(Entity entity)
        {
            return bounds.Intersects(entity.bounds);
        }

       /* public bool PixelCollition(Entity other)
        {
            Color[] dataA = new Color[tex.Width * tex.Height];
            tex.GetData(dataA);
            Color[] dataB = new Color[other.tex.Width * other.tex.Height];
            other.tex.GetData(dataB);
            int top = Math.Max(hitBox.Top, other.hitBox.Top);
            int bottom = Math.Min(hitBox.Bottom, other.hitBox.Bottom);
            int left = Math.Max(hitBox.Left, other.hitBox.Left);
            int right = Math.Min(hitBox.Right, other.hitBox.Right);
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = dataA[(x - hitBox.Left) +
                    (y - hitBox.Top) * hitBox.Width];
                    Color colorB = dataB[(x - other.hitBox.Left) +
                   (y - other.hitBox.Top) *
                   other.hitBox.Width];
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }*/
    }
}
