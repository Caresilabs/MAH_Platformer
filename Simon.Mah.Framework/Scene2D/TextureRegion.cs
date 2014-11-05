using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework.Scene2D
{
    public class TextureRegion
    {
        private Texture2D texture;
        private Rectangle region;

        public TextureRegion(Texture2D texture, int x, int y, int width, int height)
        {
            this.texture = texture;
            this.region = new Rectangle(x, y, width, height);
        }

        public Rectangle GetSource()
        {
            return region;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public static implicit operator Rectangle(TextureRegion obj)
        {
            return obj.region;
        }

        public static implicit operator Texture2D(TextureRegion obj)
        {
            return obj.texture;
        }
    }
}
