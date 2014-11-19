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

        public bool Collision { get; set; }

        public bool IsGravity { get; set; }

        public bool IsGrounded { get; set; }

        public bool Alive { get; set; }

        public float FrictionModifier { get; set; }

        protected Vector2 velocity;

        public Entity(TextureRegion region, float x, float y, float width, float height)
            : base(region, x, y, width, height)
        {
            this.sprite.ZIndex = .3f;
            this.IsGravity = true;
            this.Alive = true;
            this.IsGrounded = false;
            this.Collision = true;
            this.velocity = new Vector2();
            this.FrictionModifier = 1;
            this.Gravity = World.GRAVITY;
        }

        public virtual void Update(float delta, bool processGravity = true) // TODO hmm check if virtual is working
        {
            // Entity is moving
            //if (processGravity || IsGravity)
            // {
            Vector2 oldPosition = position;

            if (processGravity && IsGravity)
                velocity += Gravity * delta;

            if (CurrentBlock != null)
                velocity *= MathHelper.Clamp(CurrentBlock.GetFriction(this) * FrictionModifier, 0, 1);

            position += velocity * delta;

            UpdateBounds();

            // Collision
            if (!Vector2.Equals(velocity, Vector2.Zero))
            {
                List<World.Direction> dirs = UpdateCollisions();
                if (dirs.Count != 0)
                {
                    if (dirs.Contains(World.Direction.DOWN))
                    {
                        IsGrounded = true;
                        //position = oldPosition;
                        velocity.Y = 0;
                        IsGravity = false;
                        OnGrounded();
                    }
                }
                else
                {
                    if (processGravity)
                    {
                        IsGravity = true;
                        IsGrounded = false;
                    }
                }
            }

            UpdatePos(position);

            // }

            base.Update(delta);
        }

        // Update what block im in
        private void UpdatePos(Vector2 oldPos)
        {
            if (CurrentBlock == null)
            {
                CurrentBlock = Level.GetBlock(position);
                CurrentBlock.AddEntity(this);
            }

            Vector2 newPos = oldPos + new Vector2(0, bounds.Height);
            if (Level.GetBlock(newPos) != CurrentBlock)
            {
                CurrentBlock.RemoveEntity(this);

                CurrentBlock = Level.GetBlock(newPos);

                Level.GetBlock(position).AddEntity(this);
            }
        }

        private List<World.Direction> UpdateCollisions()
        {
            List<World.Direction> dirs = new List<World.Direction>();

            // Loop all entities
            List<Entity> es = Level.GetEntities();
            foreach (var e in es)
            {
                if (e == this) continue;

                if (e.Blocks(this))
                {
                    e.Collide(this);
                    this.Collide(e);

                    if (e.Collision && Collision)
                        ProcessCollision(dirs, e.bounds);
                }
            }

            Block[,] bks = Level.GetBlocks();
            for (int j = 0; j < bks.GetLength(1); j++)
            {
                for (int i = 0; i < bks.GetLength(0); i++)
                {
                    Block block = Level.GetBlock(i, j);
                    if (block is AirBlock) continue;

                    if (block.GetBounds().Intersects(bounds))
                    {
                        //if (PixelCollition(block)) {
                        block.Collide(this);
                        if (block.Blocks(this))
                            ProcessCollision(dirs, block.GetBounds());
                        //}
                    }
                }

            }

            return dirs;
        }

        private void ProcessCollision(List<World.Direction> dirs, Rectangle tBounds)
        {
            Rectangle inter = Rectangle.Intersect(bounds, tBounds);
            Vector2 norVelocity = new Vector2(velocity.X, velocity.Y);
            norVelocity.Normalize();

            if (inter.Height - 4 > inter.Width)
            {
                if (position.X < tBounds.X)
                    position.X -= inter.Width - 0;
                else
                    position.X += inter.Width - 0;

                velocity.X = 0;
            }
            else
            {
                if (position.Y < tBounds.Y)
                {
                    position.Y -= (inter.Height - 1);
                    dirs.Add(World.Direction.DOWN);
                }
                else
                    position.Y += (inter.Height +4);

                velocity.Y = 0;
            }
        }

        public virtual void OnGrounded() { }

        public virtual void Collide(Entity entity) { }

        public virtual bool Blocks(Entity entity)
        {
            return bounds.Intersects(entity.bounds);
        }

        public void SetVelocity(float x, float y)
        {
            velocity.X = x;
            velocity.Y = y;
        }

        public Vector2 GetVelocity()
        {
            return velocity;
        }

        public bool PixelCollition(GameObject other)
        {
            if (other.sprite.Region == null || sprite.Region == null) return false;

            Color[] dataA = new Color[sprite.Region.GetTexture().Width * sprite.Region.GetTexture().Height];
            sprite.Region.GetTexture().GetData(dataA);
            Color[] dataB = new Color[other.sprite.Region.GetTexture().Width * other.sprite.Region.GetTexture().Height];
            other.sprite.Region.GetTexture().GetData(dataB);

            int top = Math.Max(bounds.Top, other.GetBounds().Top);
            int bottom = Math.Min(bounds.Bottom, other.GetBounds().Bottom);
            int left = Math.Max(bounds.Left, other.GetBounds().Left);
            int right = Math.Min(bounds.Right, other.GetBounds().Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = dataA[(int)((x - bounds.Left) / sprite.GetRealScale().X) +
                    (int)((y - bounds.Top) * (bounds.Width / sprite.GetRealScale().X) / sprite.GetRealScale().Y)];

                    Color colorB = dataB[(int)((x - other.GetBounds().Left) / other.sprite.GetRealScale().X ) +
                   (int)((y - other.GetBounds().Top) * (other.GetBounds().Width / other.sprite.GetRealScale().X) / sprite.GetRealScale().Y)];

                    if (colorA.A != 0 && colorB.A != 0) // Collision
                        return true;
                }
            }
            return false;
        }
    }
}
