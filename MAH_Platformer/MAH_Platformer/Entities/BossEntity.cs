using MAH_Platformer.Levels.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Entities
{
    public class BossEntity : Entity
    {
        public static float DEFAULT_SPEED = 100;
        public const float SHOOT_SPEED = 700f;

        public int Life { get; set; }

        public int State { get; set; }

        public int Counter { get; set; }

        public float StateTime { get; set; }

        public float YSpeed { get; set; }

        public bool Reloading { get; set; }

        public BossEntity(TextureRegion region, float x, float y)
            : base(region, x, y, Block.BLOCK_SIZE * 4.2f, Block.BLOCK_SIZE * 4.2f)
        {
            this.Life = 75;
            this.YSpeed = 120;
            this.StateTime = 0;
            this.Counter = 0;
            this.Collision = false;
            this.Reloading = false;
            InitAnimations();
        }

        private void InitAnimations()
        {
            sprite.AddAnimation("run", new FrameAnimation(Assets.boss, 0, 0, 128, 128, 3, .21f));
            sprite.SetAnimation("run");
        }

        public override void Update(float delta, bool processGravity = true)
        {
            base.Update(delta, false);

            UpdateAI(delta);
        }

        private void UpdateAI(float delta)
        {
            this.StateTime += delta;

            switch (State)
            {
                case 0:
                    velocity.X = -DEFAULT_SPEED;
                    if (StateTime > 4)
                        ShootAtPlayer();
                    if (StateTime > 5)
                        SetState(1);
                    break;
                case 1:
                    velocity.X = DEFAULT_SPEED;
                    if (StateTime > 4)
                        ShootCircle();
                    if (StateTime > 5)
                        SetState(0);
                    break;
                case 2:
                    if (StateTime > 1)
                    {
                        if (Counter < 10)
                        {
                            Counter++;
                            ShootSpray();
                            SetState(2);
                            break;
                        }
                        else
                        {
                            SetState(3);
                            Counter = 0;
                        }
                    }
                    break;
                case 3:
                    velocity.X = 0;
                    if (StateTime > 3.5f)
                    {
                        if (Counter < 3)
                        {
                            Counter++;
                            ShootCircle();
                            SetState(3);
                            break;
                        }
                        else
                        {
                            Counter = 0;
                            SetState(2);
                        }
                    }
                    break;
                case 4:
                    YSpeed = 160;
                    if (StateTime > 1.3f)
                    {
                        if (Counter < 3)
                        {
                            Counter++;
                            ShootCircle();
                            SetState(4);
                            break;
                        }
                        else
                        {
                            Counter = 0;
                            SetState(5);
                        }
                    }
                    break;
                case 5:
                    velocity.X = -DEFAULT_SPEED;
                    if (StateTime > 4)
                        ShootCircle();
                    if (StateTime > 5)
                    {
                        SetState(6);
                        velocity.X *= -1;
                    }
                    break;
                case 6:
                    if (StateTime > 3)
                    {
                        ShootAtPlayer();
                        SetState(6);
                    }
                    break;
                default:
                    break;
            }

            velocity.Y = (float)Math.Cos(StateTime * 1) * YSpeed;

            sprite.Effect = velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        private void ShootCircle()
        {
            if (Reloading) return;

            for (int i = 0; i < 17; i++)
            {
                float offset = MathUtils.random(0, .5f);
                Vector2 direction = new Vector2((float)Math.Cos(i + offset), (float)Math.Sin(i + offset));
                direction.Normalize();

                BulletEntity bullet = new BulletEntity(Assets.GetRegion("BoulderEntity"),
                   position.X, position.Y, direction.X * SHOOT_SPEED, direction.Y * SHOOT_SPEED);
                bullet.Owner = this;
                Level.AddEntity(bullet);
            }
            Reloading = true;
        }

        private void ShootAtPlayer()
        {
            if (Reloading) return;

            Vector2 direction = new Vector2();
            Vector2 playerPos = Level.GetPlayer().GetPosition();
            Vector2.Subtract(ref playerPos, ref position, out direction);
            direction.Normalize();

            BulletEntity bullet = new BulletEntity(Assets.GetRegion("BoulderEntity"),
               position.X, position.Y, direction.X * SHOOT_SPEED * 1.1f, direction.Y * SHOOT_SPEED * 1.1f);
            bullet.Owner = this;
            Level.AddEntity(bullet);
            Reloading = true;
        }

        private void ShootSpray()
        {
            if (Reloading) return;

            for (int i = 0; i < 12; i++)
            {
                Vector2 direction = new Vector2((float)Math.Cos(i), (float)Math.Sin(i));
                direction.Normalize();

                BulletEntity bullet = new BulletEntity(Assets.GetRegion("BoulderEntity"),
                   position.X, position.Y, velocity.X + direction.X * SHOOT_SPEED, direction.Y * SHOOT_SPEED);
                bullet.Owner = this;
                Level.AddEntity(bullet);
            }
            Reloading = true;
        }

        private void SetState(int state)
        {
            this.StateTime = 0;
            this.State = state;
            this.Reloading = false;
        }

        public override void Collide(Entity entity)
        {
            base.Collide(entity);

            if (entity is PlayerEntity)
            {
                if (velocity.Y >= 0 && entity.GetBounds().Bottom < position.Y)
                {
                    //Alive = false;
                    //entity.SetVelocity(entity.GetVelocity().X, 400);
                }
                else
                    ((PlayerEntity)entity).Alive = false;
            }

            if (entity is BulletEntity)
            {
                if (((BulletEntity)entity).Owner != this)
                    Hurt();
            }
        }

        public void Hurt()
        {
            Life--;

            if (Life == 60)
            {
                SetState(2);
                velocity.X *= -1.2f;
            }

            if (Life == 50)
            {
                SetState(4);
                velocity.X *= -1.2f;
            }

            if (Life == 25)
            {
                SetState(6);
            }


            if (Life <= 0)
            {
                Alive = false;
                Level.NextLevel();
            }
        }
    }
}
