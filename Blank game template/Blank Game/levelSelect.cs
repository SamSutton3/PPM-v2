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
    public class levelSelect : State
    {
        private List<Component> _components;

        public levelSelect(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var backButton = new Button(_content.Load<Texture2D>("Controls/backButton"))
            {
                Position = new Vector2(50, 50),
            };

            backButton.Click += backButton_Click;

            var levelOneButton = new Button(_content.Load<Texture2D>("Controls/levelButton1"))
            {
                Position = new Vector2(300, 200),
            };

            levelOneButton.Click += levelOneButton_Click;

            var levelTwoButton = new Button(_content.Load<Texture2D>("Controls/levelButton2"))
            {
                Position = new Vector2(500, 200),
            };

            levelTwoButton.Click += levelTwoButton_Click;

            var levelThreeButton = new Button(_content.Load<Texture2D>("Controls/levelButton3"))
            {
                Position = new Vector2(700, 200),
            };

            levelThreeButton.Click += levelThreeButton_Click;

            var levelFourButton = new Button(_content.Load<Texture2D>("Controls/levelButton4"))
            {
                Position = new Vector2(900, 200),
            };

            levelFourButton.Click += levelFourButton_Click;

            var levelFiveButton = new Button(_content.Load<Texture2D>("Controls/levelButton5"))
            {
                Position = new Vector2(300, 400),
            };
            levelFiveButton.Click += levelFiveButton_Click;

            var levelSixButton = new Button(_content.Load<Texture2D>("Controls/levelButton6"))
            {
                Position = new Vector2(500, 400),
            };
            levelSixButton.Click += levelSixButton_Click;

            var levelSevenButton = new Button(_content.Load<Texture2D>("Controls/levelButton7"))
            {
                Position = new Vector2(700, 400),
            };
            levelSevenButton.Click += levelSevenButton_Click;

            var levelEightButton = new Button(_content.Load<Texture2D>("Controls/levelButton8"))
            {
                Position = new Vector2(900, 400),
            };
            levelEightButton.Click += levelEightButton_Click;

            var levelNineButton = new Button(_content.Load<Texture2D>("Controls/levelButton9"))
            {
                Position = new Vector2(300, 600),
            };
            levelNineButton.Click += levelNineButton_Click;

            var levelTenButton = new Button(_content.Load<Texture2D>("Controls/levelButton10"))
            {
                Position = new Vector2(500, 600),
            };
            levelTenButton.Click += levelTenButton_Click;

            var levelElevenButton = new Button(_content.Load<Texture2D>("Controls/levelButton11"))
            {
                Position = new Vector2(700, 600),
            };
            levelElevenButton.Click += levelElevenButton_Click;

            var levelTwelveButton = new Button(_content.Load<Texture2D>("Controls/levelButton12"))
            {
                Position = new Vector2(900, 600),
            };
            levelTwelveButton.Click += levelTwelveButton_Click;


        



            _components = new List<Component>()
                {
                    backButton,
                    levelOneButton,
                    levelTwoButton,
                    levelThreeButton,
                    levelFourButton,
                    levelFiveButton,
                    levelSixButton,
                    levelSevenButton,
                    levelEightButton,
                    levelNineButton,
                    levelTenButton,
                    levelElevenButton,
                    levelTwelveButton
                };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            var myTexture = _content.Load<Texture2D>("Controls/levelSelect");
            var myRectangle = new Rectangle(390, 30, 571, 140);
            spriteBatch.Draw(myTexture, myRectangle, Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
        private void backButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
        private void levelOneButton_Click(object sender, System.EventArgs e)
        {

            // Load Level One
        }

        private void levelTwoButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Two
            
        }

        private void levelThreeButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Three

        }
        private void levelFourButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Four

        }

        private void levelFiveButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Five

        }

        private void levelSixButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Six

        }

        private void levelSevenButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Seven

        }

        private void levelEightButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Eight

        }

        private void levelNineButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Nine

        }
        private void levelTenButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Ten

        }
        private void levelElevenButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Eleven

        }
        private void levelTwelveButton_Click(object sender, System.EventArgs e)
        {
            // Load Level Twelve

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