using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class HeroAnimation : Animation
    {
        public int AnimationRows { get; private set; }
        public int[] XOffset { get; private set; }
        public int YOffset { get; private set; }

        public HeroAnimation(Texture2D spriteStrip, SpriteBatch spriteBatch, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool animationLoop, int animationRows, int[] xOffset, int yOffset) : base(spriteStrip, spriteBatch, position, frameWidth, frameHeight, frameCount, frameTime, scale, animationLoop)
        {
            AnimationRows = animationRows;
            XOffset = xOffset;
            YOffset = yOffset;
        }

        protected override void SetSourceRect()
        {
            SourceRect = new Rectangle((CurrentFrame * (SpriteStrip.Width / FrameCount)) + XOffset[RunDirectionIndex], RunDirectionIndex * (SpriteStrip.Height / AnimationRows) + YOffset, FrameWidth, FrameHeight);
        }
    }
}
