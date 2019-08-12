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
        public bool left { get; set; }
        public bool right { get; set; }
        public bool space { get; set; }
        public KeyboardState keyboardOldState { get; set; }
        public abstract void Update();
    }
}
