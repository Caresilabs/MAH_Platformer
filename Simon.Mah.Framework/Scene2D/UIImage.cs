using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework.Scene2D
{
    public class UIImage : Actor
    {
        private Rectangle bounds;
        private Vector2 startPos;
        private Color color;

        private float scale;

        public UIImage(TextureRegion region, float x, float y, float scale = 1)
            : base(x, y, 10, 10)
        {
            this.scale = scale;
            this.color = Color.White;
            this.startPos = new Vector2(x, y);
            this.bounds = new Rectangle((int)(x - ((Rectangle)region).Width/2*scale), (int)(y - ((Rectangle)region).Height/2*scale),
                (int)(((Rectangle)region).Width*scale), (int)(((Rectangle)region).Height*scale));

            SetRegion(region);
            SetPosition(bounds.X - bounds.Width / 20, bounds.Y - bounds.Width / 10);
            SetSize(bounds.Width + bounds.Width / 10, bounds.Height + bounds.Width / 20);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(GetRegion(), GetBounds(), GetRegion(), color, 0, Vector2.Zero, SpriteEffects.None, .1f);
            base.Draw(batch);
        }
    }
}
