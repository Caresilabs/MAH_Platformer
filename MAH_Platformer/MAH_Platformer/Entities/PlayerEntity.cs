using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Entities
{
    public class PlayerEntity : Entity
    {
        public const float WIDTH = 28;
        public const float HEIGHT = 52;
        public const float DEFAULT_SPEED = 5;

        private float speed;

        public PlayerEntity(TextureRegion region, float x, float y)
            : base(region, x, y, WIDTH, HEIGHT)
        {
            this.speed = DEFAULT_SPEED;
        }

        public override void Update(float delta, bool processGravity = true)
        {
            if (InputHandler.KeyDown(Keys.A))
            {
                velocity.X = -speed;
            }
            else if (InputHandler.KeyDown(Keys.D))
            {
                velocity.X = speed;
            }
            else
            {
                velocity.X = 0;
            }

            base.Update(delta, processGravity);
        }

    }
}
