using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PongClone
{
    class Input
    {
        private KeyboardState keyboardState;
        private KeyboardState lastState;

        public bool RightUp
        {
            get { return keyboardState.IsKeyDown(Keys.Up); }
        }

        public bool RightDown
        {
            get { return keyboardState.IsKeyDown(Keys.Down); }
        }

        public bool LeftUp
        {
            get { return keyboardState.IsKeyDown(Keys.W); }
        }

        public bool LeftDown
        {
            get { return keyboardState.IsKeyDown(Keys.S); }
        }

        public bool MenuSelect
        {
            get { return keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter); }
        }

        public Input()
        {
            keyboardState = Keyboard.GetState();
            lastState = keyboardState;
        }

        public void Update()
        {
            lastState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

    }
}
