using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        LevelOne levelOne;
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
            level = new Levels();
            

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
            coinTexture = this.Content.Load <Texture2D>("coin");
            Coin.setTexture(coinTexture);
            cursor = new Cursor(cursorTexture, new Vector2(400, 240));
            levelOne = new LevelOne(2000,pathTexture);
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
            cursor.Location = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            cursor.spritePos = new Vector2(Mouse.GetState().X - 15, Mouse.GetState().Y - 15);

            _camera.Position += new Vector2(cameraScrollSpeed, 0) * deltaTime;

            foreach(Coin coin in Levels.coinList)
            {
                coin.checkIfCollected(cursor.Location);

            }
            LevelOne.checkCollectedCoins();
            //obstacle.updatePosition(gameTime);

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
            if (Levels.path.isPlayerInBounds(cursor.Location))
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
    }
}
