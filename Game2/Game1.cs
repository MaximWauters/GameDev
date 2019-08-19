using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
        private Texture2D _orangeTileTexture, _heroTexture, _greenTileTexture, _enemyTexture, _flamesTexture, _menuBackground, _endScreenBackground,_finishTexture, _backTexture, _backTexture2, _coinTexture, _coinScoreTexture, _asunaTexture, _asunaTexture2;
        private Texture2D[] _blokkenVoorLevel;
        private Vector2[] _enemyPositonsLvl1, _enemyPositonsLvl2;
        private Vector2[] _trapPositons;
        private Camera2d _camera;
        private Hero _hero;
        private Coin _Coin, _Coin2, _Coin3, _Coin4, _Coin5, _Coin6;
        private Level _level1, _level2;
        private Finish _finish, _asuna1, _asuna2;
        private SpriteFont _debugFont;
        private Animation _heroAnimation, _coinAnimation, _coinAnimation2, _coinAnimation3, _coinAnimation4, _coinAnimation5, _coinAnimation6;
        private Menu _menu { get; set; }
        private EndScreen _theEnd { get; set; }
        private SpriteFont _titleFont, _descriptionFont;
        private SpriteFont[] _menuFonts;
        public SoundEffect sound { get; private set; }
        public Song themeSong;
        public int lvl = 1;
        private int score = 0;
        
        List<Enemy> Enemies = new List<Enemy>();
        List<Trap> Traps = new List<Trap>();
        List<Coin> Coins = new List<Coin>();

        public byte[,] _tileArrayLvl1 = new byte[,]  // tile map level 1 ( GUI for level design possible? :) )
        {
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,0,2,1,1,1,1,1,1,1,1,1,1,1,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,2,2,0,0,0,0,2,2,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,2,2,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,0,0,0,0,2,2,1,0,0,0,2,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,0,0,0,0,2,2,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1 },
            { 1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,1,1,2,2,0,0,0,0,2,2,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,2,2,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,1 },
            { 1,0,0,1,1,1,0,0,1,1,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,0,0,0,0,1,1,2,2,0,0,0,0,2,2,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1 },
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1 },
        };

        public byte[,] _tileArrayLvl2 = new byte[,]  // tile map level 2
        {
            { 1,1,1,1,1,1,1,1,1,1,2,0,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,0,2,2,2,0,2,1,1,1,1,1,1,1,1 },
            { 1,0,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,2,0,2,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,2,0,2,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,1,0,0,0,0,1,0,2,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1 },
            { 1,1,1,1,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,2,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1 },
            { 1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,2,0,0,0,1,1,1,1,1,1,1,1 },
        };

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
            _orangeTileTexture = Content.Load<Texture2D>("Brick_Block1");
            _greenTileTexture = Content.Load<Texture2D>("groen2");
            _heroTexture = Content.Load<Texture2D>("kiri4");
            _enemyTexture = Content.Load<Texture2D>("enemy2");
            _flamesTexture = Content.Load<Texture2D>("flames4");
            _menuBackground = Content.Load<Texture2D>("bk1");
            _endScreenBackground = Content.Load<Texture2D>("end");
            _finishTexture = Content.Load<Texture2D>("finish2");
            _backTexture = Content.Load<Texture2D>("BK3");
            _backTexture2 = Content.Load<Texture2D>("BK2");
            _coinTexture = Content.Load<Texture2D>("Coin");
            _coinScoreTexture = Content.Load<Texture2D>("CoinScore");
            _asunaTexture = Content.Load<Texture2D>("Asuna2");
            _asunaTexture2 = Content.Load<Texture2D>("Asuna3");

            _titleFont = Content.Load<SpriteFont>("menuTitle");
            _descriptionFont = Content.Load<SpriteFont>("Description");
            _debugFont = Content.Load<SpriteFont>("DebugFont2");

            _menuFonts = new SpriteFont[] { _titleFont, _descriptionFont };
            _menu = new Menu(_menuFonts, _menuBackground, _spriteBatch);

            sound = Content.Load<SoundEffect>("coinCollect1");

            themeSong = Content.Load<Song>("SAO3_Administrator");
            MediaPlayer.Play(themeSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            _finish = new Finish(_finishTexture, _spriteBatch, new Vector2(6250, 450), 1);
            _asuna1 = new Finish(_asunaTexture, _spriteBatch, new Vector2(6280, 475), 1.7f);
            _asuna2 = new Finish(_asunaTexture2, _spriteBatch, new Vector2(6280, 475), 1.7f);

            _coinAnimation6 = new ObjectAnimation(_coinTexture, _spriteBatch, new Vector2(2880, 540), 130, 150, 6, 90, Color.WhiteSmoke, .5f, true, 1, new int[] { 0, 0, 0, 0 }, 0);
            _coinAnimation5 = new ObjectAnimation(_coinTexture, _spriteBatch, new Vector2(1800, 600), 130, 150, 6, 90, Color.WhiteSmoke, .5f, true, 1, new int[] { 0, 0, 0, 0 }, 0);
            _coinAnimation4 = new ObjectAnimation(_coinTexture, _spriteBatch, new Vector2(1500, 600), 130, 150, 6, 90, Color.WhiteSmoke, .5f, true, 1, new int[] { 0, 0, 0, 0 }, 0);
            _coinAnimation = new ObjectAnimation(_coinTexture, _spriteBatch, new Vector2(1411, 165), 130, 150, 6, 90, Color.WhiteSmoke, .5f, true, 1, new int[] { 0, 0, 0, 0 }, 0);
            _coinAnimation2 = new ObjectAnimation(_coinTexture, _spriteBatch, new Vector2(845, 290), 130, 150, 6, 90, Color.WhiteSmoke, .5f, true, 1, new int[] { 0, 0, 0, 0 }, 0);
            _coinAnimation3 = new ObjectAnimation(_coinTexture, _spriteBatch, new Vector2(250, 480), 130, 150, 6, 90, Color.WhiteSmoke, .5f, true, 1, new int[] { 0, 0, 0, 0 }, 0);

            _heroAnimation = new ObjectAnimation(_heroTexture, _spriteBatch, new Vector2(70, 100), 40, 65, 4, 90, Color.WhiteSmoke, 1.4f, true, 4, new int[] { 0, 0, 0, 0 }, 0);
            _hero = new Hero(_heroAnimation, new Vector2(70, 100), _spriteBatch);

            _blokkenVoorLevel = new Texture2D[] { _orangeTileTexture, _greenTileTexture };
            _enemyPositonsLvl1 = new Vector2[] { new Vector2(1400, 550), new Vector2(2800, 160) };
            _enemyPositonsLvl2 = new Vector2[] { new Vector2(900, 550), new Vector2(4450, 550) };
            _trapPositons = new Vector2[] { new Vector2(5500, 400), new Vector2(700, 400), new Vector2(5750, 400) };

            _level1 = new Level(_spriteBatch, _blokkenVoorLevel, _tileArrayLvl1, _enemyTexture, _enemyPositonsLvl1, _flamesTexture, _trapPositons, 1);

            CreateCoins();
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0.5f;
            MediaPlayer.Play(themeSong);
            MediaPlayer.IsRepeating = true;
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
            if (_menu.IsActive) _menu.Update(gameTime);

            // Zorg ervoor dat als je beweegt & je hero midden van het scherm komt dat je dan pas de x van de camera updatetet
            if (!_menu.IsActive) camPos.X = _hero.Position.X - 600;

            UpdateCoins(gameTime, _hero);   // update animatie coin
            checkCol(_hero);                // col-det voor coins

            _hero.Update(gameTime); 

            if (lvl == 1)
            {
                _level1.Update(gameTime, _hero);
                _finish.Update(_hero);
            }
            if (lvl == 2)
            {
                _level2.Update(gameTime, _hero);
                _asuna1.Update(_hero);
                _asuna2.Update(_hero);
            }
            base.Update(gameTime);
        }

        private void CreateCoins()
        {
            _Coin6 = new Coin(_coinAnimation6, new Vector2(2880, 540), _spriteBatch);
            _Coin5 = new Coin(_coinAnimation5, new Vector2(1800, 600), _spriteBatch);     
            _Coin4 = new Coin(_coinAnimation4, new Vector2(1500, 600), _spriteBatch);
            _Coin = new Coin(_coinAnimation, new Vector2(1411, 165), _spriteBatch);
            _Coin2 = new Coin(_coinAnimation2, new Vector2(845, 290), _spriteBatch);
            _Coin3 = new Coin(_coinAnimation3, new Vector2(250, 480), _spriteBatch);

            Coins.Add(_Coin6);
            Coins.Add(_Coin5);              
            Coins.Add(_Coin4);
            Coins.Add(_Coin);
            Coins.Add(_Coin2);
            Coins.Add(_Coin3);
            
        }

        private void UpdateCoins(GameTime gameTime, Hero player)
        {
            foreach ( Coin c in Coins)
            {
                c.Update(gameTime, player);
            }
        }

        private void DrawCoins() { foreach (Coin c in Coins) c.Draw(); }

        private void checkCol(Hero h)
        {
            for (int i = 0; i < Coins.Count; i++)
            {
                if (Coins[i].Bounds.Intersects(h.Bounds))
                {
                    score++;
                    sound.Play();
                    Coins.RemoveAt(i);
                }
            }
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

            bool canDraw = false;

            if (_menu.IsActive == true) _menu.Draw(); 
            else
            {
                // TODO: Add your drawing code here
                if (_finish.Win == true)
                {
                    lvl = 2;
                    _level2 = new Level(_spriteBatch, _blokkenVoorLevel, _tileArrayLvl2, _enemyTexture, _enemyPositonsLvl2, _flamesTexture, _trapPositons, 2);
                    _hero.Position = new Vector2(70, 100);
                    _hero.Movement = Vector2.Zero;
                    _finish.Win = false;
                    Coins.Clear();
                }
                else if (!_finish.Win == true && lvl == 1)
                {
                    DrawBackground(_backTexture);
                    _level1.Draw(_spriteBatch);
                    _hero.Draw();
                    _finish.Draw();
                    DrawCoins();
                    WriteDebugInfo();
                    DrawScore();
                }
                else if (_hero.Position == Vector2.Zero) _theEnd.Draw();
                else if (!_finish.Win == true && lvl == 2)
                {
                    DrawBackground(_backTexture2);
                    _level2.Draw(_spriteBatch);
                    _hero.Draw();
                    _asuna1.Draw();
                    WriteDebugInfo();
                    DrawScore();
                    if (_asuna1.Win == true && canDraw == false)
                    {
                        _asuna2.Draw();
                        canDraw = true;
                    }
                }
                if (_asuna2.Win == true && canDraw == true && _hero.Position.X > 6270)
                {
                    System.Threading.Thread.Sleep(2000);    // let the game sleep for 2 sec to ensure to player that the end has been reached.
                    _hero.Position = Vector2.Zero;
                    _camera.Position = Vector2.Zero;
                    //GraphicsDevice.Clear(Color.WhiteSmoke);

                    _theEnd = new EndScreen(_menuFonts, score, _endScreenBackground, _spriteBatch);
                    _theEnd.IsActive = true;
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawBackground(Texture2D background)
        {
            int marge = 630;
            int bkWidth = _graphics.PreferredBackBufferWidth;
            int bkHeight = _graphics.PreferredBackBufferHeight;
            Rectangle destRect = new Rectangle((int)(_hero.Position.X - marge), 0, bkWidth + 50, bkHeight);
            _spriteBatch.Draw(background, destRect, Color.WhiteSmoke);
        }
        
        private void DrawScore()
        {
            string scoreText = string.Format("{0}x", score);
            Rectangle destRect = new Rectangle((int)(camPos.X + 1430), 70, 50, 50);

            _spriteBatch.DrawString(_titleFont, scoreText, new Vector2(camPos.X + 1330, 55), Color.Orange);
            _spriteBatch.Draw(_coinScoreTexture, destRect, Color.WhiteSmoke);
        }
        
        private void WriteDebugInfo()
        {
            string positionInText = string.Format("Position of the hero: ({0:0.0}, {1:0.0})", _hero.Position.X, _hero.Position.Y);
            string movementInText = string.Format("Current movement: ({0:0.0}, {1:0.0})", _hero.Movement.X, _hero.Movement.Y);
            string isOnFirmGroundText = string.Format("On firm ground? : {0} ", _hero.IsOnFirmGround());

            _spriteBatch.DrawString(_debugFont, positionInText, camPos, Color.Red);
            _spriteBatch.DrawString(_debugFont, movementInText, new Vector2(camPos.X, 20), Color.Red);
            _spriteBatch.DrawString(_debugFont, isOnFirmGroundText, new Vector2(camPos.X, 40), Color.Red);
        }
    }
}
