using MAH_Platformer.Levels.Blocks;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Entities
{
    public class BoulderEntity : Entity
    {
        public BoulderEntity(TextureRegion region, float x, float y)
            : base(region, x, y, Block.BLOCK_SIZE, Block.BLOCK_SIZE)
        {
            this.FrictionModifier = 1.1f;
        }

        public override void Update(float delta, bool processGravity = true)
        {
            sprite.Rotation += velocity.X * delta / 60;

            base.Update(delta, processGravity);
        }

        public override void Collide(Entity entity)
        {
            base.Collide(entity);

           // if (entity is Entity)
           // {
                if (bounds.Top < entity.GetBounds().Bottom - 16)
                    velocity.X = entity.GetVelocity().X * 2;//((PlayerEntity)entity).GetVelocity().X*2;
           // }
        }
    }
}
