using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class SolidGameObject : Sprite
    {
        public new Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }
        
        public SolidGameObject(Texture2D texture, Vector2 position, SpriteBatch spriteBatch) : base(texture, spriteBatch, position)
        { }
    }
}
