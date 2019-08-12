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
        public override void Update()
        {
            KeyboardState stateKey = Keyboard.GetState();

            if (stateKey.IsKeyDown(Keys.Left))
            {
                left = true;
            }
            if (stateKey.IsKeyUp(Keys.Left))
            {
                left = false;
            }

            if (stateKey.IsKeyDown(Keys.Right))
            {
                right = true;
            }
            if (stateKey.IsKeyUp(Keys.Right))
            {
                right = false;
            }

            if (stateKey.IsKeyDown(Keys.Space))
            {
                space = true;
            }
            if (stateKey.IsKeyUp(Keys.Space))
            {
                space = false;
            }
        }
    }
}
