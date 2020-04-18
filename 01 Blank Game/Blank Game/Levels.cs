using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PPM_Maze
{
    public class Levels
    {
        /*Implement:
         * Path
         * List of obstacles
         * List of treasure (to be implemented later)
         * Finish line location
         * Level width(same as path width)
         */
        public Path path { get; set; }
        //protected Obstacle[] obstacleList;
        public static List<Obstacle> obstacleList { get; set; }
        public static List<Coin> coinList { get; set; }
        protected int levelWidth;
        protected int levelHeight;
        protected Texture2D pathTexture;
        protected SpriteBatch spriteBatch;
        protected Texture2D whiteRect;

        protected static int numCoinsCollected;

        bool isFinished = false;
        Rectangle finishBounds;
        


        public Levels()
        {


        }

        public Levels(String fileName, Texture2D pathText)
        {
            loadLevel(fileName);
            pathTexture = pathText;
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
            whiteRect = _whiteRect;
        }

        public void Draw()
        {
            path.Draw(spriteBatch);

            for (int i = 0; i < obstacleList.Count(); i++)
            {
                obstacleList[i].Draw(spriteBatch, whiteRect);
            }
            for (int i = 0; i < coinList.Count(); i++)
            {
                coinList[i].Draw(spriteBatch);
            }
            for (int i = 0; i < numCoinsCollected; i++)
            {
                Vector2 UICoinPos;
                UICoinPos.X = 0 + i * 60;
                UICoinPos.Y = Game1.windowHeight - 50;

                spriteBatch.Begin();
                spriteBatch.Draw(Game1.coinTexture, UICoinPos, Color.White);
                spriteBatch.End();
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
