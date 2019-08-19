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
        private Vector2 OldPosition;
        public Vector2 Movement { get; set; }
        public bool IsHalted { get; private set; }
 
        public MovingGameObject(Animation animation, Vector2 position) : base(animation, position)
        { }

        public static bool IsOnFirmGround(Rectangle bounds)
        {
            Rectangle onePixelLower = bounds;
            onePixelLower.Offset(0, 1);
            return !Level.CurrentBoard.HasRoomForRectangle(onePixelLower);
        }

        protected virtual void StopMovingIfBlocked()
        {
            Vector2 lastMovement = Position - OldPosition;
            if (lastMovement.X == 0) Movement *= Vector2.UnitY; 
            if (lastMovement.Y == 0) Movement *= Vector2.UnitX; 
        }

        protected virtual void AffectWithGravity() { Movement += Vector2.UnitY * .65f; }

        protected void MoveAsFarAsPossible(GameTime gameTime)               
        {
            OldPosition = Position;
            UpdatePositionBasedOnMovement(gameTime);
            Position = Level.CurrentBoard.WhereCanIGetTo(OldPosition, Position, Bounds);
        }

        protected virtual void SimulateFriction()
        {
            if (IsOnFirmGround(Bounds)) Movement *= Vector2.One * .92f;     // Slow down hero 92% per update cycle
            else Movement *= Vector2.One * 0.98f;                           // Slow down hero 98% per update cycle
        }
        
        private void UpdatePositionBasedOnMovement(GameTime gameTime)
        {
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;  // Elapsed time since last update
        }
        
    }
}

