using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class Trap : MovingGameObject
    {
        public float Speed { get; set; }

        public Trap(Animation animation, Vector2 position, SpriteBatch spritebatch, float speed = 1f) : base(animation, position)
        {
            Speed = speed;
            Movement = Vector2.UnitY * (15 + Speed);
        }

        public void Update(GameTime gameTime, Hero player)
        {
            UpdatePositionBasedOnMovement(gameTime);
            if (Animation != null)
            {
                Animation.Position = Position;
                Animation.SetRunDirection(Animation.FrameDirection.Front);
                Animation.Update(gameTime, Position);
            }
            if (Position.Y > 800)           // als trap van het scherm af is reset dan deze trap op dezelfde locatie
            { 
                Animation.Position = new Vector2(Position.X, -500);
                Position = new Vector2(Position.X, -500);
            }
            Collision(player);
        }

        private void Collision(Hero player)
        {
            if (this.Bounds.Intersects(player.Bounds)) player.Position = new Vector2(70, 100);
        }
        
        private void UpdatePositionBasedOnMovement(GameTime gameTime)
        {
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;  // Elapsed time since last update
        }
    }
}
