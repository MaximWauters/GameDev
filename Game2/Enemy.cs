using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class Enemy : Jumper
    {
        #region Fields
        int _walkingTime = 2000;
        #endregion

        #region Properties
        public int WalkingTime { get; private set; }
        public float Speed { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        #endregion

        public Enemy(Animation animation, Vector2 position, SpriteBatch spritebatch, float speed = 0.4f)
            : base(animation, position)
        {
            WalkingTime = _walkingTime;
            Speed = speed;
            Movement += Vector2.UnitX * Speed;
            //Width = texture.Width;
            //Height = texture.Height;

            //Game1.landscape.(this);
        }

        public void Update(GameTime gameTime, Hero player)
        {
            Movement += Vector2.UnitX * Speed;
            AffectWithGravity();
            SimulateFriction();
            MoveAsFarAsPossible(gameTime, true);
            StopMovingIfBlocked();
            ChangeRunningDirectionIfBlocked();
            if (Animation != null)
            {
                Animation.Position = Position;
                if (IsHalted) Animation.SetRunDirection(Animation.runDirection.Front);
                Animation.Update(gameTime, Position);
            }
            Collision(player);
        }

        #region Private Fields
        private void Collision(Hero player)
        {
            if (this.Bounds.Intersects(player.Bounds))
            {
                //game1.CurrentLevel.ShouldRestartGame = true;
                player.Position = new Vector2(1000, 70);
            }
        }
        #endregion
        #region Private Methods
        private void ChangeRunningDirectionIfBlocked()
        {
            if (Movement.X == 0)
            {
                Speed *= -1;
                Animation.SetRunDirection(Animation.runDirection.Right);
            } else if (Movement.X < 0)
            {
                Animation.SetRunDirection(Animation.runDirection.Front);
            }

        }
        #endregion
    }
}
