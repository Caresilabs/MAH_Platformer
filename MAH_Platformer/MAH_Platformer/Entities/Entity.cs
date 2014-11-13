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

        public bool IsGrounded { get; set; }

        public bool Alive { get; set; }

        protected Vector2 velocity;

        public Entity(TextureRegion region, float x, float y, float width, float height)
            : base(region, x, y, width, height)
        {
            this.IsGravity = true;
            this.Alive = true;
            this.IsGrounded = false;
            this.velocity = new Vector2();
            this.Gravity = World.GRAVITY;
        }

        public virtual void Update(float delta, bool processGravity = true) // TODO hmm check if virtual is working
        {
            // Entity is moving
            if (processGravity || IsGravity)
            {
                Vector2 oldPosition = position;

                if (!Vector2.Equals(velocity, Vector2.Zero))
                {
                    List<World.Direction> dirs = IsFree(position.X, position.Y);
                    if (dirs.Count != 0)
                    {
                       // if (dirs.Contains(World.Direction.DOWN) && !IsGrounded)
                       // {
                            IsGrounded = true;
                            //position = oldPosition;
                            velocity.Y = 0;
                            IsGravity = false;
                       // }

                        if (dirs.Contains(World.Direction.LEFT))
                        {
                            //position = oldPosition;
                            //velocity = new Vector2();
                            //IsGravity = false;
                        }
                    }
                    else 
                    {
                        IsGravity = true;
                        IsGrounded = false;
                    }
                }

                if (processGravity && IsGravity)
                    velocity += Gravity * delta;

                if (CurrentBlock != null)
                    velocity *= CurrentBlock.GetFriction(this);

                position += velocity * delta;

                UpdatePos(position);
               
            }

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

            if (Level.GetBlock(oldPos) != CurrentBlock)
            {
                CurrentBlock.RemoveEntity(this);

                CurrentBlock = Level.GetBlock(oldPos + new Vector2(0, bounds.Height + Block.BLOCK_SIZE/3 ));

                Level.GetBlock(position).AddEntity(this);
            }
        }

        private List<World.Direction> IsFree(float x, float y)
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
                    return dirs; // TODO!!!!
                }
            }

            bool blocks = false;
            Block[,] bks = Level.GetBlocks();
            for (int j = 0; j < bks.GetLength(1); j++)
                for (int i = 0; i < bks.GetLength(0); i++)
                    if (Level.GetBlock(i, j).Blocks(this) && Level.GetBlock(i, j).GetBounds().Intersects(bounds))
                    {
                        blocks = true;
                       // position.Y = Level.GetBlock(i, j).GetBounds().Y - bounds.Height / 2 - Level.GetBlock(i, j).GetBounds().Height/2 - 5;
                        if (x < i * Block.BLOCK_SIZE + Block.BLOCK_SIZE / 2) dirs.Add(World.Direction.LEFT);
                        if (y < j * Block.BLOCK_SIZE + Block.BLOCK_SIZE / 2) dirs.Add(World.Direction.DOWN);
                    }

            return dirs;//!blocks;
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
