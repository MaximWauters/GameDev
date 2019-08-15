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
    class Menu
    {
        public List<MenuItem> MenuItems { get; private set; }
        public List<MenuItem> ControlsItems { get; private set; }

        //public KeyboardKeys ks { get; set; }

        public Vector2 MenuItemPos
        {
            get { return _menuItemPos; }
            set { _menuItemPos = value; }
        }
        public Vector2 ControlsItemPos
        {
            get { return _controlsItemPos; }
            set { _controlsItemPos = value; }
        }
        private Vector2 _controlsItemPos;
        private Vector2 _menuItemPos; // hoort bij bovenstaande get en set ofc...

        public SpriteFont GameFont { get; private set; }
        public SpriteFont GameFont2 { get; private set; }

        public Texture2D BackgroundTexture { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public KeyboardState OldKeyboardState { get; private set; }
        public bool IsActive { get; set; }
        public int Selection { get; private set; }
        public string MenuTitle { get; private set; }
        public string Footer { get; private set; }
        public string Description { get; private set; }

        public Vector2 TitlePos { get; private set; }
        public Vector2 DescriptionPos { get; private set; }
        public Vector2 FooterPos { get; private set; }
        public Vector2 ControlsPos { get; private set; }

        public Menu(SpriteFont[] gameFonts, Texture2D backgroundTexture, SpriteBatch spriteBatch)
        {
            BackgroundTexture = backgroundTexture;
            SpriteBatch = spriteBatch;

            GameFont = gameFonts[0];
            GameFont2 = gameFonts[1];

            IsActive = true;

            MenuTitle = "Kirito's Adventures";
            Description = "The objective is to advance from left to right, \ngrind trough each level, \ncollect as many coins as you can along the way \nand rescue the princess!";
            Footer = "Game by Maxim Wauters for AP hogeschool";

            TitlePos = new Vector2(400, 20);
            FooterPos = new Vector2(50, 750);
            DescriptionPos = new Vector2(410, 120);

            

            MenuItem _playGame = new MenuItem("Play Game", GameFont, spriteBatch);
            MenuItem _options = new MenuItem("Controls", GameFont, spriteBatch);
            MenuItem _exitGame = new MenuItem("Exit", GameFont, spriteBatch);

            MenuItem _space = new MenuItem("Space Bar = Jump", GameFont, spriteBatch);
            MenuItem _right = new MenuItem("Right Arrow = Go Right", GameFont, spriteBatch);
            MenuItem _left = new MenuItem("Left Arrow = Go Left", GameFont, spriteBatch);

            MenuItems = new List<MenuItem>();
            ControlsItems = new List<MenuItem>();

            MenuItems.Add(_playGame);
            MenuItems.Add(_options);
            MenuItems.Add(_exitGame);

            ControlsItems.Add(_space);
            ControlsItems.Add(_right);
            ControlsItems.Add(_left);  
        }
        
        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            //KeyboardState prevKs = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Up) && !OldKeyboardState.Equals(ks))
            {
                Selection--;
                if (Selection < 0) Selection = 0;
            }
            if (ks.IsKeyDown(Keys.Down) && !OldKeyboardState.Equals(ks))
            {
                Selection++;
                if (Selection > MenuItems.Count - 1) Selection = MenuItems.Count - 1;
            }

            if (ks.IsKeyDown(Keys.Enter) && !OldKeyboardState.Equals(ks))
            {
                switch (Selection)
                {
                    case 0:
                        IsActive = false;
                        break;
                    case 1:
                        break; // show controls in draw section...
                    case 2:
                        Game1.landscape.Exit();
                        break;
                }
            }
            OldKeyboardState = ks;
        }

        public void PositionMenuItems()
        {
            int marge = 0;

            foreach (MenuItem item in MenuItems)
            {
                marge += 100;

                _menuItemPos.X = 50;
                _menuItemPos.Y = (120 + marge);

                item.Position = MenuItemPos; 
            }
        }

        public void PositionControls()
        {
            int marge = 0;

            foreach (MenuItem item in ControlsItems)
            {
                marge += 60;
                _controlsItemPos.X = 320;
                _controlsItemPos.Y = (300 + marge);

                item.Position = ControlsItemPos;
            }
        }

        public void Draw()
        {
            PositionMenuItems();
            PositionControls();

            Game1.landscape.GraphicsDevice.Clear(Color.WhiteSmoke);

            if (BackgroundTexture != null)
            {
                Rectangle _backgroundRect = new Rectangle(0, 0, Game1.landscape.GraphicsDevice.Viewport.Width, Game1.landscape.GraphicsDevice.Viewport.Height);
                SpriteBatch.Draw(BackgroundTexture, _backgroundRect, Color.WhiteSmoke);
                SpriteBatch.DrawString(GameFont, MenuTitle, TitlePos, Color.Orange);
                SpriteBatch.DrawString(GameFont2, Description, DescriptionPos, Color.Aquamarine);
                SpriteBatch.DrawString(GameFont2, Footer, FooterPos, Color.Orange);
            }

            if (Selection == 1)
            {
                for (int i = 0; i < ControlsItems.Count; i++)
                {
                    ControlsItems.ElementAt(i).Draw(false);
                }
            }

            for (int i = 0; i < MenuItems.Count; i++)
            {
                bool _isSelected = (i == Selection);
                MenuItems.ElementAt(i).Draw(_isSelected);
            } 
        }
    }
}
