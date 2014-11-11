using MAH_Platformer.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Model
{
    public class World
    {
        private Level level;

        public World(int level = 1)
        {
            this.level = new Level();
            this.level.InitLevel(level);
        }
    }
}
