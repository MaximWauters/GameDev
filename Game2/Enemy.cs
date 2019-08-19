using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class Enemy : MovingGameObject
    {
        public float Speed { get; set; }

        public Enemy(Animation animation, Vector2 position, SpriteBatch spritebatch, float speed = 0.4f): base(animation, position)
        {
            Speed = speed;
        }

        public void Update(GameTime gameTime, Hero player)
        {
            Movement += Vector2.UnitX * Speed;
            AffectWithGravity();
            SimulateFriction();
            MoveAsFarAsPossible(gameTime);
            StopMovingIfBlocked();
            ChangeRunningDirectionIfBlocked();
            if (Animation != null)
            {
                Animation.Position = Position;
                if (IsHalted) Animation.SetRunDirection(Animation.FrameDirection.Front);
                Animation.Update(gameTime, Position);
            }
            Collision(player);
        }

        private void Collision(Hero player)
        {
            if (Bounds.Intersects(player.Bounds)) player.Position = new Vector2(70, 100);  //back to startpos
        }

        private void ChangeRunningDirectionIfBlocked()
        {
            if (Movement.X == 0)
            {
                Speed *= -1;
                Animation.SetRunDirection(Animation.FrameDirection.Right);
            } else if (Movement.X < 0)
            {
                Animation.SetRunDirection(Animation.FrameDirection.Front);
            }
        }
    }
}
