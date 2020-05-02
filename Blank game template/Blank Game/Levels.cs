using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PPM_Maze
{
    public class Levels : State
    {
        
        public Path path { get; set; }
        //protected Obstacle[] obstacleList;
        public static List<Obstacle> obstacleList { get; set; }
        public static List<Coin> coinList { get; set; }
        protected int levelWidth;
        protected int levelHeight;

        public static Texture2D pathTexture;
        public static Texture2D coinTexture;
        public static Color goodColor = Color.Black;
        public static Color badColor = Color.Crimson;
        public Texture2D whiteRectangle;

        protected SpriteBatch spriteBatch;
        protected Texture2D whiteRect;
        protected Camera2D camera;
        protected Cursor cursor;
        UI ui;
        public static int cameraScrollSpeed = 25;
        float recordInterval = 0.1f;
        float thetaTime = 0;
        float healthDrainBuffer = 0.2f;
        int healthDrain = 1;
        float lastDamageTime = 0;
        float elapsedTime = 0;
        protected static int numCoinsCollected;
        protected bool isProcedural = false;
        bool isFinished = false;
        Rectangle finishBounds;
        protected GraphicsDevice graphicsDevice;


        public Levels(Game1 game, GraphicsDevice graphicsdevice, ContentManager content) : base(game,graphicsdevice,content)
        {
            graphicsDevice = graphicsdevice;
            whiteRectangle = new Texture2D(graphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            camera = new Camera2D(graphicsDevice.Viewport);
            cursor = new Cursor(content.Load<Texture2D>("CircleSprite"), new Vector2(400, 240), camera);
            pathTexture = content.Load<Texture2D>("gray");
            coinTexture = content.Load<Texture2D>("coin");
            ui = new UI(whiteRectangle, content.Load<SpriteFont>("Fonts/font"),cursor.getMaxHealth());
            Game1.toggleMenuMusic(false);
        }

        public Levels(String fileName, Game1 game, GraphicsDevice graphicsdevice, ContentManager content) : base(game, graphicsdevice, content)
        {
            graphicsDevice = graphicsdevice;
            whiteRectangle = new Texture2D(graphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            camera = new Camera2D(graphicsDevice.Viewport);
            cursor = new Cursor(content.Load<Texture2D>("CircleSprite"), new Vector2(400, 240), camera);
            pathTexture = content.Load<Texture2D>("gray");
            coinTexture = content.Load<Texture2D>("coin");
            loadLevel(fileName);
            Game1.toggleMenuMusic(false);



        }

       

        void loadLevel(String fileName)
        {
            StreamReader file = new StreamReader(fileName);
            String firstline = file.ReadLine();
            String[] firstSplit = firstline.Split(',');
            levelWidth = Int32.Parse(firstSplit[0]);
            levelHeight = Int32.Parse(firstSplit[1]);
            int numObstacles = Int32.Parse(file.ReadLine());
            int numCoins = Int32.Parse(file.ReadLine());
            obstacleList = new List<Obstacle>();
            coinList = new List<Coin>();
            initialisePath(levelWidth, levelHeight);
            for (int i = 0; i < numObstacles; i++)
            {
                Obstacle tempObstacle;
                String obstacleLine = file.ReadLine();
                String[] obstacleSplit = obstacleLine.Split(',');
                if (obstacleSplit[3] == "top")
                {
                    tempObstacle = new Obstacle(Int32.Parse(obstacleSplit[0]), (int)path.startPos.Y, Int32.Parse(obstacleSplit[1]), Int32.Parse(obstacleSplit[2]));
                }
                else
                {
                    tempObstacle = new Obstacle(Int32.Parse(obstacleSplit[0]), (int)path.startPos.Y + path.height - Int32.Parse(obstacleSplit[2]), Int32.Parse(obstacleSplit[1]), Int32.Parse(obstacleSplit[2]));
                }
                obstacleList.Add(tempObstacle);
            }
            for (int j = 0; j < numCoins; j++)
            {
                Coin tempCoin;
                String coinLine = file.ReadLine();
                String[] coinSplit = coinLine.Split(',');
                tempCoin = new Coin(Int32.Parse(coinSplit[0]), (int)path.startPos.Y + Int32.Parse(coinSplit[1]));
                coinList.Add(tempCoin);
            }

            finishBounds = new Rectangle(levelWidth - 100, (int)path.startPos.Y, 100, levelHeight);
        }

        void initialisePath(int width, int height)
        {
            path = new Path(width, height, pathTexture);
        }

        public void initialiseGraphics(SpriteBatch _spriteBatch, Texture2D _whiteRect)
        {
            spriteBatch = _spriteBatch;
            whiteRectangle = _whiteRect;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.ChangeState(new levelSelect(_game, _graphicsDevice, _content));
            }

            if (ui != null)
            {
                ui.updateUI(cursor.getHealth(), elapsedTime.ToString());
            }
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            cursor.worldLocation = new Vector2(Mouse.GetState().X + camera.Position.X, Mouse.GetState().Y);
            cursor.spritePos = new Vector2(Mouse.GetState().X - 15, Mouse.GetState().Y - 15);

            camera.Position += new Vector2(cameraScrollSpeed, 0) * deltaTime;
            if (coinList != null)
            {
                foreach (Coin coin in Levels.coinList)
                {
                    coin.checkIfCollected(cursor.worldLocation);

                }
                checkCollectedCoins();
            }

            


            //obstacle.updatePosition(gameTime);
            //if (Keyboard.GetState().IsKeyDown(Keys.Enter) || level.getFinished())
            //{
            //    levelIndex++;
            //    if (levelIndex > levelList.Count - 1) levelIndex = levelList.Count - 1;
            //    level = new Levels(levelList[levelIndex], pathTexture);
            //    Debug.WriteLine(calculatePlayerPercentage(cursor.getPositionList()));
            //    cursor.resetList();
            //    level.initialiseGraphics(spriteBatch, whiteRectangle);
            //    _camera.Position = new Vector2(0, 0);
            //}
            finishCheck(new Rectangle((int)cursor.worldLocation.X, (int)cursor.worldLocation.Y, 30, 30));

            if (getFinished() || (cursor.getHealth() <= 0 && isProcedural))
            {
                /*
                 * TO DO:
                 * IMPLEMENT ADDITIONAL MENU CLASS
                 * WILL NEED ADDITIONAL ARGUMENTS: WHETHER THE USER PASSED OR FAILED THE LEVEL, WHETHER LEVEL WAS PROCEDURAL OR NOT AND THEIR ACCURACY
                 * MENU SHOULD CONTAIN OPTIONS TO :
                 *  RETURN TO MAIN MENU
                 *  IF  LEVEL WAS PROCEDURAL -> PLAY AGAIN
                 *  IF LEVEL WAS NORMAL -> PLAY NEXT LEVEL
                 *  
                 * MENU SHOULD DISPLAY ACCURACY AND INDICATE PASS OR FAIL (EG GREEN BACKGROUND FOR PASS OR RED BACKGROUND FOR FAIL)
                 */
                double accuracy = calculatePlayerPercentage(cursor.getPositionList());
                bool pass;
                if (getFinished())
                {
                    pass = true;
                }
                else
                {
                    pass = false;
                }
                if (isProcedural)
                {
                    _game.ChangeState(new AfterProceduralMenu(pass, accuracy, _game, _graphicsDevice, _content));
                }
                else
                {
                    _game.ChangeState(new AfterLevelMenu(pass, accuracy, _game, _graphicsDevice, _content));
                }
            }

            //record mouse location for accuracy
            if (elapsedTime > thetaTime + recordInterval)
            {
                cursor.recordLocation();
                thetaTime += recordInterval;
                //Debug.WriteLine("locationRecorded");
            }
            //check if out of bounds and deduct health if so
            if(elapsedTime > lastDamageTime + healthDrainBuffer)
            {
                if (!path.isPlayerInBounds(cursor.worldLocation))
                {
                    cursor.deductHealth(healthDrain);
                    
                }
                foreach(Obstacle o in obstacleList)
                {
                    if (o.isPlayerInBounds(cursor.worldLocation))
                    {
                        cursor.deductHealth(healthDrain);
                    }
                }
                lastDamageTime += healthDrainBuffer;
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
           
            if (path.isPlayerInBounds(cursor.worldLocation))
            {
                graphicsDevice.Clear(goodColor);
            }
            else { graphicsDevice.Clear(badColor); }
            path.Draw(spriteBatch, camera);
            for (int i = 0; i < obstacleList.Count(); i++)
            {
                obstacleList[i].Draw(spriteBatch, whiteRectangle, cursor.worldLocation, camera);
            }
            if (coinList != null)
            {
                for (int i = 0; i < coinList.Count(); i++)
                {
                    coinList[i].Draw(spriteBatch,camera);
                }
            }
            for (int i = 0; i < numCoinsCollected; i++)
            {
                Vector2 UICoinPos;
                UICoinPos.X = 0 + i * 60;
                UICoinPos.Y = Game1.windowHeight - 50;

                spriteBatch.Begin();
                spriteBatch.Draw(coinTexture, UICoinPos, Color.White);
                spriteBatch.End();
            }
            cursor.Draw(spriteBatch);

            if (ui != null)
            {
                ui.draw(spriteBatch);
            }
        }
        public void checkCollectedCoins()
        {
            int tempCoinCounter = 0;
            foreach (Coin coin in coinList)
            {
                if (!coin.isAvailable)
                {
                    tempCoinCounter++;
                }
            }
            numCoinsCollected = tempCoinCounter;
        }

        public void finishCheck(Rectangle playerRect)
        {
            if (playerRect.Intersects(finishBounds))
            {
                isFinished = true;
            }

        }

        public bool getFinished()
        {
            return isFinished;
        }
        public bool isPointInObstacle(Vector2 point)
        {
            foreach(Obstacle obstacle in obstacleList)
            {
                if (obstacle.isPlayerInBounds(point))
                {
                    return true;
                }
            }
            return false;
        }
        double calculatePlayerPercentage(List<Vector2> positionList)
        {
            double numInBounds = 0;
            double numOutBounds = 0;

            foreach (Vector2 point in positionList)
            {
                //if point not in path
                if (isPointInObstacle(point))
                {
                    numOutBounds += 1;
                }
                else if (!path.isPointInBounds(point))
                {
                    numOutBounds += 1;
                }
                else
                {
                    numInBounds += 1;
                }
            }
            double total = numInBounds + numOutBounds;
            double ratio = 100 * (numInBounds / total);
            ratio = Math.Round(ratio, 2);
            return ratio;
        }
    }

    public class ProceduralLevel : Levels
    {
        //float 0 - 1 represents difficulty
        float difficulty = 0.5f;
        int standardLevelHeight = 400;
        int minDistance = 70;
        int maxObstacles;
        const int OBSTACLE_LIMIT = 40;
        int maxObsWidth = 300;
        int minObsWidth = 30;
        int maxObsHeight;
        int minObsHeight = 50;
        int padding = 50;
        int segmentWidth = 2000;
        int generationinterval = 500;

        int segmentMarker = 0;
        Random r = new Random();
        List<Obstacle> tempObsList = new List<Obstacle>();
        public ProceduralLevel(Game1 game, GraphicsDevice graphicsdevice, ContentManager content) : base(game, graphicsdevice, content)
        {
            graphicsDevice = graphicsdevice;
            whiteRectangle = new Texture2D(graphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            camera = new Camera2D(graphicsDevice.Viewport);
            cursor = new Cursor(content.Load<Texture2D>("CircleSprite"), new Vector2(400, 240), camera);
            pathTexture = content.Load<Texture2D>("gray");
            obstacleList = new List<Obstacle>();
            path = new Path(1, standardLevelHeight, pathTexture);
            generateSegment();
            isProcedural = true;
        }

        public void generationCheck(Vector2 pos)
        {
            if((int)pos.X >= segmentMarker - generationinterval)
            {
                generateSegment();
            }
        }

        public void generateSegment()
        {
            maxObsHeight = standardLevelHeight - minDistance;
            path.extend(segmentWidth);
            maxObstacles = (int)(OBSTACLE_LIMIT * difficulty);
            //int numObstacles = (int)(maxObstacles * difficulty);
            //add obstacles, varying on difficulty
            for (int i = 0; i < maxObstacles; i++)
            {
                bool overlapFlag = false;
                Obstacle tempObstacle;
                int x = r.Next(segmentMarker, segmentMarker + segmentWidth);
                int h = r.Next(minObsHeight, maxObsHeight);
                int w = r.Next(minObsWidth,maxObsWidth);
                int y;
                if(r.Next(2) == 1)
                {
                    y = (int)path.startPos.Y;
                }
                else {
                    y = (int)path.startPos.Y + path.height - h;
                }

                tempObstacle = new Obstacle(x, y, w, h);

                if(tempObsList == null)
                {
                    tempObsList.Add(tempObstacle);
                }
                else
                {
                    foreach(Obstacle o in tempObsList)
                    {
                        //if valid placement
                        if (obstacleOverlap(o, tempObstacle))
                        {
                            if((int)o.getWidthHeight().Y + (int)tempObstacle.getWidthHeight().Y < standardLevelHeight - minDistance)
                            {
                                tempObsList.Add(tempObstacle);
                                break;
                            }
                            else
                            {
                                overlapFlag = true;
                                break;
                            }
                        }
                        
                    }

                    if (!tempObsList.Contains(tempObstacle) && overlapFlag == false)
                    {
                        tempObsList.Add(tempObstacle);
                    }

                }
                


            }
            foreach(Obstacle o  in tempObsList)
            {
                obstacleList.Add(o);
            }
            segmentMarker += segmentWidth;
        }

        bool obstacleOverlap(Obstacle o1,Obstacle o2)
        {
            if (o1.getPosition().X + o1.getWidthHeight().X + padding >= o2.getPosition().X ||
                o2.getPosition().X + o2.getWidthHeight().X + padding >= o1.getPosition().X)
            {
                return true;
            }

            return false;
        }

        void alterDifficulty(float amount)
        {
            difficulty += amount;
        }
    }



}



//    public class LevelOne : Levels
//    {
//        public LevelOne(int levelwidth, Texture2D pathtexture )
//        {
//            levelWidth = levelwidth;
//            pathTexture = pathtexture;
//            //Levels.obstacleList = new List<Obstacle>();
//            initialisePath();
//            initialiseObstacles();
//            initialiseCoins();
//        }

//        public void initialisePath()
//        {
//            path = new Path(levelWidth, Game1.windowHeight /4, pathTexture, Game1.windowHeight /4);
//        }
//        public void initialiseObstacles()
//        {

//            obstacleList = new List<Obstacle>
//            { 
//             new Obstacle(300, (int)Path.startPos.Y, 50, 100),
//             new Obstacle(600,((int)Path.startPos.Y + Path.height) - 100,100,100),
//             new Obstacle(900, (int)Path.startPos.Y, 80, 80),
//             new Obstacle(1200, (int)Path.startPos.Y, 100, 30),
//             new Obstacle(1200,((int)Path.startPos.Y + Path.height) - 30,100,30),
//             new Obstacle(1500, (int)Path.startPos.Y, 100, 60),
//             new Obstacle(1500,((int)Path.startPos.Y + Path.height) - 60,100,60),
//            };

//        }

//        public void initialiseCoins()
//        {
//            coinList = new List<Coin>
//            {
//                new Coin(400,((int)Path.startPos.Y +50)),
//                new Coin(1030,((int)Path.startPos.Y)),
//                new Coin(1620,((int)Path.startPos.Y+150))
//            };
//        }

//        public static void checkCollectedCoins()
//        {
//            int tempCoinCounter = 0;
//            foreach(Coin coin in coinList)
//            {
//                if(!coin.isAvailable)
//                {
//                    tempCoinCounter++;
//                }
//            }
//            numCoinsCollected = tempCoinCounter;
//        }
//    }

//    public class LevelTwo:Levels
//    {
//        public LevelTwo(int levelwidth, Texture2D pathtexture)
//        {
//            levelWidth = levelwidth;
//            pathTexture = pathtexture;

//            initialisePath();
//            initialiseObstacles();
//            initialiseCoins();
//        }
//        public void initialisePath()
//        {
//            path = new Path(levelWidth, Game1.windowHeight / 4, pathTexture, Game1.windowHeight / 4);
//        }
//        public void initialiseObstacles()
//        {

//            obstacleList = new List<Obstacle>
//            {
//             new Obstacle(100, (int)Path.startPos.Y, 50, 100),
//             new Obstacle(300,((int)Path.startPos.Y + Path.height) - 100,100,100),
//             new Obstacle(500, (int)Path.startPos.Y, 80, 80),
//             new Obstacle(800, (int)Path.startPos.Y, 100, 30),
//             new Obstacle(800,((int)Path.startPos.Y + Path.height) - 30,100,30),
//             new Obstacle(1500, (int)Path.startPos.Y, 100, 60),
//             new Obstacle(1500,((int)Path.startPos.Y + Path.height) - 60,100,60),
//            };

//        }

//        public void initialiseCoins()
//        {
//            coinList = new List<Coin>
//            {
//                new Coin(600,((int)Path.startPos.Y +50)),
//                new Coin(1030,((int)Path.startPos.Y)),
//                new Coin(1620,((int)Path.startPos.Y+150))
//            };
//        }

//        public static void checkCollectedCoins()
//        {
//            int tempCoinCounter = 0;
//            foreach (Coin coin in coinList)
//            {
//                if (!coin.isAvailable)
//                {
//                    tempCoinCounter++;
//                }
//            }
//            numCoinsCollected = tempCoinCounter;
//        }
//    }
//}
