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
    {                                          
        private Blok[,] Blokken { get; set; }
        private byte[,] TileArray { get; set; }
        private Vector2[] EnemyPositions { get; set; }
        private Vector2[] TrapPositions { get; set; }
        private SpriteBatch SpriteBatch { get; set; }

        private Texture2D OrangeTileTexture, GreenTileTexture, EnemyTexture, TrapTexture;
        private ObjectAnimation EnemyAnimation, TrapAnimation, TrapAnimation2, TrapAnimation3;
        private Enemy Enemy1, Enemy2;
        private Trap Trap1, Trap1Lvl2, Trap2Lvl2, Trap3Lvl2;               

        private readonly int Columns = 100;
        private readonly int Rows = 12;

        private List<Trap> Traps = new List<Trap>();
        private List<Enemy> Enemies = new List<Enemy>();

        public static Level CurrentBoard { get; private set; }
        public int LevelNr { get; set; }

        public Level(SpriteBatch spritebatch, Texture2D[] tileTextures, byte[,] tileArray, Texture2D enemyTexture, Vector2[] enemyPositions, Texture2D trapTexture, Vector2[] trapPositions, int levelNr)
        {
            OrangeTileTexture = tileTextures[0];
            GreenTileTexture = tileTextures[1];
            SpriteBatch = spritebatch;
            TileArray = tileArray;
            EnemyTexture = enemyTexture;
            EnemyPositions = enemyPositions;
            TrapTexture = trapTexture;
            TrapPositions = trapPositions;
            LevelNr = levelNr;

            Blokken = new Blok[Columns, Rows];

            Enemies.Add(Enemy1);
            Enemies.Add(Enemy2);
            Traps.Add(Trap1);

            TrapAnimation = new ObjectAnimation(TrapTexture, SpriteBatch, TrapPositions[0], 120, 400, 6, 60, Color.WhiteSmoke, .7f, true, 1, new int[] { 0, 0, 0, 0, 0, 0 }, 0);
            TrapAnimation2 = new ObjectAnimation(TrapTexture, SpriteBatch, TrapPositions[1], 120, 400, 6, 60, Color.WhiteSmoke, .7f, true, 1, new int[] { 0, 0, 0, 0, 0, 0 }, 0);
            TrapAnimation3 = new ObjectAnimation(TrapTexture, SpriteBatch, TrapPositions[2], 120, 400, 6, 60, Color.WhiteSmoke, .7f, true, 1, new int[] { 0, 0, 0, 0, 0, 0 }, 0);

            CreateLevel();

            CurrentBoard = this;

            if (levelNr == 2)                // kan zonder levelNr als je traps en enemies meegeeft met Level Object
            {
                Traps.Clear();               //clear enemies en traps van vorige level voor je nieuwe level start
                Enemies.Clear();
                Enemies.Add(Enemy1);
                Enemies.Add(Enemy2);
                CreateLevel2();
            }
        }

        private void CreateLevel()
        {
            InitBlokkenForLevel();
            ZetBorderTilesBlocked();
            CreateEnemies();
            CreateTrapsLvl1();
        }

        private void CreateLevel2()
        {
            InitBlokkenForLevel();
            ZetBorderTilesBlocked();
            CreateEnemies();
            CreateTrapsLvl2();
        }

        private void InitBlokkenForLevel()
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    Vector2 tilePosition = new Vector2(x * OrangeTileTexture.Width, y * OrangeTileTexture.Height);
                    Vector2 tile2Position = new Vector2(x * GreenTileTexture.Width, y * GreenTileTexture.Height);

                    if (TileArray[y, x] == 1) Blokken[x, y] = new Blok(OrangeTileTexture, tilePosition, SpriteBatch, true);
                    if (TileArray[y, x] == 2) Blokken[x, y] = new Blok(GreenTileTexture, tile2Position, SpriteBatch, true);// maak nieuwe klasse met appart blok
                }  
            }
        }

        private void ZetBorderTilesBlocked()
        {
            for (int j = 0; j < Rows; j++)
            {
                for (int i = 0; i < Columns; i++)
                {
                    if (j == 0 || i == 0 || j == Rows - 1 || i == Columns - 1) if (Blokken[i, j] != null) Blokken[i, j].IsBlocked = true;
                }
            }
        }

        private void DrawBlokken(SpriteBatch spritebatch)
        {
            for (int j = 0; j < Rows; j++)
            {
                for (int i = 0; i < Columns; i++)
                {
                    if (Blokken[i, j] != null) Blokken[i, j].Draw(spritebatch);
                }
            }
        }
        //  ===== Start Collision logic =====  ( checkt of de hero een blok in het huidige level raakt ) thanks to https://www.youtube.com/watch?v=P834oA6s6gQ

        public bool HasRoomForRectangle(Rectangle rectangleToCheck)
        {
            foreach (Blok blok in Blokken)
            {
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
        //  ===== End Collision logic =====
        //  ===== Start Enemy / trap logic =====
        private void CreateEnemies()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                EnemyAnimation = new ObjectAnimation(EnemyTexture, SpriteBatch, EnemyPositions[i], 70, 65, 8, 200, Color.WhiteSmoke, 1.4f, true, 2, new int[] { 0, 20, 20, 0 }, 0);
                Enemies[i] = new Enemy(EnemyAnimation, EnemyPositions[i], SpriteBatch);
            }
        }

        public void DrawEnemies() { foreach (Enemy e in Enemies) e.Draw(); }

        private void CreateTrapsLvl1()
        {
            for (int i = 0; i < Traps.Count; i++)
            {
                TrapAnimation = new ObjectAnimation(TrapTexture, SpriteBatch, TrapPositions[i], 120, 400, 6, 60, Color.WhiteSmoke, .7f, true, 1, new int[] { 0, 0, 0, 0, 0, 0 }, 0);
                Traps[i] = new Trap(TrapAnimation, TrapPositions[i], SpriteBatch);
            }
            //Trap1Lvl1 = new Trap(trapAnimation2, TrapPositions[0], SpriteBatch);
            //traps.Add(Trap1Lvl1);
        }

        private void CreateTrapsLvl2()           // niet 1 methode voor aanmaken traps omdat laatste traps variabele snelheid hebben
        {
            Trap1Lvl2 = new Trap(TrapAnimation, TrapPositions[1], SpriteBatch);
            Trap2Lvl2 = new Trap(TrapAnimation2, TrapPositions[0], SpriteBatch, 1.4f);
            Trap3Lvl2 = new Trap(TrapAnimation3, TrapPositions[2], SpriteBatch, 2.5f);      // geef laatste trap aangepaste snelheid voor challange effect

            Traps.Add(Trap1Lvl2);
            Traps.Add(Trap2Lvl2);
            Traps.Add(Trap3Lvl2);
        }

        public void Update(GameTime gameTime, Hero hero)
        {
            foreach (Trap t in Traps) t.Update(gameTime, hero);
            foreach (Enemy e in Enemies) e.Update(gameTime, hero);
        }

        public void DrawTraps() { foreach (Trap t in Traps) t.Draw(); }
        //  ===== End Enemy / trap logic =====

        public void Draw(SpriteBatch spritebatch)
        {
            DrawBlokken(spritebatch);
            DrawEnemies();
            DrawTraps(); 
        }
    }
}
