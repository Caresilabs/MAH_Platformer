using MAH_Platformer.Levels.Blocks;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Entities
{
    public class BulletEntity : Entity
    {
        public Entity Owner { get; set; }

        public float StateTime { get; set; }

        public BulletEntity(TextureRegion region, float x, float y, float vx, float vy)
            : base(region, x, y, Block.BLOCK_SIZE / 4, Block.BLOCK_SIZE / 4)
        {
            this.velocity.X = vx;
            this.velocity.Y = vy;
            this.IsGravity = false;
            this.Collision = false;
            this.StateTime = 0;
            this.FrictionModifier = 3;
        }

        public override void Update(float delta, bool processGravity = true)
        {
            base.Update(delta, false);
            this.StateTime += delta;

            if (StateTime > 2.5f)
                Alive = false;
        }

        public override void Collide(Entity entity)
        {
            base.Collide(entity);
            if (Owner.Equals(entity)) return;

            if (entity is BulletEntity)
                if (((BulletEntity)entity).Owner == Owner) return;

            Alive = false;
        }
    }
}
