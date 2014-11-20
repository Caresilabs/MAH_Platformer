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
    public class HighscoreScreen : Screen
    {
        private int[] highscores;

        public override void Init()
        {
            this.highscores = HighscoreManager.GetHighscores();
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
            batch.Draw(Assets.GetRegion("bg1"),
                new Rectangle(0, 0, batch.GraphicsDevice.Viewport.Width, batch.GraphicsDevice.Viewport.Height),
                    Assets.GetRegion("bg1"), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);

            // Draw title
            batch.DrawString(Assets.font, "HIGHSCORES", new Vector2(GetGraphics().Viewport.Width / 2 - 120, 40), Color.YellowGreen);

            // Draw highscores
            for (int i = 0; i < highscores.Length; i++)
            {
                batch.DrawString(Assets.font, (i + 1) + ". " + highscores[i].ToString(), new Vector2(GetGraphics().Viewport.Width / 2 - 90, 150 + i * 60), Color.Black);
            }
            batch.End();
        }

        public override void Dispose()
        {
        }
    }
}
