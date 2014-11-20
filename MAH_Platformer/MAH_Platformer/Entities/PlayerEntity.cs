using MAH_Platformer.Levels.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Entities
{
    public class PlayerEntity : Entity
    {
        public const float WIDTH = 46;
        public const float HEIGHT = 80;

        public const float DEFAULT_SPEED = 170;
        public const float DEFAULT_JUMP = -640;
        public const float MAX_SPEED = 1050;

        public const float SHOOT_DELAY = .3f;
        public const float SHOOT_SPEED = 650f;

        public enum PlayerState
        {
            RUNNING, CLIMBING, IDLE, JUMPING
        }

        private PlayerState state;
        private Vector2 spawnPoint;
        private Point direction;
        private float speed;
        private float reloadTime;
        private int jumps;

        public PlayerEntity(TextureRegion region, float x, float y)
            : base(region, x, y, WIDTH, HEIGHT)
        {
            this.sprite.ZIndex = .05f;
            this.speed = DEFAULT_SPEED;
            this.spawnPoint = new Vector2();
            this.direction = new Point(1, 0);
            this.jumps = 0;
            this.reloadTime = 0;
            
            InitAnimations();
            this.SetState(PlayerState.IDLE);
        }

        private void InitAnimations()
        {
            sprite.AddAnimation("run", new FrameAnimation(Assets.character, 0, 160, 48, 64, 4, .1f));
            sprite.AddAnimation("idle", new FrameAnimation(Assets.character, 0, 80, 48, 64, 3, .2f));
            sprite.AddAnimation("jump", new FrameAnimation(Assets.character, 240, 464, 48, 32, 1, .2f));
            sprite.AddAnimation("shoot", new FrameAnimation(Assets.character, 352, 96, 16*5, 64, 3, .2f));
        }

        public override void Update(float delta, bool processGravity = true)
        {
            this.reloadTime += delta;

            UpdateInput();
            UpdateStates(delta);
            
            UpdateDirection(delta);

            velocity.Y = MathHelper.Clamp(velocity.Y, -MAX_SPEED, MAX_SPEED);
            base.Update(delta, state != PlayerState.CLIMBING);
        }

        private void UpdateInput()
        {
            // Move
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                velocity.X = -speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                velocity.X = speed;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (!IsGrounded)
                    velocity.Y += speed / 3;
            }

            if (InputHandler.KeyDown(Keys.Space))
            {
                if (IsGrounded || jumps < 2)
                    Jump();
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Shoot();
            }

            if (state == PlayerState.CLIMBING)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    velocity.Y = -DEFAULT_SPEED * 2.5f;
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                    velocity.Y = DEFAULT_SPEED * 2.5f;
                else
                    velocity.Y = 0;
            }
        }

        private void UpdateDirection(float delta)
        {
            if (velocity.X > 0) direction.X = 1;
            if (velocity.X < 0) direction.X = -1;
            if (velocity.Y > 0) direction.Y = 1;
            if (velocity.Y < 0) direction.Y = -1;

            sprite.Effect = direction.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        private void Shoot()
        {
            if (reloadTime > SHOOT_DELAY)
            {
                BulletEntity bullet = new BulletEntity(Assets.GetRegion("BoulderEntity"),
                    position.X, position.Y, velocity.X + direction.X * SHOOT_SPEED, 0);
                bullet.Owner = this;
                Level.AddEntity(bullet);
                reloadTime = 0;
            }
        }

        private void UpdateStates(float delta)
        {
            // Check for death
            if (position.Y > Level.GetBlocks().GetLength(1) * Block.BLOCK_SIZE + Block.BLOCK_SIZE * 7)
            {
                Alive = false;
            }

            if (Math.Abs(velocity.X) < 8f)
            {
                if (state == PlayerState.RUNNING)
                    SetState(PlayerState.IDLE);
            }
            else if (state == PlayerState.IDLE)
                SetState(PlayerState.RUNNING);

            // Check for ladder
            if (Level.GetBlock(position) is LadderBlock)
            {
                if (!Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.S)) return;
                if (!(state == PlayerState.JUMPING || IsGrounded) || state == PlayerState.CLIMBING) return;

                if (IsGrounded)
                    position.Y -= 4;

                SetState(PlayerState.CLIMBING);
                IsGravity = false;
                OnGrounded();
                velocity = new Vector2();
            }
            else
            {
                if (state == PlayerState.CLIMBING)
                {
                    SetState(PlayerState.JUMPING);
                    IsGravity = true;
                    IsGrounded = false;
                }
            }
        }

        public override void Collide(Entity entity)
        {
            base.Collide(entity);

            if (entity is BulletEntity)
            {
                if (((BulletEntity)entity).Owner != this)
                    Alive = false;
            }
        }

        public void Jump()
        {
            SetState(PlayerState.JUMPING);
            position.Y -= 6;
            velocity.Y = DEFAULT_JUMP;
            IsGravity = true;
            IsGrounded = false;
            jumps++;
        }

        public override void OnGrounded()
        {
            base.OnGrounded();
            this.jumps = 0;
            if (state == PlayerState.JUMPING)
                SetState(PlayerState.RUNNING);
        }

        public void Respawn()
        {
            position.X = spawnPoint.X;
            position.Y = spawnPoint.Y - bounds.Height;

            jumps = 0;
            SetState(PlayerState.IDLE);
            IsGrounded = false;
            Alive = true;
        }

        public PlayerState GetState()
        {
            return state;
        }

        public void SetState(PlayerState state)
        {
            this.state = state;

            switch (state)
            {
                case PlayerState.RUNNING:
                    sprite.SetAnimation("run");
                    break;
                case PlayerState.CLIMBING:
                    sprite.SetAnimation("idle");
                    break;
                case PlayerState.IDLE:
                    sprite.SetAnimation("idle");
                    break;
                case PlayerState.JUMPING:
                      sprite.SetAnimation("jump");
                    break;
                default:
                    break;
            }
        }

        public void SetSpawn(float x, float y)
        {
            spawnPoint.X = x;
            spawnPoint.Y = y;
        }

    }
}
