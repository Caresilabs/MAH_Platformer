using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;

namespace MAH_Platformer.Screens
{
    /**
     * A game screen that manages the world, renderer and input and put them togheter in a convenient way
     */
    public class WinScreen : Screen
    {
        private int score;

        public WinScreen(int score)
        {
            this.score = score;
        }

        public override void Init()
        {
        }

        public override void Update(float delta)
        {
            if (InputHandler.KeyReleased(Keys.M))
                SetScreen(new MainMenuScreen());
        }

        public override void Draw(SpriteBatch batch)
        {
            GetGraphics().Clear(Color.Black);

            batch.Begin();

            // draw bg
            //  batch.Draw(Assets.GetRegion("bg1"),
            //    new Rectangle(0, 0, batch.GraphicsDevice.Viewport.Width, batch.GraphicsDevice.Viewport.Height),
            //      Assets.GetRegion("bg1"), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);

            // Draw title
            DrawCenterString(batch, GetGraphics().Viewport.Width, "You slayed King Wasp!!", 180, Color.YellowGreen, 1.4f);
            DrawCenterString(batch, GetGraphics().Viewport.Width, "Congratulations!", 250, Color.YellowGreen);

            DrawCenterString(batch, GetGraphics().Viewport.Width,
                "The kingdom gave you the Score: " + score, .5f * GetGraphics().Viewport.Height + 10, Color.White);

            batch.End();
        }

        public static void DrawCenterString(SpriteBatch batch, float width, string text, float y, Color color, float scale = 1)
        {
            batch.DrawString(Assets.font, text,
                    new Vector2(
                        width / 2 - ((Assets.font.MeasureString(text).Length() * scale) / 2), y),
                         color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public static void DrawCenterString(SpriteBatch batch, float width, string text, float y, float scale = 1)
        {
            DrawCenterString(batch, width, text, y, Color.White, scale);
        }

        public override void Dispose()
        {

        }
    }
}
