using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Levels.Blocks
{
    public class SolidBlock : Block
    {
        public SolidBlock(TextureRegion region, int x, int y)
            : base(region, x, y)
        {
            this.BlocksMotion = true;
        }

        public override float GetFriction(Entities.Entity entity)
        {
            return .88f;
        }
    }
}
