using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class EndScreen
    {
        public SpriteFont GameFont { get; private set; }
        public SpriteFont GameFont2 { get; private set; }
        public string EndTitle { get; private set; }
        private Texture2D BackgroundTexture { get; set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public bool IsActive { get; set; }
        public int Score { get; set; }
        public string ScoreText { get; set; }

        public EndScreen(SpriteFont[] gameFonts, int score, Texture2D backgroundTexture, SpriteBatch spriteBatch)
        {
            GameFont = gameFonts[0];
            GameFont2 = gameFonts[1];
            BackgroundTexture = backgroundTexture;
            SpriteBatch = spriteBatch;
            Score = score;
            EndTitle = "The end has been reached! \n\nYou succesfully managed \n to rescue the princess!";
            ScoreText = "Total coins collected: " + score;
            IsActive = false;
        }

        public void Draw()
        {
            Game1.landscape.GraphicsDevice.Clear(Color.WhiteSmoke);

            Vector2 TitlePos = new Vector2(-500, 30);
            Vector2 ScorePos = new Vector2(-500, 400);

            if (BackgroundTexture != null)
            {
                Rectangle _backgroundRect = new Rectangle(-600, 0, Game1.landscape.GraphicsDevice.Viewport.Width, Game1.landscape.GraphicsDevice.Viewport.Height);
                SpriteBatch.Draw(BackgroundTexture, _backgroundRect, Color.WhiteSmoke);
                SpriteBatch.DrawString(GameFont, EndTitle, TitlePos, Color.Orange);
                SpriteBatch.DrawString(GameFont, ScoreText, ScorePos, Color.Orange);
            }
        }
    }
}
