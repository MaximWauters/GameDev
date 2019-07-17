using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class Level
    {                                          // init de enemies hier ook zodat bij het aanmaken van het level de enemies erin zitten!
        public Blok[,] Blokken { get; set; }   // klasse groen blok is niet nodig omdat je simpelweg de texture in een blok kan aanpassen
        private readonly int Columns = 50;
        private readonly int Rows = 12;
        public Texture2D TileTexture { get; set; }
        public Texture2D Tile2Texture { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public static Level CurrentBoard { get; private set; }
        public int BoardHeight { get; private set; }

        public Level(SpriteBatch spritebatch, Texture2D tileTexture, Texture2D tile2Texture)
        {
            TileTexture = tileTexture;
            SpriteBatch = spritebatch;
            Tile2Texture = tile2Texture;

            Blokken = new Blok[Columns, Rows];
            
            MaakNieuwLevel();

            Level.CurrentBoard = this;

            BoardHeight = Blokken.GetLength(1) * Blok.TileHeight;
        }

        public byte[,] tileArray = new byte[,]
        {
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,2,2,0,0,0,0,2,2,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,2,2,1,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,0,0,0,0,2,2,1,0,0,0,2,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,0,0,0,0,2,2,1,1,0,0,0,0,0,0,1 },
            { 1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,1,1,2,2,0,0,0,0,2,2,1,1,0,0,0,0,0,1,1 },
            { 1,0,0,1,1,1,0,0,1,1,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,0,0,0,0,1,1,2,2,0,0,0,0,2,2,1,1,1,1,1,1,1,1,1 },
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1 },
        };

        private void MaakNieuwLevel()
        {
            InitBlokkenVoorLevel();
            ZetBorderTilesBlocked();
        }

        public bool HasRoomForRectangle(Rectangle rectangleToCheck)
        {
            foreach (Blok blok in Blokken)
            {
                /*
                if (blok != null && blok.IsBlocked == false)
                {
                    return true;
                }
                */
                if (blok != null && blok.Bounds.Intersects(rectangleToCheck))
                {
                    return false;
                }
                
            }
            return true;
        }

        private Rectangle CreateRectangleAtPosition(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        public Vector2 WhereCanIGetTo(Vector2 originalPosition, Vector2 destination, Rectangle boundingRectangle)
        {
            MovementWrapper move = new MovementWrapper(originalPosition, destination, boundingRectangle);
            for (int i = 1; i <= move.NumberOfStepsToBreakMovementInto; i++)
            {
                Vector2 positionToTry = originalPosition + move.OneStep * i;
                Rectangle newBoundary = CreateRectangleAtPosition(positionToTry, boundingRectangle.Width, boundingRectangle.Height);
                if (HasRoomForRectangle(newBoundary)) { move.FurthestAvailableLocationSoFar = positionToTry; }
                else
                {
                    if (move.IsDiagonalMove)
                    {
                        move.FurthestAvailableLocationSoFar = CheckPossibleNonDiagonalMovement(move, i);
                    }
                    break;
                }
            }
            return move.FurthestAvailableLocationSoFar;
        }

        private Vector2 CheckPossibleNonDiagonalMovement(MovementWrapper wrapper, int i)
        { 
            int stepsLeft = wrapper.NumberOfStepsToBreakMovementInto - (i - 1);

            Vector2 remainingHorizontalMovement = wrapper.OneStep.X * Vector2.UnitX * stepsLeft;
            Vector2 finalPositionIfMovingHorizontally = wrapper.FurthestAvailableLocationSoFar + remainingHorizontalMovement;
            wrapper.FurthestAvailableLocationSoFar = WhereCanIGetTo(wrapper.FurthestAvailableLocationSoFar, finalPositionIfMovingHorizontally, wrapper.BoundingRectangle);

            Vector2 remainingVerticalMovement = wrapper.OneStep.Y * Vector2.UnitY * stepsLeft;
            Vector2 finalPositionIfMovingVertically = wrapper.FurthestAvailableLocationSoFar + remainingVerticalMovement;
            wrapper.FurthestAvailableLocationSoFar = WhereCanIGetTo(wrapper.FurthestAvailableLocationSoFar, finalPositionIfMovingVertically, wrapper.BoundingRectangle);
            
            return wrapper.FurthestAvailableLocationSoFar;
        }

        private void InitBlokkenVoorLevel()
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    Vector2 tilePosition = new Vector2(x * TileTexture.Width, y * TileTexture.Height);
                    Vector2 tile2Position = new Vector2(x * Tile2Texture.Width, y * Tile2Texture.Height);
                    if (tileArray[y, x] == 1)
                        Blokken[x, y] = new Blok(TileTexture, tilePosition, SpriteBatch, true);
                    if (tileArray[y, x] == 2)  // maak nieuwe klasse met appart blok
                    {
                        Blokken[x, y] = new Blok(Tile2Texture, tile2Position, SpriteBatch, true);   // zorg dat je door vast object geraakt
                    }
                }
            }
        }
        
        private void ZetBorderTilesBlocked()
        {
            for (int j = 0; j < Rows; j++)
            {
                for (int i = 0; i < Columns; i++)
                {
                    if (Blokken[i, j] != null && tileArray[j, i] == 2)   // zorg dat je door vast object geraakt
                        Blokken[i, j].IsBlocked = true;

                    if (j == 0 || i == 0 || j == Rows - 1 || i == Columns - 1)
                       if(Blokken[i,j] != null)
                            Blokken[i, j].IsBlocked = true;
                       
                }
            }
        }
        
        public void Draw(SpriteBatch spritebatch)
        {
            for (int j = 0; j < Rows; j++)
            {
                for (int i = 0; i < Columns; i++)
                {
                    if (Blokken[i, j] != null) Blokken[i, j].Draw(spritebatch);
                }
            }
        }
    }
}
