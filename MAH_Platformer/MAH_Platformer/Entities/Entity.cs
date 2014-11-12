using MAH_Platformer.Levels;
using MAH_Platformer.Levels.Blocks;
using MAH_Platformer.Model;
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

        public Vector2 Gravity { get; set; }

        public Block CurrentBlock { get; set; }

        public bool IsGravity { get; set; }

        public bool Alive { get; set; }

        protected Vector2 velocity;

        public Entity(TextureRegion region, float x, float y, float width, float height)
            : base(region, x, y, width, height)
        {
            this.IsGravity = true;
            this.Alive = true;
            this.velocity = new Vector2();
            this.Gravity = World.GRAVITY;
        }

        public virtual void Update(float delta, bool processGravity = true) // TODO hmm check if virtual is working
        {
            // Entity is moving
            if (processGravity && IsGravity)
            {
                Vector2 oldPosition = position;

                if (processGravity && IsGravity)
                    velocity += Gravity * delta;

                if (CurrentBlock != null)
                    velocity *= CurrentBlock.GetFriction(this) * delta;

                position += velocity * delta;

                if (!Vector2.Equals(velocity, Vector2.Zero))
                {
                    if (!IsFree(position.X, position.Y))
                    {
                        position = oldPosition;
                        velocity = new Vector2();
                    }
                    else
                        UpdatePos(oldPosition);
                }
            }

            base.Update(delta);
        }

        // Update what block im in
        private void UpdatePos(Vector2 oldPos)
        {
            if (CurrentBlock == null)
                CurrentBlock = Level.GetBlock(position);

            if (Level.GetBlock(oldPos) != CurrentBlock)
            {
                CurrentBlock.RemoveEntity(this);

                CurrentBlock = Level.GetBlock(oldPos);

                Level.GetBlock(position).AddEntity(this);
            }
        }

        private bool IsFree(float x, float y)
        {
            // Loop all entities
            List<Entity> es = Level.GetEntities();
            foreach (var e in es)
            {
                if (e == this) continue;

                if (e.Blocks(this))
                {
                    e.Collide(this);
                    this.Collide(e);
                    return false;
                }
            }
            return true;
        }

        public virtual void Collide(Entity entity) { }

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
