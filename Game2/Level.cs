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

        public Trap Trap1Lvl1 { get; set; } // doe hetzelfde voor traps als blok!
        public Trap Trap1Lvl2 { get; set; }
        public Trap Trap2Lvl2 { get; set; }
        public Trap Trap3Lvl2 { get; set; }

        public HeroAnimation trapAnimation {get; set;}
        public HeroAnimation trapAnimation2 { get; set; }
        public HeroAnimation trapAnimation3 { get; set; }

        private Enemy enemy1 { get; set; }
        private Enemy enemy2 { get; set; }
        private HeroAnimation enemy1Animation { get; set; }
        private HeroAnimation enemy2Animation { get; set; }
        private HeroAnimation enemy1Animation2 { get; set; }
        private HeroAnimation enemy2Animation2 { get; set; }

        private readonly int Columns = 100;
        private readonly int Rows = 12;

        public Texture2D TileTexture { get; set; }
        public Texture2D Tile2Texture { get; set; }
        public Texture2D _enemyTexture { get; set; }
        public Texture2D _trapTexture { get; set; }

        public SpriteBatch SpriteBatch { get; set; }
        public static Level CurrentBoard { get; private set; }
        public int LevelNr { get; set; }

        public List<Trap> traps = new List<Trap>();
        private List<Enemy> enemies = new List<Enemy>();

        public Level(SpriteBatch spritebatch, Texture2D tileTexture, Texture2D tile2Texture, Texture2D enemyTexture, Texture2D trapTexture, int levelNr)
        {
            TileTexture = tileTexture; // array pls
            SpriteBatch = spritebatch;
            Tile2Texture = tile2Texture;
            _enemyTexture = enemyTexture;
            _trapTexture = trapTexture;
            LevelNr = levelNr;

            Blokken = new Blok[Columns, Rows];

            enemy1Animation = new HeroAnimation(_enemyTexture, spritebatch, new Vector2(900, 550), 70, 65, 8, 200, Color.WhiteSmoke, 1.4f, true, 2, new int[] { 0, 20, 20, 0 }, 0);
            enemy2Animation = new HeroAnimation(_enemyTexture, spritebatch, new Vector2(4450, 550), 70, 65, 8, 200, Color.WhiteSmoke, 1.4f, true, 2, new int[] { 0, 20, 20, 0 }, 0);
            enemy1Animation2 = new HeroAnimation(_enemyTexture, spritebatch, new Vector2(2000, 160), 70, 65, 8, 200, Color.WhiteSmoke, 1.4f, true, 2, new int[] { 0, 20, 20, 0 }, 0);
            enemy2Animation2 = new HeroAnimation(_enemyTexture, spritebatch, new Vector2(2800, 160), 70, 65, 8, 200, Color.WhiteSmoke, 1.4f, true, 2, new int[] { 0, 20, 20, 0 }, 0);

            trapAnimation = new HeroAnimation(_trapTexture, spritebatch, new Vector2(700, 400), 120, 400, 6, 60, Color.WhiteSmoke, .7f, true, 1, new int[] { 0, 0, 0, 0, 0, 0 }, 0);
            trapAnimation2 = new HeroAnimation(_trapTexture, spritebatch, new Vector2(5500, 400), 120, 400, 6, 60, Color.WhiteSmoke, .7f, true, 1, new int[] { 0, 0, 0, 0, 0, 0 }, 0);
            trapAnimation3 = new HeroAnimation(_trapTexture, spritebatch, new Vector2(5750, 400), 120, 400, 6, 60, Color.WhiteSmoke, .7f, true, 1, new int[] { 0, 0, 0, 0, 0, 0 }, 0);

            MaakLevel1();

            Level.CurrentBoard = this;

            if (levelNr == 2)
            {
                traps.Clear();
                enemies.Clear();  //clear enemies en traps van vorige level voor je nieuwe level start
                MaakLevel2();
                
            }
        }

        public byte[,] tileArray = new byte[,]  // tile map level 1 ( GUI for level design possible? :) )
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

        public byte[,] tileArrayLvl2 = new byte[,]  // tile map level 2
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

        private void MaakLevel1()
        {
            InitBlokkenVoorLevel();
            ZetBorderTilesBlocked();
            CreateEnemiesLvl1();
            CreateTrapsLvl1();
        }

        public void MaakLevel2()
        {
            InitBlokkenVoorLevel();
            ZetBorderTilesBlocked();
            CreateEnemiesLvl2();
            CreateTrapsLvl2();
        }

        private void InitBlokkenVoorLevel()
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    Vector2 tilePosition = new Vector2(x * TileTexture.Width, y * TileTexture.Height);
                    Vector2 tile2Position = new Vector2(x * Tile2Texture.Width, y * Tile2Texture.Height);

                    if (LevelNr == 1) // geef de tile array mee met het level
                    {
                        if (tileArray[y, x] == 1) Blokken[x, y] = new Blok(TileTexture, tilePosition, SpriteBatch, true);
                        
                        if (tileArray[y, x] == 2) Blokken[x, y] = new Blok(Tile2Texture, tile2Position, SpriteBatch, false);// maak nieuwe klasse met appart blok
                    }
                    //
                    if (LevelNr == 2)
                    {
                        if (tileArrayLvl2[y, x] == 1) Blokken[x, y] = new Blok(TileTexture, tilePosition, SpriteBatch, true);

                        if (tileArrayLvl2[y, x] == 2) Blokken[x, y] = new Blok(Tile2Texture, tile2Position, SpriteBatch, true);// maak nieuwe klasse met appart blok
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
                   // if (Blokken[i, j] != null && tileArray[j, i] == 2) Blokken[i, j].IsBlocked = false;  // zorg dat je door vast object geraakt

                    if (j == 0 || i == 0 || j == Rows - 1 || i == Columns - 1) if (Blokken[i, j] != null) Blokken[i, j].IsBlocked = true;
                }
            }
        }
        //  ===== Start Collision logic =====  ( checkt of de hero een blok in het huidige level raakt )

        public bool HasRoomForRectangle(Rectangle rectangleToCheck)
        {
            foreach (Blok blok in Blokken)
            {
                /*
                if (blok != null && blok.Bounds.Intersects(rectangleToCheck) && blok.IsBlocked == false)
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
        //  ===== End Collision logic =====
        //  ===== Start Enemy / trap logic =====
        public void CreateEnemiesLvl1() //y
        {
            Vector2 _enemyPosition = new Vector2(2000, 160);
            Vector2 _enemy2Position = new Vector2(2800, 160);

            enemy1 = new Enemy(enemy1Animation2, _enemyPosition, SpriteBatch);
            enemy2 = new Enemy(enemy2Animation2, _enemy2Position, SpriteBatch);

            enemies.Add(enemy1);
            enemies.Add(enemy2);
        }
        public void CreateEnemiesLvl2() 
        {
            Vector2 _enemyPosition = new Vector2(900, 550); 
            Vector2 _enemy2Position = new Vector2(4450, 550);

            enemy1 = new Enemy(enemy1Animation, _enemyPosition, SpriteBatch);
            enemy2 = new Enemy(enemy2Animation, _enemy2Position, SpriteBatch);

            enemies.Add(enemy1);
            enemies.Add(enemy2);
        }

        public void UpdateEnemies(GameTime gt, Hero hero) { foreach (Enemy enemy in enemies) enemy.Update(gt, hero); }

        public void DrawEnemies() { foreach (Enemy enemy in enemies) enemy.Draw(); }

        public void CreateTrapsLvl1()
        {
            Vector2 _trap1Position = new Vector2(5500, 400);

            Trap1Lvl1 = new Trap(trapAnimation2, _trap1Position, SpriteBatch);

            traps.Add(Trap1Lvl1);

        }
        public void CreateTrapsLvl2()
        {
            Vector2 _trap1Position = new Vector2(700, 400);
            Vector2 _trap2Position = new Vector2(5500, 400);
            Vector2 _trap3Position = new Vector2(5750, 400);

            Trap1Lvl2 = new Trap(trapAnimation, _trap1Position, SpriteBatch);
            Trap2Lvl2 = new Trap(trapAnimation2, _trap2Position, SpriteBatch, 1.4f);
            Trap3Lvl2 = new Trap(trapAnimation3, _trap3Position, SpriteBatch, 2.5f); // geef laatste trap aangepaste snelheid voor challange effect

            traps.Add(Trap1Lvl2);
            traps.Add(Trap2Lvl2);
            traps.Add(Trap3Lvl2);
        }

        public void UpdateTraps(GameTime gt, Hero p) { foreach (Trap c in traps) c.Update(gt, p); }

        public void DrawTraps() { foreach (Trap t in traps) t.Draw(); }
        //  ===== End Enemy / trap logic =====

        public void Draw(SpriteBatch spritebatch)
        {
            for (int j = 0; j < Rows; j++)
            {
                for (int i = 0; i < Columns; i++)
                {
                    if (Blokken[i, j] != null) Blokken[i, j].Draw(spritebatch);
                }
            }
            DrawEnemies();
            DrawTraps(); 
        }
    }
}
