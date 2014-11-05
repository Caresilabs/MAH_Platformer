using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework
{
    public class RegionAnimation : Animation
    {
        public Point origin;

        public float stateTime;

        public int size;

        public int frames;

        public float frameDuration;

        public RegionAnimation(int x, int y, int size, int frames, float frameDuration)
        {
            this.origin = new Point(x, y);
            this.size = size;
            this.stateTime = 0;
            this.frames = frames;
            this.frameDuration = frameDuration;
        }

        Scene2D.TextureRegion Animation.GetRegion(float delta)
        {
            throw new NotImplementedException();
        }
    }
}
