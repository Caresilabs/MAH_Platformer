using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;

namespace MAH_Platformer.Screens
{
    /**
     * A game screen that manages the world, renderer and input and put them togheter in a convenient way
     */
    public class MainMenuScreen : Screen, EventListener
    {
        private Scene scene;

        public override void Init()
        {
            this.scene = new Scene(new Camera2D(GetGraphics(),1280, 720), this);

            UIButton button = new UIButton("Play!", scene.GetWidth()/2, 320, 2.5f);
            scene.Add("start", button);

            UIButton highscores = new UIButton("Highscores", scene.GetWidth() / 2, 500, 2.5f);
            scene.Add("highscores", highscores);

            //UIButton exit = new UIButton("Level Editor", scene.GetWidth() / 2, 550, 2.5f);
            //scene.Add("editor", exit);

            UIImage title = new UIImage(Assets.GetRegion("title"), scene.GetWidth() / 2, 140, 1.5f);
            scene.Add("title", title);
        }


        public override void Update(float delta)
        {
            scene.Update(delta);
        }

        public override void Draw(SpriteBatch batch)
        {
            GetGraphics().Clear(Color.Black);

            batch.Begin();
            batch.Draw(Assets.GetRegion("bg1"), 
                new Rectangle(0, 0, batch.GraphicsDevice.Viewport.Width, batch.GraphicsDevice.Viewport.Height),
                    Assets.GetRegion("bg1"), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            batch.End();

            scene.Draw(batch);
        }

        public override void Dispose()
        {

        }

        public void EventCalled(Events e, Actor actor)
        {
            if (e == Events.TouchUp)
            {
                if (actor.name == "start")
                {
                    SetScreen(new GameScreen());
                }
            }
        }
    }
}
