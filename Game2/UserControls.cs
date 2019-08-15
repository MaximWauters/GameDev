using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    abstract class UserControls
    {
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Space { get; set; }
        public bool Enter { get; set; }
        public KeyboardState OldKeyboardState { get; set; }
        public KeyboardState StateKey { get; set; }
        public abstract void Update();
    }
}
