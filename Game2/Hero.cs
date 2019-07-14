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
    class Hero : GameObject
    {
        public Vector2 Movement { get; set; }
        private Vector2 oldPosition;
        private KeyboardState keyboardOldState;
        public bool isMoving = false;

        // animatie
        private Texture2D currentAnim { get; set; }
        private Animation animation;
        // 

        public Hero(Texture2D texture, Vector2 position, SpriteBatch spritebatch) : base(texture, position, spritebatch)
        {
            //texture = currentAnim;

            //animation = new Animation();

            //animation.AddFrame(new Rectangle(0, 0, 40, 80));
            //animation.AddFrame(new Rectangle(40, 0, 44, 80));
            //animation.AddFrame(new Rectangle(85, 0, 50, 80));
            //animation.AddFrame(new Rectangle(135, 0, 40, 80));

            //animation.AantalBewegingenPerSeconde = 15;
        }

        public void Update(GameTime gameTime)
        {
            CheckKeyboardAndUpdateMovement(gameTime);
            AffectWithGravity();
            SimulateFriction();
            MoveIfPossible(gameTime);
            StopMovingIfBlocked();
            
        }

        public bool IsOnFirmGround()
        {
            Rectangle onePixelLower = Bounds;
            onePixelLower.Offset(0, 1);
            return !Level.CurrentBoard.HasRoomForRectangle(onePixelLower);
        }

        private void StopMovingIfBlocked()
        {
            Vector2 lastMovement = Position - oldPosition;
            if (lastMovement.X == 0) { Movement *= Vector2.UnitY; }
            if (lastMovement.Y == 0) { Movement *= Vector2.UnitX; }
        }

        private void AffectWithGravity()
        {
            Movement += Vector2.UnitY * .65f;
        }

        private void MoveIfPossible(GameTime gameTime)
        {
            oldPosition = Position;
            UpdatePositionBasedOnMovement(gameTime);
            Position = Level.CurrentBoard.WhereCanIGetTo(oldPosition, Position, Bounds);

            
        }

        private void CheckKeyboardAndUpdateMovement(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left)) { Movement -= Vector2.UnitX * 0.8f; isMoving = true; }
            if (keyboardState.IsKeyDown(Keys.Right)) { Movement += Vector2.UnitX * 0.8f; isMoving = true;   /* animation.Update(gameTime);  */  }
            if (keyboardState.IsKeyDown(Keys.Space) && IsOnFirmGround())
            {
                if (!keyboardOldState.IsKeyDown(Keys.Space))
                {
                    Movement = -Vector2.UnitY * 24;         // jump height
                    isMoving = true;
                }
            }
            else if (keyboardOldState.IsKeyDown(Keys.Space))
            {
                // Key was down last update, but not now, so it has just been released
                //isMoving = false;
            }
            keyboardOldState = keyboardState;
        }

        private void SimulateFriction()
        {
            if (IsOnFirmGround()) { Movement -= Movement * Vector2.One * .08f; }
            else { Movement -= Movement * Vector2.One * .02f; }
        }

        private void UpdatePositionBasedOnMovement(GameTime gameTime)
        {
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 20;
        }
        /*
        public override void Draw()
        {
            SpriteBatch.Draw(Texture, Position, animation.CurrentFrame.SourceRectangle, Color.White);
        }
        */
    }
}
