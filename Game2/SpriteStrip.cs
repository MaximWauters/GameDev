using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
        public struct SpriteStrip
        {
            #region Properties
            public Texture2D Texture { get; private set; }
            public int OffSetPosX { get; private set; }
            public int OffSetPosY { get; private set; }
            public int FrameWidth { get; private set; }
            public int FrameHeight { get; private set; }
            public static SpriteStrip CurrentSpriteStrip { get; private set; }
            #endregion

            public SpriteStrip(Texture2D texture)
            {
                Texture = texture;
                OffSetPosX = 0;
                OffSetPosY = 0;
                FrameWidth = 0;
                FrameHeight = 0;
                CurrentSpriteStrip = this;
            }
        }
}
