using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Simon.Mah.Framework;
using MAH_Platformer.Screens;

namespace MAH_Platformer
{
    public class Start : Simon.Mah.Framework.Game
    {
        private float aspectRatio;

        public Start() : base()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;

            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            GAME_NAME = "Bob's Quest for Sky Haven City";
            Window.Title = GAME_NAME + " by [Simon Bothen]"; //  set title to our game name
        }

        protected override void LoadContent()
        {
            Assets.Load(Content);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Assets.Unload();

            base.UnloadContent();
        }

        public override Screen GetStartScreen()
        {
            return new GameScreen();
        }
    }
}
