using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PPM_Maze
{
    public class AfterLevelMenu : State
    {
        private List<Component> _components;
        SpriteFont fontDec;
        double accuracy;

        public AfterLevelMenu(bool pass, double accuracy, Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {

            var backButton = new Button(_content.Load<Texture2D>("Controls/backButton"))
            {
                Position = new Vector2(50, 50),
            };

            backButton.Click += backButton_Click;

            var levelSelect = new Button(_content.Load<Texture2D>("Controls/levelSelectButton"))
            {
                Position = new Vector2(410, 500),
            };

            levelSelect.Click += levelSelect_Click;

            _components = new List<Component>()
                {
                    backButton,
                    levelSelect
                };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            var myTexture = _content.Load<Texture2D>("Controls/levelComplete");
            var myRectangle = new Rectangle(360, 30, 556, 140);
            spriteBatch.Draw(myTexture, myRectangle, Color.White);
            var myTexture2 = _content.Load<Texture2D>("Controls/accuracy");
            var myRectangle2 = new Rectangle(390, 300, 410, 140);
            spriteBatch.Draw(myTexture2, myRectangle2, Color.White);


            string acc;
            //accuracy.ToString(acc);
            fontDec = _content.Load<SpriteFont>("Fonts/font");
            spriteBatch.DrawString(fontDec, "acc", new Vector2(830, 350), Color.White);




            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
        private void backButton_Click(object sender, System.EventArgs e)
        {
            if (Game1.audio)
            {
                Game1.buttonClick.Play();
            }
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
        private void playAgain_Click(object sender, System.EventArgs e)
        {
            if (Game1.audio)
            {
                Game1.buttonClick.Play();
            }
            _game.ChangeState(new ProceduralLevel(_game, _graphicsDevice, _content));
        }
        private void levelSelect_Click(object sender, System.EventArgs e)
        {
            if (Game1.audio)
            {
                Game1.buttonClick.Play();
            }
            _game.ChangeState(new levelSelect(_game, _graphicsDevice, _content));
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