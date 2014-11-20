using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;

namespace MAH_Platformer.Screens
{
    public class IntroScreen : Screen
    {
        private const float textTime = 2.65f;

        private int state;
        private float time;

        private String[] introTexts = {
           Simon.Mah.Framework.Game.GAME_NAME,
          "You are Bob, a robotic peace killer",
          "His quest is simple",
          "Get his @$$ to Sky Haven City",
          "Kill the King and take his throne!",
          "If you fail, you will be trapped in the Endless Pit of Death"
        };

        public override void Init()
        {
            this.state = 0;
        }

        public override void Update(float delta)
        {
            time += delta;

            if (time > textTime || InputHandler.Clicked())
            {
                time = 0;
                state++;
                if (state >= introTexts.Length)
                    SetScreen(new GameScreen());
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            GetGraphics().Clear(Color.Black);

            batch.Begin();

            // Draw story text
            batch.DrawString(Assets.font, introTexts[state],
                new Vector2(
                    GetGraphics().Viewport.Width / 2 - Assets.font.MeasureString(introTexts[state]).Length() / 2,
                    GetGraphics().Viewport.Height / 2 - 32), Color.White);

            batch.End();
        }

        public override void Dispose()
        {
        }
    }
}
