using MAH_Platformer.Levels;
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
        private GameScreen game;

        public WorldRenderer(GameScreen game)
        {
            this.world = game.GetWorld();
            this.camera = game.GetCamera();
            this.game = game;
        }

        public void Update(float delta)
        {
            LerpCamera(world.GetLevel().GetPlayer().GetBounds().X - camera.GetWidth() / 2, world.GetLevel().GetPlayer().GetBounds().Y - camera.GetHeight() / 2, delta);
        }

        public void LerpCamera(float x, float y, float delta)
        {
            x = MathHelper.Clamp(x, Block.BLOCK_SIZE/2, Level.WIDTH - camera.GetWidth() - Block.BLOCK_SIZE/2);
            camera.SetPosition(camera.GetPosition().X + (x - camera.GetPosition().X) * delta * 7, camera.GetPosition().Y + (y - camera.GetPosition().Y) * delta * 7);
        }

        public void Render(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.BackToFront,
                     BlendState.AlphaBlend,
                     SamplerState.PointWrap,
                     null, null, null,
                     camera.GetMatrix());


            DrawBackground(batch);

            DrawBlocks(batch);

            DrawEntites(batch);

            DrawUI(batch);

            batch.End();
        }

        private void DrawUI(SpriteBatch batch)
        {
            batch.DrawString(Assets.font, "Score: " + game.GetScore(), new Vector2(camera.GetPosition().X + 10, camera.GetPosition().Y + 10), Color.White, 0, Vector2.Zero, .5f, SpriteEffects.None, 0);
            batch.DrawString(Assets.font, "Lives: " + game.GetLives(), new Vector2(camera.GetPosition().X + 10, camera.GetPosition().Y + 40), Color.White, 0, Vector2.Zero, .4f, SpriteEffects.None, 0);
        }

        private void DrawBackground(SpriteBatch batch)
        {
            // BG
            batch.Draw(Assets.GetRegion("bg1"), new Rectangle((int)camera.GetPosition().X, (int)camera.GetPosition().Y - 6, (int)camera.GetWidth() + 10, (int)camera.GetHeight() + 10), Assets.GetRegion("bg1"), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);

            //bg 2
            var source = new Rectangle((int)(camera.GetPosition().X * .1f), 0, Assets.GetRegion("bg1").GetTexture().Width, (int)(Assets.GetRegion("bg1").GetTexture().Height/1.6f));

            batch.Draw(Assets.bg2, new Rectangle((int)camera.GetPosition().X, (int)camera.GetPosition().Y - 6, (int)camera.GetWidth() + 10, (int)camera.GetHeight() + 10),
                source, Color.White, 0, Vector2.Zero, SpriteEffects.None, .9f);

            // Clouds
            source = new Rectangle((int)(camera.GetPosition().X * .18f), 0, Assets.bg3.Width, (int)(Assets.bg3.Height / 1.6f));
            batch.Draw(Assets.bg3, new Rectangle((int)camera.GetPosition().X, (int)camera.GetPosition().Y - 6, (int)camera.GetWidth() + 10, (int)camera.GetHeight() + 10),
                source, Color.White, 0, Vector2.Zero, SpriteEffects.None, .87f);
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
