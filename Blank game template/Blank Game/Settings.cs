using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blank_Game
{
    public class Settings : State
    {
        private List<Component> _components;

        public Settings(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var backButton = new Button(_content.Load<Texture2D>("Controls/backButton"))
            {
                Position = new Vector2(50, 50),
            };

            backButton.Click += backButton_Click;

            var whiteButton = new Button(_content.Load<Texture2D>("Controls/whiteButton"))
            {
                Position = new Vector2(600, 295),
            };

            whiteButton.Click += whiteButton_Click;

            var goldButton = new Button(_content.Load<Texture2D>("Controls/goldButton"))
            {
                Position = new Vector2(800, 295),
            };

            goldButton.Click += goldButton_Click;

            var blueButton = new Button(_content.Load<Texture2D>("Controls/blueButton"))
            {
                Position = new Vector2(1000, 295),
            };

            blueButton.Click += blueButton_Click;


            var toggleOnButton = new Button(_content.Load<Texture2D>("Controls/onButton"))
            {
                Position = new Vector2(600, 535),
            };

            toggleOnButton.Click += toggleOnButton_Click;

            var toggleOffButton = new Button(_content.Load<Texture2D>("Controls/offButton"))
            {
                Position = new Vector2(800, 535),
            };

            toggleOffButton.Click += toggleOffButton_Click;

            _components = new List<Component>()
                {
                    backButton,
                    whiteButton,
                    goldButton,
                    blueButton,
                    toggleOnButton,
                    toggleOffButton

                };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            var myTexture = _content.Load<Texture2D>("Controls/settingsLabel");
            var myRectangle = new Rectangle(390, 30, 571, 140);
            spriteBatch.Draw(myTexture, myRectangle, Color.White);
            var myTexture2 = _content.Load<Texture2D>("Controls/colourLabel");
            var myRectangle2 = new Rectangle(150, 300, 418, 140);
            spriteBatch.Draw(myTexture2, myRectangle2, Color.White);
            var myTexture3 = _content.Load<Texture2D>("Controls/audioLabel");
            var myRectangle3 = new Rectangle(150, 540, 418, 140);
            spriteBatch.Draw(myTexture3, myRectangle3, Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
        private void backButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void whiteButton_Click(object sender, System.EventArgs e)
        {
            string path = "../../../../colour.txt";
            string strColour = "White";
            System.IO.File.WriteAllText(path, strColour);

        }

        private void goldButton_Click(object sender, System.EventArgs e)
        {
            string path = "../../../../colour.txt";
            string strColour = "Gold";
            System.IO.File.WriteAllText(path, strColour);
        }

        private void blueButton_Click(object sender, System.EventArgs e)
        {
            string path = "../../../../colour.txt";
            string strColour = "Blue";
            System.IO.File.WriteAllText(path, strColour);
        }

        private void toggleOnButton_Click(object sender, System.EventArgs e)
        {
            // TO DO
        }

        private void toggleOffButton_Click(object sender, System.EventArgs e)
        {
            // TO DO
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}