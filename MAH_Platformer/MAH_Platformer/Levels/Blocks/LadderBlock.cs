using MAH_Platformer.Entities;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Levels.Blocks
{
    public class LadderBlock : Block
    {
        public LadderBlock(TextureRegion region, int x, int y)
            : base(region, x, y)
        {
        }

        public override float GetFriction(Entities.Entity entity)
        {
            if (entity is PlayerEntity)
            {
                if (((PlayerEntity)entity).GetState() == PlayerEntity.PlayerState.CLIMBING)
                {
                    return .5f;
                }
            }
            return 1f;
        }

    }
}
