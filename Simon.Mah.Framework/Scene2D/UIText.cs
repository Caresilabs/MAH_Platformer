using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework.Scene2D
{
    public class UIText : Actor
    {
        private Rectangle bounds;
        private Vector2 textSize;
        private Vector2 startPos;
        private Color color;

        private string text;
        private float scale;

        public UIText(string text, float x, float y, float scale = 1)
            : base(x, y, 10, 10)
        {
            this.text = text;
            this.scale = scale;
            this.color = Color.SlateBlue;
            this.startPos = new Vector2(x, y);
            this.bounds = new Rectangle((int)x - (int)(UIConfig.DEFAULT_FONT.MeasureString(text).Length() / 2 * (scale * UIConfig.DEFAULT_FONT_SCALE)), (int)y,
                (int)(UIConfig.DEFAULT_FONT.MeasureString(text).Length() * (scale * UIConfig.DEFAULT_FONT_SCALE)), (int)(UIConfig.DEFAULT_FONT.MeasureString(text).Y * (scale * UIConfig.DEFAULT_FONT_SCALE)));

            this.textSize = (UIConfig.DEFAULT_FONT.MeasureString(text));
            textSize = Vector2.Multiply(textSize, (scale * UIConfig.DEFAULT_FONT_SCALE));

            SetPosition(bounds.X - bounds.Width / 20, bounds.Y - bounds.Width / 10);
            SetSize(bounds.Width + bounds.Width / 10, bounds.Height + bounds.Width / 20);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.DrawString(UIConfig.DEFAULT_FONT, text, 
                new Vector2(GetX() + (GetBounds().Width - textSize.X) / 2, GetY() + (GetBounds().Height - textSize.Y) / 2),
                Color.White, 0, Vector2.Zero, scale * UIConfig.DEFAULT_FONT_SCALE, SpriteEffects.None, 0);

            base.Draw(batch);
        }

        public void SetText(string text)
        {
            this.text = text;
            this.bounds = new Rectangle((int)startPos.X - (int)(UIConfig.DEFAULT_FONT.MeasureString(text).Length() / 2 * scale * UIConfig.DEFAULT_FONT_SCALE),
                (int)startPos.Y, (int)(UIConfig.DEFAULT_FONT.MeasureString(text).Length() * scale * UIConfig.DEFAULT_FONT_SCALE), (int)UIConfig.DEFAULT_FONT.MeasureString(text).Y);

            SetPosition(bounds.X - 20, bounds.Y - 10);
            SetSize(bounds.Width + 30, bounds.Height + 30);
        }
    }
}
