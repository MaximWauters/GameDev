using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{/*
    class Animation
    {
        private List<AnimationFrame> frames;
        public AnimationFrame CurrentFrame { get; set; }
       // ContentManager content;

        //private Bediening b = new BedieningPijltjes();
        public int AantalBewegingenPerSeconde { get; set; }

        private int counter = 0;

        private double x = 0;
        public double offset { get; set; }

        private int _totalWidth = 0;

        public Animation()
        {
            frames = new List<AnimationFrame>();
            AantalBewegingenPerSeconde = 1;
        }
        public void AddFrame(Rectangle rectangle)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
                //Duration = duration
            };

            frames.Add(newFrame);
            CurrentFrame = frames[0];
            offset = CurrentFrame.SourceRectangle.Width;
            foreach (AnimationFrame f in frames)
                _totalWidth += f.SourceRectangle.Width;
        }

        public void Update(GameTime gameTime)
        {
            double temp = CurrentFrame.SourceRectangle.Width * ((double)gameTime.ElapsedGameTime.Milliseconds / 1000);

            x += temp;
            if (x >= CurrentFrame.SourceRectangle.Width / AantalBewegingenPerSeconde)
            {
                Console.WriteLine(x);
                x = 0;
                counter++;
                if (counter >= frames.Count)
                    counter = 0;
                CurrentFrame = frames[counter];
                offset += CurrentFrame.SourceRectangle.Width;
            }
            if (offset >= _totalWidth)
                offset = 0;
        }
    }
    */
    class Animation
    {
        #region Properties
        public Texture2D SpriteStrip { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public Texture2D[] SpriteArray { get; private set; }
        public float Scale { get; private set; }
        public int ElapsedTime { get; private set; }
        public int FrameTime { get;  set; }
        public int FrameCount { get; set; }
        public int CurrentFrame { get; private set; }
        public Color Color { get; private set; }
        public Rectangle SourceRect { get; set; }
        public Rectangle DestinationRect { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public virtual bool Active { get; set; }
        public bool AnimationLoop { get; set; }
        public Vector2 Position { get; set; }
        public int RunDirectionIndex { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public enum runDirection
        {
            Front,
            Right,
            Back,
            Left
        }
        #endregion

        public Animation(Texture2D spriteStrip, SpriteBatch spriteBatch, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool animationLoop)
        {
            SpriteStrip = spriteStrip;
            SpriteBatch = spriteBatch;
            Position = position;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FrameCount = frameCount;
            FrameTime = frameTime;
            Color = color;
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

        public Animation(Texture2D[] spriteArray, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool animationLoop)
        {
            SpriteArray = spriteArray;
            Position = position;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FrameCount = frameCount;
            FrameTime = frameTime;
            Color = color;
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

        #region Public Methods

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
                if (SpriteArray != null) SpriteBatch.Draw(SpriteArray[CurrentFrame], DestinationRect, SourceRect, Color.Red);
            }
        }

        public void SetRunDirection(runDirection runDirection)
        {
            RunDirectionIndex = (int)runDirection;
        }
        #endregion

        #region Private/Protected Methods
        private void CheckForElapsedTimeAndUpdateCurrentFrameIfNeeded(GameTime gameTime)
        {
            ElapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (ElapsedTime > FrameTime)
            {
                CurrentFrame++;
                if (CurrentFrame == FrameCount)
                {
                    CurrentFrame = 0;
                    if (AnimationLoop == false)
                        Active = false;
                }
                ElapsedTime = 0;
            }
        }

        protected virtual void SetSourceRect()
        {
            if (SpriteStrip != null)
            {
                SourceRect = new Rectangle(CurrentFrame * (SpriteStrip.Width / FrameCount), 0, FrameWidth, FrameHeight);
            }
            if (SpriteArray != null) SourceRect = new Rectangle(0, 0, SpriteArray[CurrentFrame].Width, SpriteArray[CurrentFrame].Height);
        }

        private void SetDestinationRect()
        {
            DestinationRect = new Rectangle((int)Position.X,
                (int)Position.Y,
                (int)(FrameWidth * Scale),
                (int)(FrameHeight * Scale));
        }
        #endregion
    }
}
