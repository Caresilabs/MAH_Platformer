﻿using MAH_Platformer.Levels.Blocks;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
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
            : base(region, x, y, Block.BLOCK_SIZE, Block.BLOCK_SIZE / 2f)
        {
            sprite.AddAnimation("run", new FrameAnimation(Assets.crawler, 0, 0, 44, 22, 2, .21f));
            sprite.SetAnimation("run");
        }

        public override void Update(float delta, bool processGravity = true)
        {
            base.Update(delta, processGravity);
            UpdateAI(delta);
        }

        private void UpdateAI(float delta)
        {
            if (Id % 2 == 0)
                velocity.X = DEFAULT_SPEED;
            else
                velocity.X = -DEFAULT_SPEED;

            sprite.Effect = velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
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
                {
                    Alive = false;
                    entity.Alive = false;
                }
            }

            if (entity is BulletEntity)
            {
                Alive = false;
            }
        }
    }
}
