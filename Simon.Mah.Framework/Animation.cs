using Microsoft.Xna.Framework;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework
{
    public abstract class Animation
    {
        protected int currentFrame;
        protected int lastFrame;

        public abstract void Update(float delta);

        public abstract TextureRegion GetRegion();

        public abstract bool HasNext();
    }
}
