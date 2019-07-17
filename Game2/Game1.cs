using System;
using System.Collections.Generic;
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
        public static Game1 landscape { get; set; }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _tileTexture, _jumperTexture, _tile2Texture, _enemyTexture;
        private Camera2d _camera;
        private Hero _hero;
        private Enemy _enemy, _enemy2;
        private Level _level1;
        private SpriteFont _debugFont;
        private Animation _heroAnimation, _enemyAnimation, _enemyAnimation2;
        private SpriteStrip _heroSpriteStrip, _enemySpriteStrip;
        List<Enemy> Enemies = new List<Enemy>();
        public bool ShouldRestartGame { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1500;
            _graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
            landscape = this;
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
            _jumperTexture = Content.Load<Texture2D>("kiri4");
            _enemyTexture = Content.Load<Texture2D>("enemy2");

            _heroSpriteStrip = new SpriteStrip(_jumperTexture);
            _enemySpriteStrip = new SpriteStrip(_enemyTexture);

            _heroAnimation = new HeroAnimation(_jumperTexture, _spriteBatch, new Vector2(70, 100), 40, 65, 4, 90, Color.WhiteSmoke, 1.4f, true, 4, new int[] { 0, 0 , 0, 0 }, 0);
            _enemyAnimation = new HeroAnimation(_enemyTexture, _spriteBatch, new Vector2(2000, 160), 70, 65, 8, 200, Color.WhiteSmoke, 1.4f, true, 2, new int[] { 0 , 20, 20, 0 }, 0);
            _enemyAnimation2 = new HeroAnimation(_enemyTexture, _spriteBatch, new Vector2(2800, 160), 70, 65, 8, 200, Color.WhiteSmoke, 1.4f, true, 2, new int[] { 0, 20, 20, 0 }, 0);

            CreateEnemies(1, _enemyTexture, _spriteBatch);

            _debugFont = Content.Load<SpriteFont>("DebugFont2");
            _hero = new Hero(_heroAnimation, new Vector2(70, 100), _spriteBatch);
            _enemy = new Enemy(_enemyAnimation, new Vector2(2000, 160), _spriteBatch);
            _enemy2 = new Enemy(_enemyAnimation2, new Vector2(2800, 160), _spriteBatch);
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
            if (_hero.isMoving == (true|false) && _hero.Position.X > 600)
                camPos.X = _hero.Position.X - 600;


            UpdateEnemies(gameTime, _hero);

            base.Update(gameTime);
            _hero.Update(gameTime);
            
        }

        float rotation = 0;
        float zoom = 1;
        Vector2 camPos = new Vector2();

        private void CreateEnemies(int level, Texture2D enemyTexture, SpriteBatch spriteBatch)
        {
            Vector2 _enemyPosition = new Vector2(2000,160);
            Vector2 _enemyPosition2 = new Vector2(2800, 160);

            float _enemySpeed;
            Random rnd = new Random();

            for (int i = 0; i < 1; i++)
            {
                _enemySpeed = (rnd.Next(0, 9) * ((0.4f - 0.2f) / 9)) + 0.2f;

                //_enemyAnimation = new Animation(_enemyTexture, _spriteBatch, _enemyPositions[i], 175, 241, 1, 45, Color.WhiteSmoke, 1f, true);

                _enemy = new Enemy(_enemyAnimation, _enemyPosition, _spriteBatch, _enemySpeed);
                _enemy2 = new Enemy(_enemyAnimation2, _enemyPosition2, _spriteBatch, _enemySpeed);


                Enemies.Add(_enemy);
                Enemies.Add(_enemy2);
            }
        }

        // kan in enemy klasse met in de update hier enemy.draw
        private void DrawEnemies()
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw();
            }
        }

        private void UpdateEnemies(GameTime gameTime, Hero player)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(gameTime, player);
            }
        }

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

            DrawEnemies();
            //_enemy.Draw();
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
