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
        private List<MenuItem> MenuItems { get; set; }
        public List<MenuItem> ControlsItems { get; private set; }

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
        private Vector2 _menuItemPos;

        public SpriteFont TitleFont { get; private set; }
        public SpriteFont DescriptionFont { get; private set; }

        public Texture2D BackgroundTexture { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public KeyboardState OldKeyboardState { get; private set; }

        public int Selection { get; private set; }
        public string MenuTitle { get; private set; }
        public string Footer { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; set; }
        public Vector2 TitlePos { get; private set; }
        public Vector2 DescriptionPos { get; private set; }
        public Vector2 FooterPos { get; private set; }
        public Vector2 ControlsPos { get; private set; }

        public Menu(SpriteFont[] menuFonts, Texture2D backgroundTexture, SpriteBatch spriteBatch)
        {
            BackgroundTexture = backgroundTexture;
            SpriteBatch = spriteBatch;

            TitleFont = menuFonts[0];
            DescriptionFont = menuFonts[1];

            IsActive = true;

            MenuTitle = "Kirito's Adventures";
            Description = "The objective is to advance from left to right, \ngrind trough each level, \ncollect as many coins as you can along the way \nand rescue the princess!";
            Footer = "Game by Maxim Wauters for AP hogeschool";

            TitlePos = new Vector2(400, 20);
            FooterPos = new Vector2(50, 750);
            DescriptionPos = new Vector2(410, 120);

            MenuItem _playGame = new MenuItem("Play Game", TitleFont, SpriteBatch);
            MenuItem _options = new MenuItem("Controls", TitleFont, SpriteBatch);
            MenuItem _exitGame = new MenuItem("Exit", TitleFont, SpriteBatch);

            MenuItem _space = new MenuItem("Space Bar = Jump", TitleFont, SpriteBatch);          // kon evt ook met string
            MenuItem _right = new MenuItem("Right Arrow = Go Right", TitleFont, SpriteBatch);
            MenuItem _left = new MenuItem("Left Arrow = Go Left", TitleFont, SpriteBatch);

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
            int topOffset = 100;

            foreach (MenuItem item in MenuItems)
            {
                marge += 120;

                _menuItemPos.X = 50;
                _menuItemPos.Y = (topOffset + marge);

                item.Position = MenuItemPos; 
            }
        }

        public void PositionControls()
        {
            int marge = 0;
            int topOffset = 300;

            foreach (MenuItem item in ControlsItems)
            {
                marge += 60;
                _controlsItemPos.X = 320;
                _controlsItemPos.Y = (topOffset + marge);

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
                SpriteBatch.DrawString(TitleFont, MenuTitle, TitlePos, Color.Orange);
                SpriteBatch.DrawString(DescriptionFont, Description, DescriptionPos, Color.Aquamarine);
                SpriteBatch.DrawString(DescriptionFont, Footer, FooterPos, Color.Orange);
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
