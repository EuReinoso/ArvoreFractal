using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArvoreFractal.MgEngine.Input
{
    public class InputManager
    {
        private KeyboardState _keyState;
        private KeyboardState _lastKeyState;

        public InputManager() 
        { 
        }
        public void Update(KeyboardState keyState)
        {
            _lastKeyState = _keyState;
            _keyState = keyState;
        }

        public bool KeyDown(Keys key)
        {
            return (_keyState.IsKeyDown(key) && _lastKeyState.IsKeyUp(key));
        }

        public bool KeyUp(Keys key)
        {
            return (_keyState.IsKeyUp(key) && _lastKeyState.IsKeyDown(key));
        }

        public bool IsKeyDown(Keys key)
        {
            return _keyState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return _keyState.IsKeyUp(key);
        }
    }
}
