using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class Coin : MovingGameObject
    {
        public Coin(Animation anim, Vector2 position, SpriteBatch spriteBatch, float speed = 300) : base(anim, position)
        {
            Position = position;
        }

        public void Update(GameTime gameTime, Hero player)
        {
            if (Animation != null)
            {
                Animation.Position = Position;
                Animation.SetRunDirection(Animation.FrameDirection.Front);
                Animation.Update(gameTime, Position);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Rectangle destinationRect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            SpriteBatch.Draw(Texture, destinationRect, Color.White);
        }

        private void UpdatePositionBasedOnMovement(GameTime gameTime)
        {
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;  // Elapsed time since last update
        }
    }
}
