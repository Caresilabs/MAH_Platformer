using MAH_Platformer.Levels.Blocks;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Entities
{
    public class SpawnEntity : Entity
    {
        public SpawnEntity(TextureRegion region, float x, float y)
            : base(region, x, y, Block.BLOCK_SIZE, Block.BLOCK_SIZE)
        {
            this.IsGravity = false;
            this.Collision = false;
        }

        public override void Collide(Entity entity)
        {
            base.Collide(entity);

            if (entity is PlayerEntity)
            {
                ((PlayerEntity)entity).SetSpawn(position.X, position.Y);
            }
        }
    }
}
