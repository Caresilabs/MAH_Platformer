using MAH_Platformer.Entities;
using Microsoft.Xna.Framework;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Levels.Blocks
{
    public class GoalBlock : Block
    {
        public GoalBlock(TextureRegion region, int x, int y)
            : base(region, x, y)
        {
            this.BlocksMotion = false;
        }

        public override void Collide(Entity entity)
        {
            base.Enter(entity);

            if (entity is PlayerEntity)
                Level.NextLevel();

        }
    }
}
