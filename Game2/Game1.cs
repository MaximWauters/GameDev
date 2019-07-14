using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _tileTexture, _jumperTexture, _tile2Texture;
        private Camera2d _camera;
        private Hero _hero;
        private Level _level1;
        private SpriteFont _debugFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1500;
            _graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _camera = new Camera2d(GraphicsDevice.Viewport);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _tileTexture = Content.Load<Texture2D>("Brick_Block1");
            _tile2Texture = Content.Load<Texture2D>("groen2");
            _jumperTexture = Content.Load<Texture2D>("char");
            _debugFont = Content.Load<SpriteFont>("DebugFont2");
            _hero = new Hero(_jumperTexture, Vector2.One * 140, _spriteBatch); 
            _level1 = new Level(_spriteBatch, _tileTexture, _tile2Texture);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Zorg ervoor dat als je beweegt & je hero midden van het scherm komt dat je dan pas de x van de camera updatetet
            if (_hero.isMoving == true && _hero.Position.X > 600)
                camPos.X = _hero.Position.X - 600;

            base.Update(gameTime);
            _hero.Update(gameTime);
            
        }

        float rotation = 0;
        float zoom = 1;
        Vector2 camPos = new Vector2();

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            var viewMatrix = _camera.GetViewMatrix();

            _camera.Position = camPos;
            _camera.Rotation = rotation;
            _camera.Zoom = zoom;

            _spriteBatch.Begin(transformMatrix: viewMatrix);

            // TODO: Add your drawing code here
            
            _level1.Draw(_spriteBatch);
            WriteDebugInformation();
            _hero.Draw();
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void WriteDebugInformation()
        {
            string positionInText = string.Format("Position of Jumper: ({0:0.0}, {1:0.0})", _hero.Position.X, _hero.Position.Y);
            string movementInText = string.Format("Current movement: ({0:0.0}, {1:0.0})", _hero.Movement.X, _hero.Movement.Y);
            string isOnFirmGroundText = string.Format(" On firm ground ? : {0} ", _hero.IsOnFirmGround());
            string isMovingText = string.Format(" isMoving ? : {0} ", _hero.isMoving);

            _spriteBatch.DrawString(_debugFont, positionInText, camPos, Color.Red);
            _spriteBatch.DrawString(_debugFont, movementInText, new Vector2(camPos.X, 20), Color.Red);
            _spriteBatch.DrawString(_debugFont, isOnFirmGroundText, new Vector2(camPos.X, 40), Color.Red);
            _spriteBatch.DrawString(_debugFont, isMovingText, new Vector2(camPos.X, 60), Color.Red);
        }
    }
}
