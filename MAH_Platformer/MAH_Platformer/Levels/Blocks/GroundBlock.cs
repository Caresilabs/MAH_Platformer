using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Levels.Blocks
{
    public class GroundBlock : SolidBlock
    {
        public GroundBlock(TextureRegion region, int x, int y)
            : base(region, x, y)
        {

        }
    }
}
