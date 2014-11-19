using MAH_Platformer.Levels.Blocks;
using MAH_Platformer.Model;
using MAH_Platformer.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Screens
{
    public class GameScreen : Screen
    {
        private const int START_SCORE = 20000;

        private Camera2D camera;
        private World world;
        private WorldRenderer renderer;
       
        private float score;

        public override void Init()
        {
            Block.BLOCK_SIZE = 1280 / WorldRenderer.WIDTH;
            this.camera = new Camera2D(GetGraphics(), 1280, 720);
            this.world = new World();
            this.renderer = new WorldRenderer(this);
            this.score = START_SCORE;
        }

        public override void Update(float delta)
        {
            this.score -= delta*183;

            world.Update(delta);
            renderer.Update(delta);

            if (!world.GetLevel().GetPlayer().Alive)
                world.GetLevel().GetPlayer().Respawn();
        }

        public override void Draw(SpriteBatch batch)
        {
            renderer.Render(batch);
        }

        public override void Dispose()
        {
        }

        public Camera2D GetCamera()
        {
            return camera;
        }

        public World GetWorld()
        {
            return world;
        }

        public int GetScore()
        {
            return (int)score;
        }
    }
}
