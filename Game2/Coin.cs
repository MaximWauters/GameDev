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
        //public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public Animation Anim { get; set; }
        public int Score { get; set; }

        public Coin(Animation anim, Vector2 position, SpriteBatch spriteBatch, float speed) : base(anim, position)
        {
            Anim = anim;
            Position = position;
            Speed = speed;
            Score = 0;
        }

        public void Update(GameTime gameTime, Hero player)
        {
            if (Animation != null)
            {
                Animation.Position = Position;
                Animation.SetRunDirection(Animation.runDirection.Front);
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
