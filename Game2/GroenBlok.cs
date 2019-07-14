﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class GroenBlok : GameObject
    {
        public bool IsBlocked { get; set; }
        public static int TileHeight { get; private set; }

        int _tileHeight = 63; // original 63

        public GroenBlok(Texture2D texture, Vector2 position, SpriteBatch batch, bool isBlocked) : base(texture, position, batch)
        {
            IsBlocked = isBlocked;
            TileHeight = _tileHeight;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            // if (IsBlocked)
            //{
            Rectangle destinationRect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, TileHeight);
            SpriteBatch.Draw(Texture, destinationRect, Color.White);
            //}
        }
    }
}