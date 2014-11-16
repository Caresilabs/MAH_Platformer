using MAH_Platformer.Levels.Blocks;
using MAH_Platformer.Model;
using MAH_Platformer.Screens;
using Microsoft.Xna.Framework;
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
            LerpCamera(world.GetLevel().GetPlayer().GetBounds().X - camera.GetWidth() / 2, world.GetLevel().GetPlayer().GetBounds().Y - camera.GetHeight() / 2, delta);
        }

        public void LerpCamera(float x, float y, float delta)
        {
            camera.SetPosition(camera.GetPosition().X + (x - camera.GetPosition().X) * delta * 7, camera.GetPosition().Y + ( y - camera.GetPosition().Y) * delta * 7);
        }

        public void Render(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.BackToFront,
                     BlendState.AlphaBlend,
                     SamplerState.PointClamp,
                     null, null, null,
                     camera.GetMatrix());

            
            DrawBackground(batch);

            DrawBlocks(batch);

            DrawEntites(batch);

            batch.End();
        }

        private void DrawBackground(SpriteBatch batch)
        {
            batch.Draw(Assets.GetRegion("bg1"), new Rectangle((int)camera.GetPosition().X, (int)camera.GetPosition().Y, (int)camera.GetWidth() + 10, (int)camera.GetHeight() + 10), Assets.GetRegion("bg1"), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
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
                }
            }
        }

        private void DrawEntites(SpriteBatch batch)
        {
            foreach (var entity in world.GetLevel().GetEntities()) 
            {
                entity.Draw(batch);
            }
        }
    }
}
