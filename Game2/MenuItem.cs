using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class MenuItem
    {
        public string Text { get; private set; }
        public SpriteFont MenuItemFont { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public Vector2 Position { get; set; }

        public MenuItem(string text, SpriteFont menuItemFont, SpriteBatch spriteBatch)
        {
            Text = text;
            MenuItemFont = menuItemFont;
            SpriteBatch = spriteBatch;
        }

        public void Draw(bool isSelected)
        {
            if (isSelected) SpriteBatch.DrawString(MenuItemFont, Text, Position, Color.White);
            else SpriteBatch.DrawString(MenuItemFont, Text, Position, Color.Orange);
        }
    }
}
