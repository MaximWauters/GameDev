using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{ 
    abstract class Animation
    {
        public Texture2D SpriteStrip { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public Vector2 Position { get; set; }
        public Rectangle SourceRect { get; set; }
        public Rectangle DestinationRect { get; private set; }
        public float Scale { get; private set; }
        public int ElapsedTime { get; private set; }
        public int FrameTime { get;  set; }
        public int FrameCount { get; set; }
        public int CurrentFrame { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public int RunDirectionIndex { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public virtual bool Active { get; set; }
        public bool AnimationLoop { get; set; }
        public enum runDirection
        {
            Front,
            Right,
            Back,
            Left
        }

        public Animation(Texture2D spriteStrip, SpriteBatch spriteBatch, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, float scale, bool animationLoop)
        {
            SpriteStrip = spriteStrip;
            SpriteBatch = spriteBatch;
            Position = position;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FrameCount = frameCount;
            FrameTime = frameTime;
            Scale = scale;
            AnimationLoop = animationLoop;

            Width = (int)(FrameWidth * Scale);
            Height = (int)(FrameHeight * Scale);

            ElapsedTime = 0;
            CurrentFrame = 0;

            Active = true;

            SourceRect = new Rectangle();
            DestinationRect = new Rectangle();
        }

        protected virtual void SetSourceRect()
        {
            if (SpriteStrip != null) SourceRect = new Rectangle(CurrentFrame * (SpriteStrip.Width / FrameCount), 0, FrameWidth, FrameHeight);
        }

        private void SetDestinationRect()
        {
            DestinationRect = new Rectangle((int)Position.X, (int)Position.Y, (int)(FrameWidth * Scale), (int)(FrameHeight * Scale));
        }

        public void SetRunDirection(runDirection runDirection)
        {
            RunDirectionIndex = (int)runDirection;
        }

        private void CheckForElapsedTimeAndUpdateCurrentFrameIfNeeded(GameTime gameTime)
        {
            ElapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (ElapsedTime > FrameTime)
            {
                CurrentFrame++;
                if (CurrentFrame == FrameCount)
                {
                    CurrentFrame = 0;
                    if (AnimationLoop == false) Active = false;
                }
                ElapsedTime = 0;
            }
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            if (Active == false) return;
            Position = position;
            CheckForElapsedTimeAndUpdateCurrentFrameIfNeeded(gameTime);
            SetSourceRect();
            SetDestinationRect();
        }

        public void Draw()
        {
            if (Active)
            {
                if (SpriteStrip != null) SpriteBatch.Draw(SpriteStrip, DestinationRect, SourceRect, Color.White);
            }
        }   
    }
}
