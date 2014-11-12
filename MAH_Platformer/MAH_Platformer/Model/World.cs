using MAH_Platformer.Levels;
using MAH_Platformer.Levels.Blocks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Model
{
    public class World
    {
        public static readonly Vector2 GRAVITY = new Vector2(0, 20);

        private Level level;

        public World(int level = 1)
        {
            this.level = new Level();
            this.level.InitLevel(level);
        }

        public void Update(float delta)
        {
            level.Update(delta);
        }

        public Level GetLevel()
        {
            return level;
        }
    }
}
