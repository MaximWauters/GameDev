using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    abstract class Sprite
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public Animation Animation { get; set; }
        public float Scale { get; protected set; }
        public Rectangle Bounds
        {
            get
            {
                if (Texture != null) return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
                else return new Rectangle((int)Position.X, (int)Position.Y, (int)(Animation.FrameWidth * Animation.Scale), (int)(Animation.FrameHeight * Animation.Scale));
            }
        }
        
        // base voor Solid Game Object ( geen animation )
        public Sprite(Texture2D texture, SpriteBatch spriteBatch, Vector2 position, float scale = 1f)
        {
            Texture = texture;
            Position = position;
            SpriteBatch = spriteBatch;
            Scale = scale;
        }
        
        // base voor Moving Game Object
        public Sprite(Animation animation, Vector2 position)
        {
            Animation = animation;
            Position = position;
        }
        

        public virtual void Draw()
        {
            if (Texture != null) SpriteBatch.Draw(Texture, Position, Color.WhiteSmoke);
            else if (Animation != null) Animation.Draw();
        }
    }
}

