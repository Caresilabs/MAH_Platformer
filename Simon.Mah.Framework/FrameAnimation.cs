using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework
{
    public class FrameAnimation : Animation
    {
        private Texture2D texture;
        private Point origin;
        private float stateTime;
        private int size;
        private int frames;
        private float frameDuration;

        public FrameAnimation(Texture2D tex, int x, int y, int size, int frames, float frameDuration)
        {
            this.origin = new Point(x, y);
            this.size = size;
            this.stateTime = 0;
            this.frames = frames;
            this.frameDuration = frameDuration;
            this.texture = tex;
        }

        public override bool HasNext()
        {
            return lastFrame != currentFrame;
        }

        public override void Update(float delta)
        {
            lastFrame = currentFrame;
            stateTime += delta;
            currentFrame = (int)(stateTime / (float)frameDuration) % frames;
        }

        public override TextureRegion GetRegion()
        {
            return new TextureRegion(
           texture, origin.X + (size * currentFrame), origin.Y, size, size);
        }
    }
}
