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
    class Hero : MovingGameObject
    {
        private Vector2 oldPosition;
        private KeyboardState keyboardOldState;

        // animatie
        private Texture2D currentAnim { get; set; }
        private ObjectAnimation  heroAnim {get;set;}
        // 

        public Hero(Animation animation, Vector2 position, SpriteBatch sprite) : base(animation, position)
        {

        }

        public void Update(GameTime gameTime)
        {
            CheckKeyboardAndUpdateMovement(gameTime);
            AffectWithGravity();
            SimulateFriction();
            MoveIfPossible(gameTime);
            StopMovingIfBlocked();
            CheckPossibleMovement();

            if (Animation != null)
            {
                Animation.Position = Position;
                if (IsHalted) Animation.SetRunDirection(Animation.FrameDirection.Front);
                Animation.Update(gameTime, Position);
            }
        }

        public void CheckPossibleMovement()  // als je in een trap valt bv
        {
            if (Position.Y > 2000) Position = new Vector2(70, 100);
        }

        public bool IsOnFirmGround()
        {
            Rectangle onePixelLower = Bounds;
            onePixelLower.Offset(0, 1);
            return !Level.CurrentBoard.HasRoomForRectangle(onePixelLower);
        }
       
        protected override void StopMovingIfBlocked()
        {
            Vector2 lastMovement = Position - oldPosition;
            if (lastMovement.X == 0) Movement *= Vector2.UnitY;
            if (lastMovement.Y == 0) Movement *= Vector2.UnitX;
        }

        protected override void AffectWithGravity()
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
            if (!keyboardState.IsKeyDown(Keys.Left) || !keyboardState.IsKeyDown(Keys.Left)) { Animation.SetRunDirection(Animation.FrameDirection.Front); }
            if (keyboardState.IsKeyDown(Keys.Left)) { Movement -= Vector2.UnitX * 0.8f; Animation.SetRunDirection(Animation.FrameDirection.Left); }
            if (keyboardState.IsKeyDown(Keys.Right)) { Movement += Vector2.UnitX * 0.8f; Animation.SetRunDirection(Animation.FrameDirection.Right); }
            if (keyboardState.IsKeyDown(Keys.Space) && IsOnFirmGround())
            {
                if (!keyboardOldState.IsKeyDown(Keys.Space)) Movement = -Vector2.UnitY * 24;         // jump height
            }
            else if (keyboardOldState.IsKeyDown(Keys.Space)) { }  // Key was down last update, but not now, so it has just been released
            keyboardOldState = keyboardState;
        }
        
        protected override void SimulateFriction()
        {
            if (IsOnFirmGround()) { Movement -= Movement * Vector2.One * .08f; }
            else { Movement -= Movement * Vector2.One * .02f; }
        }
       
        private void UpdatePositionBasedOnMovement(GameTime gameTime)
        {
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 20;
        }

        public override void Draw()
        {
            if (Texture != null) SpriteBatch.Draw(Texture, Position, Color.WhiteSmoke);
            else if (Animation != null) Animation.Draw();
        }
    }
}
