using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simon.Mah.Framework
{
    public abstract class Screen
    {
        private GraphicsDevice graphics;

        private Viewport defaultViewPort;

        private Game game;

        public abstract void Init();

        public abstract void Update(float delta);

        public abstract void Draw(SpriteBatch batch);

        public abstract void Dispose();

        public void SetScreen(Screen newScreen)
        {
            game.SetScreen(newScreen);
        }

        public void SetGame(Game game)
        {
            this.game = game;
        }

        public Game GetGame()
        {
            return game;
        }

        public GraphicsDevice GetGraphics()
        {
            return graphics;
        }

        public Viewport GetDefaultViewPort()
        {
            return defaultViewPort;
        }

        public void SetGraphics(GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }

        public void SetDefaultViewPort(Viewport viewport)
        {
            this.defaultViewPort = viewport;
        }
    }
}
