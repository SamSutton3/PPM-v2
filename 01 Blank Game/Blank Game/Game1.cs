using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace PPM_Maze
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public static Cursor cursor;
        //Path path;
        //Obstacle obstacle;
        public static Camera2D _camera;
        //LevelOne levelOne;
        //LevelTwo levelTwo;
        Levels level;
        //constants
        public static int windowWidth = 1200;
        public static int windowHeight = 800;
        public static int cameraScrollSpeed = 25;
        public static Color goodColor = Color.Black;
        public static Color badColor = Color.Crimson;
        public Texture2D whiteRectangle;
        public static Texture2D pathTexture;
        public static Texture2D coinTexture;

        List<String> levelList;
        int levelIndex = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            _camera = new Camera2D(GraphicsDevice.Viewport);

            levelList = new List<String> { 
            "LevelOne.txt",
            "levelTwo.txt"
            
            };


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D cursorTexture = this.Content.Load<Texture2D>("CircleSprite");
            pathTexture = this.Content.Load<Texture2D>("gravel");
            Path.setTexture(pathTexture);
            coinTexture = this.Content.Load <Texture2D>("coin");
            Coin.setTexture(coinTexture);
            cursor = new Cursor(cursorTexture, new Vector2(400, 240),_camera);
            level = new Levels(levelList[0],pathTexture);
            //levelOne = new LevelOne(2000,pathTexture);
            level.initialiseGraphics(spriteBatch, whiteRectangle);
            
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            cursor.worldLocation = new Vector2(Mouse.GetState().X + _camera.Position.X, Mouse.GetState().Y);
            cursor.spritePos = new Vector2(Mouse.GetState().X - 15, Mouse.GetState().Y - 15);

            _camera.Position += new Vector2(cameraScrollSpeed, 0) * deltaTime;

            foreach(Coin coin in Levels.coinList)
            {
                coin.checkIfCollected(cursor.worldLocation);

            }
            level.checkCollectedCoins();
            //obstacle.updatePosition(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || level.getFinished())
            {
                levelIndex++;
                if (levelIndex > levelList.Count - 1) levelIndex = levelList.Count - 1;
                level = new Levels(levelList[levelIndex], pathTexture);
                level.initialiseGraphics(spriteBatch, whiteRectangle);
                _camera.Position = new Vector2(0, 0);
            }
            level.finishCheck(new Rectangle((int)cursor.worldLocation.X,(int)cursor.worldLocation.Y,30,30));
            //Console.WriteLine("camera position is "+ _camera.Position.X + "," + _camera.Position.Y);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var viewMatrix = _camera.GetViewMatrix();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, _camera.GetViewMatrix());
            if (level.path.isPlayerInBounds(cursor.worldLocation))
            {
                GraphicsDevice.Clear(goodColor);
            }
            else { GraphicsDevice.Clear(badColor); }


            // TODO: Add your drawing code here
            
            level.Draw();
            cursor.Draw(spriteBatch);
            //spriteBatch.End();
            base.Draw(gameTime);
        }

        //void setLevelsToNull()
        //{
            
        //    levelOne = null;
        //    levelTwo = null;
        //}
    }
}
