using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blank_Game
{
    public class Menu1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Color _backgroundColour = Color.CornflowerBlue;

        private List<Component> _gameComponents;

        public Menu1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var levelPlayButton = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(350, 200),
                Text = "Level Play",
            };

            levelPlayButton.Click += LevelPlayButton_Click;

            _gameComponents = new List<Component>()
            {
                levelPlayButton,

            };

            base.LoadContent();
        }
        private void LevelPlayButton_Click(object sender, System.EventArgs e)
        {

            throw new System.NotImplementedException("Here is where it will link to the Level Play Menu");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var component in _gameComponents)
                component.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw (GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColour);
            spriteBatch.Begin();
            foreach (var component in _gameComponents)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);


        }
    }
}