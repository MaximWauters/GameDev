using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class MovingGameObject : Sprite
    {
        private Vector2 oldPosition;
        public Vector2 Movement { get; set; }
        //public static MovingGameObject Hero { get; private set; }
        public bool IsHalted { get; private set; }
        /*
        public MovingGameObject(Texture2D texture, Vector2 position, SpriteBatch spriteBatch) : base(texture, spriteBatch, position)
        { }
        */
        public MovingGameObject(Animation animation, Vector2 position) : base(animation, position)
        {
           // Hero = this;
        }

        public static bool IsOnFirmGround(Rectangle bounds)
        {
            Rectangle onePixelLower = bounds;
            onePixelLower.Offset(0, 1);
            return !Level.CurrentBoard.HasRoomForRectangle(onePixelLower);
        }

        protected virtual void StopMovingIfBlocked()
        {
            Vector2 lastMovement = Position - oldPosition;
            if (lastMovement.X == 0) { Movement *= Vector2.UnitY; }
            if (lastMovement.Y == 0) { Movement *= Vector2.UnitX; }
        }

        protected virtual void AffectWithGravity() { Movement += Vector2.UnitY * .65f; }

        protected void MoveAsFarAsPossible(GameTime gameTime)
        {
            oldPosition = Position;
            UpdatePositionBasedOnMovement(gameTime);
            Position = Level.CurrentBoard.WhereCanIGetTo(oldPosition, Position, Bounds);
        }

        protected virtual void SimulateFriction()
        {
            if (IsOnFirmGround(Bounds)) { Movement *= Vector2.One * .92f; }    // Slow down jumper 92% per update cycle
            else { Movement *= Vector2.One * 0.98f; }                               // Slow down jumper 98% per update cycle
        }
        
        private void UpdatePositionBasedOnMovement(GameTime gameTime)
        {
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;  // Elapsed time since last update
        }
        
    }
}

