using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class Finish : Sprite
    {
        public Vector2 Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Win { get; set; }

        public Finish(Texture2D texture, SpriteBatch spriteBatch, Vector2 position, float scaleFactor) : base(texture, spriteBatch, position, scaleFactor)
        {
            Position = position;
            Scale = scaleFactor;
            Width = (int)(texture.Width * Scale);
            Height = (int)(texture.Height * Scale);
            Win = false; 
        }

        public void Update(Hero hero)
        {
            Collision(hero);
            //LoadNextLevelOrEndGame(explosionAninamtion, level);
        }

        public override void Draw()
        {
            if (Win == false)
            {
                Rectangle destinationRect = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
                SpriteBatch.Draw(Texture, destinationRect, Color.WhiteSmoke);
            } 
        }

        private void Collision(Hero hero)
        {
            if (Bounds.Intersects(hero.Bounds))
            {
                //TrumpLand.CurrentTrumpLand.AddExplosion(CenterPosition);
                //TrumpLand.CurrentTrumpLand.ExplosionSound.Play();
                //PreviousStateIsCollision = true;
                Win = true;
                //Console.WriteLine("jeps");
            }
            else
            {
                Win = false;
            }

            /*
            else if (this.Bounds.Intersects(player.Bounds) && explosionAnimation.Active == false && PreviousStateIsCollision == false)
            {
                explosionAnimation.Active = true;
                TrumpLand.CurrentTrumpLand.ExplosionSound.Play();
                PreviousStateIsCollision = true;
            }
            else if (!this.Bounds.Intersects(player.Bounds))
            {
                PreviousStateIsCollision = false;
            }
            */
        }
    }
}
