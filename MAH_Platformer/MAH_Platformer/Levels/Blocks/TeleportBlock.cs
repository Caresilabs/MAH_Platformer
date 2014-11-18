using MAH_Platformer.Entities;
using Microsoft.Xna.Framework;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Levels.Blocks
{
    public class TeleportBlock : Block
    {
        public TeleportBlock(TextureRegion region, int x, int y)
            : base(region, x, y)
        {
            this.sprite.AddAnimation("idle", new FrameAnimation(Assets.items, 0, 96, 32, 2, MathUtils.random(.2f, .4f))).SetAnimation("idle");
        }

        public override void Enter(Entity entity)
        {
            base.Enter(entity);

            Block block;
            if (Id % 2 == 0)
                block = Level.GetBlockById(Id + 1);
            else
                block = Level.GetBlockById(Id - 1);

            entity.SetPosition(block.GetPosition() - new Vector2(0, entity.GetBounds().Height));
            float vy = Math.Min(-300, entity.GetVelocity().Y * -.89f);
            entity.SetVelocity(0, vy);
        }

    }
}
