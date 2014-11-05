using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework.Scene2D
{
    public class InputHandler
    {
        private static KeyboardState oldKeyState;
        private static MouseState oldMouseState;

        public static void Update(float delta)
        {
            oldKeyState = Keyboard.GetState();
            oldMouseState = Mouse.GetState();
        }

        public static bool KeyDown(Keys key)
        {
            if (oldKeyState.IsKeyUp(key) && Keyboard.GetState().IsKeyDown(key))
                return true;
            else
                return false;
        }

        public static bool KeyReleased(Keys key)
        {
            if (Keyboard.GetState().IsKeyUp(key) && oldKeyState.IsKeyDown(key))
                return true;
            else
                return false;
        }

        public static bool Clicked()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public static bool RightClicked()
        {
            if (Mouse.GetState().RightButton == ButtonState.Released && oldMouseState.RightButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }
    }
}
