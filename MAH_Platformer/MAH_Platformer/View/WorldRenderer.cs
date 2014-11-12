using MAH_Platformer.Levels.Blocks;
using MAH_Platformer.Model;
using MAH_Platformer.Screens;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.View
{
    public class WorldRenderer
    {
        public static float WIDTH = 20; 

        private World world;
        private Camera2D camera;

        public WorldRenderer(GameScreen game)
        {
            this.world = game.GetWorld();
            this.camera = game.GetCamera();
        }

        public void Update(float delta)
        {
        }

        public void Render(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.BackToFront,
                     BlendState.AlphaBlend,
                     SamplerState.LinearClamp,
                     null, null, null,
                     camera.GetMatrix());

            DrawBlocks(batch);

            batch.End();
        }

        private void DrawBlocks(SpriteBatch batch)
        {
            Block[,] blocks = world.GetLevel().GetBlocks();
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                for (int i = 0; i < blocks.GetLength(0); i++)
                {
                    Block block = blocks[i, j];
                    block.Draw(batch);
                    DrawEntites(block, batch);
                }
            }
        }

        private void DrawEntites(Block block, SpriteBatch batch)
        {
            foreach (var entity in block.Entities)
            {
                entity.Draw(batch);
            }
        }
    }
}
