using MAH_Platformer.Levels.Blocks;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Entities
{
    public class EnemyEntity : Entity
    {
        public static float DEFAULT_SPEED = 100;

        public EnemyEntity(TextureRegion region, float x, float y)
            : base(region, x, y, Block.BLOCK_SIZE, Block.BLOCK_SIZE)
        {
        }

        public override void Update(float delta, bool processGravity = true)
        {
            base.Update(delta, processGravity);
            UpdateAI(delta);
        }

        private void UpdateAI(float delta)
        {
            // if (velocity.X == 0)
            // {
            velocity.X = DEFAULT_SPEED;
            //}
        }

        public override void Collide(Entity entity)
        {
            base.Collide(entity);

            if (entity is PlayerEntity)
            {
                if (velocity.Y >= 0 && entity.GetBounds().Bottom < position.Y)
                {
                    Alive = false;
                    entity.SetVelocity(entity.GetVelocity().X, 400);
                }
                else
                    ((PlayerEntity)entity).Respawn();
            }

            if (entity is BulletEntity)
            {
                Alive = false;
            }
        }
    }
}
