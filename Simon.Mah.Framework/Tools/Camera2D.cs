using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework.Tools
{
    /**
     * Camera makes the live a lot easier by translating a matrix and then tell the spritebatch to translate all objects by the values
     * set in this class
     */
    public class Camera2D
    {
        private GraphicsDevice graphicsDevice;
        private Matrix transform;

        private Vector2 position;
        private Vector2 zoom;
        private Vector2 defaultViewPort;

        private float rotation;

        public Camera2D(GraphicsDevice device, float width, float height)
        {
            this.graphicsDevice = device;
            this.defaultViewPort = new Vector2(width, height);
            this.rotation = 0f;
            this.zoom = new Vector2(1, 1);
            this.position = Vector2.Zero;
            this.Update();
        }

        public Camera2D Update()
        {
            zoom.X = graphicsDevice.Viewport.Width / (float)defaultViewPort.X;
            zoom.Y = graphicsDevice.Viewport.Height / (float)defaultViewPort.Y;
            
            return this;
        }

        // Gets the matrix used by the spritebatch
        public Matrix GetMatrix()
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(new Vector3(zoom.X, zoom.Y, 1));// *
                //Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
           
            return transform;
        }

        public Vector2 Unproject(float x, float y)
        {
            return Vector2.Transform(new Vector2(x, y), Matrix.Invert(transform));
        }

        public Vector2 GetZoom()
        {
            return zoom;
        }

        public void SetZoom(float width, float height)
        {
            this.zoom.X = width;
            this.zoom.Y = height;
        }

        public float GetRotation()
        {
            return rotation;
        }

        public void SetRotation(float rot)
        {
            rotation = rot;
        }

        public void Move(Vector2 amount)
        {
            position += amount;
        }

        public float GetWidth()
        {
            return defaultViewPort.X;
        }

        public float GetHeight()
        {
            return defaultViewPort.Y;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public void SetPosition(Vector2 pos)
        {
            position.X = pos.X;
            position.Y = pos.Y;
        }
    }
}
