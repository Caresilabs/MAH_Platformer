using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework.Scene2D
{
    public class UIWindow : Actor
    {
        public UIWindow(float width, float height)
        {
            SetSize(width, height);
            SetRegion(UIConfig.DEFAULT_BUTTON);
        }

        public override void Init()
        {
            base.Init();

            SetPosition(GetScene().GetWidth()/2 - GetBounds().Width / 2, GetScene().GetHeight()/2 - GetBounds().Height / 2);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(GetRegion(), GetBounds(), GetRegion(), Color.White, 0, Vector2.Zero, SpriteEffects.None, .1f);

            base.Draw(batch);
        }
    }
}
