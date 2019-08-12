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
        #region Properties
        public float Speed { get; set; }
        #endregion

        public Trap(Animation animation, Vector2 position, SpriteBatch spritebatch, float speed = 1f) : base(animation, position)
        {
            Speed = speed;
            Movement = Vector2.UnitY * (15 + Speed);
        }

        public void Update(GameTime gameTime, Hero player)
        {
            Movement = Vector2.UnitY * (15 + Speed);

            UpdatePositionBasedOnMovement(gameTime);

            if (Animation != null)
            {
                Animation.Position = Position;
                Animation.SetRunDirection(Animation.runDirection.Front);
                Animation.Update(gameTime, Position);
            }
            if (Position.Y > 800)
            { 
                Animation.Position = new Vector2(Position.X, -500);
                Position = new Vector2(Position.X, -500);
            }
            Collision(player);
        }

        #region Private Fields
        private void Collision(Hero player)
        {
            if (this.Bounds.Intersects(player.Bounds))
            {
                player.Position = new Vector2(1000, 70);
            }

        }
        
        private void UpdatePositionBasedOnMovement(GameTime gameTime)
        {
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;  // Elapsed time since last update
        }
        
        #endregion
    }
}
