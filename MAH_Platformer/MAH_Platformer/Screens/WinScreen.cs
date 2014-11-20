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
        public override void Init()
        {
        }

        public override void Update(float delta)
        {
            if (InputHandler.Clicked())
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
            batch.DrawString(Assets.font, "WOOOW DUUDE!!\n  congratulations!", new Vector2(GetGraphics().Viewport.Width / 2 - 170, 80), Color.YellowGreen);

            batch.DrawString(Assets.font, "You are Winner!", new Vector2(GetGraphics().Viewport.Width / 2 - 150, GetGraphics().Viewport.Height / 2), Color.Black);

            batch.End();
        }

        public override void Dispose()
        {

        }
    }
}
