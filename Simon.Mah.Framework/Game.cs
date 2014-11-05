using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework
{
    public abstract class Game : Microsoft.Xna.Framework.Game
    {
         public const string GAME_NAME = "Hello XNA";

        protected static GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected Screen currentScreen;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            // init startup screen
            SetScreen(GetStartScreen());
        }

        protected override void UnloadContent()
        {
            //Assets.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // get second between last frame and current frame, used for fair physics manipulation and not based on frames
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // then update the screen
            currentScreen.Update(delta);

            // update input
            InputHandler.Update(delta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw screen
            currentScreen.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public void SetScreen(Screen newScreen)
        {
            if (newScreen == null) return;

            // Dispose old screen
            if (currentScreen != null)
                currentScreen.Dispose();

            // init new screen
            currentScreen = newScreen;
            newScreen.SetGame(this);
            newScreen.SetGraphics(GraphicsDevice);
            currentScreen.Init();
        }

        public abstract Screen GetStartScreen();

    }
}
