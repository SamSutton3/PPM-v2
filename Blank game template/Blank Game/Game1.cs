using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PPM_Maze
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int windowWidth = 1200;
        public static int windowHeight = 800;
        private State _currentState;

        public static Song menuMusic;
        public static SoundEffect buttonClick;
        public static SoundEffect coin;
        public static bool audio = true;
        private State _nextState;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public static void playButtonClick()
        {
            buttonClick.Play();
        }

        public static void playCoinGrab()
        {
            coin.Play();
        }

        public static void toggleMenuMusic(bool toggle = true)
        {
            if (toggle)
            {
                MediaPlayer.Play(menuMusic);
            }
            else
            {
                MediaPlayer.Stop();
            }
        }

        public static void toggleAudio(bool toggle)
        {
            audio = toggle;
            toggleMenuMusic(toggle);
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;
            Content.RootDirectory = "Content";

            menuMusic = Content.Load<Song>("Sounds/Background-Menu3");
            buttonClick = Content.Load<SoundEffect>("Sounds/Click2");
            coin = Content.Load<SoundEffect>("Sounds/Coins3");
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}