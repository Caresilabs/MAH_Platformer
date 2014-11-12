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
        private Camera2D camera;
        private World world;
        private WorldRenderer renderer;

        public override void Init()
        {
            Block.BLOCK_SIZE = 720 / WorldRenderer.WIDTH;
            this.camera = new Camera2D(GetGraphics(), 720, 480);
            this.world = new World();
            this.renderer = new WorldRenderer(this);
        }

        public override void Update(float delta)
        {
            world.Update(delta);
            renderer.Update(delta);
        }

        public override void Draw(SpriteBatch batch)
        {
            renderer.Render(batch);
            batch.Begin(SpriteSortMode.BackToFront,
                      BlendState.AlphaBlend,
                      SamplerState.LinearClamp,
                      null, null, null,
                      camera.GetMatrix());
            batch.Draw(Assets.GetRegion("pixel"), new Rectangle(1, 1, 10, 10), Assets.GetRegion("pixel"), Color.Magenta);

            batch.End();
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
    }
}
