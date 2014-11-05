using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework
{
    public class Sprite
    {
        public TextureRegion Region { get; private set; }

        public Vector2 Position { get; set; }

        public Vector2 Scale { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 DrawOffset { get; set; }

        public Color Color { get; set; }

        public SpriteEffects Effect { get; set; }

        public float ZIndex { get; set; }

        public float Rotation { get; set; }

        private Vector2 SizeScale { get; set; }

        private Vector2 size;
        public Vector2 Size
        {
            get { return size; }
            set { size = value; UpdateSizeScale(); }
        }

        private Animations Animations { get; set; }

        public Sprite(TextureRegion region, float x, float y, float width, float height) {
            this.ZIndex = 0;
            this.Rotation = 0;
            this.Region = region;
            this.Color = Color.White;
            this.Scale = new Vector2(1, 1);
            this.Size = new Vector2(width, height);
            this.DrawOffset = new Vector2();
            this.Effect = SpriteEffects.None;
            this.Position = new Vector2(x, y);
            this.Animations = new Animations();
            this.Origin = new Vector2(region.GetSource().Width / 2, region.GetSource().Height / 2);
            this.DrawOffset = new Vector2(-Size.X/2, -Size.Y/2);

            UpdateSizeScale();
        }

        public void Update(float delta)
        {
            Animations.Update(delta);
            if (Animations.HasNext())
            {
                SetRegion(Animations.GetRegion());
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Region, Position + DrawOffset, Region, Color, Rotation, Origin, Scale * SizeScale, Effect, ZIndex);
        }

        private void UpdateSizeScale()
        {
            this.SizeScale = new Vector2(Size.X / Region.GetSource().Width, Size.Y / Region.GetSource().Height);
        }

        // ==== HELPERS ==== //

        public Sprite AddAnimation(string name, Animation anim)
        {
            Animations.AddAnimation(name, anim);
            return this;
        }

        public Sprite SetAnimation(string name)
        {
            Animations.SetAnimation(name);
            return this;
        }

        public Sprite SetScale(float x, float y)
        {
            this.Scale = new Vector2(x, y);
            return this;
        }

        public Sprite SetSize(float width, float height)
        {
            this.Size = new Vector2(width, height);
            UpdateSizeScale();
            return this;
        }

        public Sprite SetPosition(float x, float y)
        {
            this.Position = new Vector2(x, y);
            return this;
        }

        public Sprite SetRegion(TextureRegion region)
        {
            this.Region = region;
            UpdateSizeScale();
            return this;
        }
    }
}
