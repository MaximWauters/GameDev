using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class KeyboardKeys : UserControls
    {
       // public KeyboardState OldKeyboardState { get;  set; }
        public KeyboardKeys()
        {

        }

        public override void Update()
        {
            StateKey = Keyboard.GetState();
            OldKeyboardState = Keyboard.GetState();

            if (StateKey.IsKeyDown(Keys.Up))
            {
                Up = true;
                Console.WriteLine("Naar bove");
            }
            if (StateKey.IsKeyUp(Keys.Up))
            {
                Up = false;
            }
            if (StateKey.IsKeyDown(Keys.Down))
            {
                Console.WriteLine("Naar benede");
                Down = true;
            }
            if (StateKey.IsKeyUp(Keys.Down))
            {
                Down = false;
            }
            if (OldKeyboardState.IsKeyDown(Keys.Up))
            {
                Up = true;
                Console.WriteLine("Naar bove");
            }
            if (OldKeyboardState.IsKeyUp(Keys.Up))
            {
                Up = false;
            }
            if (OldKeyboardState.IsKeyDown(Keys.Down))
            {
                Console.WriteLine("Naar benede");
                Down = true;
            }
            if (OldKeyboardState.IsKeyUp(Keys.Down))
            {
                Down = false;
            }

            if (StateKey.IsKeyDown(Keys.Left))
            {
                Left = true;
            }
            if (StateKey.IsKeyUp(Keys.Left))
            {
                Left = false;
            }

            if (StateKey.IsKeyDown(Keys.Right))
            {
                Right = true;
            }
            if (StateKey.IsKeyUp(Keys.Right))
            {
                Right = false;
            }

            if (StateKey.IsKeyDown(Keys.Enter))
            {
                Enter = true;
            }
            if (StateKey.IsKeyUp(Keys.Enter))
            {
                Enter = false;
            }

            if (StateKey.IsKeyDown(Keys.Space))
            {
                Space = true;
            }
            if (StateKey.IsKeyUp(Keys.Space))
            {
                Space = false;
            }
        }
    }
}
