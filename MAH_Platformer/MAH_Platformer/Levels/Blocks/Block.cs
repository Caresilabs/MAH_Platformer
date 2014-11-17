using MAH_Platformer.Entities;
using Microsoft.Xna.Framework;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Levels.Blocks
{
    public abstract class Block : GameObject
    {
        public static float BLOCK_SIZE = 32;

        public List<Entity> Entities { get; set; }

        public Level Level { get; set; }

        public bool BlocksMotion { get; set; }

        public int Id { get; set; }

        public Block(TextureRegion region, float x, float y)
            : base(region, x, y, BLOCK_SIZE, BLOCK_SIZE)
        {
            this.Entities = new List<Entity>();
            this.BlocksMotion = false;
            this.sprite.ZIndex = .5f;
        }

        public virtual void Use(string item)
        {
        }

        public virtual void Enter(Entity entity)
        {
        }

        public virtual void Collide(Entity entity)
        {
        }

        public virtual bool Blocks(Entity entity)
        {
            return BlocksMotion;
        }

        public virtual float GetWalkSpeed(Entity entity)
        {
            return 1;
        }

        public virtual float GetFriction(Entity entity)
        {
            return 1f;
        }

        public void AddEntity(Entity entity)
        {
            this.Entities.Add(entity);
            this.Enter(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            this.Entities.Remove(entity);
        }

        public int GetBaseId()
        {
            return Id - (Id % LevelIO.ID_PER_BASE);
        }
        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
