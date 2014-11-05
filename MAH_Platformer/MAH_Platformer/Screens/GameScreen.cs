using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Screens
{
    public class GameScreen : Screen
    {
        private Camera2D camera;

        private Sprite sprite;
        public override void Init()
        {
            this.camera = new Camera2D(GetGraphics(), 720, 480);
            this.sprite = new Sprite(Assets.GetRegion("pixel"), 720 / 2, 240, 15, 15);

            this.sprite.AddAnimation("idle", new FrameAnimation(Assets.items, 0, 0, 16, 3, .18f)).SetAnimation("idle");
        }

        public override void Update(float delta)
        {
            sprite.Update(delta);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.BackToFront,
                      BlendState.AlphaBlend,
                      SamplerState.LinearClamp,
                      null, null, null,
                      camera.GetMatrix());


           sprite.Draw(batch);

            batch.Draw(Assets.GetRegion("pixel"), new Rectangle(720 / 2, 240, 10, 10), Assets.GetRegion("pixel"), Color.Magenta);
            batch.End();
        }

        public override void Dispose()
        {
        }
    }
}
