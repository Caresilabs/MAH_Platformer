using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework.Scene2D
{
    public class UISpriteButton : Actor
    {
        private Color color;
        private TextureRegion region;

        private float scale;

        public UISpriteButton(TextureRegion region, float x, float y, float width, float height, float scale = 1)
        {
            this.scale = scale;
            this.color = Color.White;
            this.region = region;

            SetRegion(UIConfig.DEFAULT_BUTTON);
            //SetPosition(x - ((Rectangle)region).Width + width / 2, y - ((Rectangle)region).Height * scale / 2);
            //SetSize(((Rectangle)region).Width * scale, ((Rectangle)region).Height * scale);
            SetPosition(x + width / 2, y - height / 2);
            SetSize(width, height);
        }

        public override void TouchDown(Vector2 mouse)
        {
            base.TouchDown(mouse);
            color = Color.Green;
        }

        public override void TouchUp(Vector2 mouse)
        {
            base.TouchUp(mouse);
            color = Color.White;
        }

        public override void TouchLeave(Vector2 mouse)
        {
            base.TouchLeave(mouse);
            color = Color.White;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(GetRegion(), GetBounds(), GetRegion(), Color.Beige, 0, Vector2.Zero, SpriteEffects.None, .06f);

            batch.Draw(region, new Vector2(
                    GetBounds().X + GetBounds().Width / 2 - (((Rectangle)region).Width / 2),
                    GetBounds().Y + GetBounds().Height / 2 - (((Rectangle)region).Height / 2)),
                    region, color, 0, Vector2.Zero, scale, SpriteEffects.None, .05f);

            base.Draw(batch);
        }

        public void SetSprite(TextureRegion region)
        {
            this.region = region;
        }
    }
}
