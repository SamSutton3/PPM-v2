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
    public class AfterProceduralMenu : State
    {
        private List<Component> _components;
        SpriteFont fontDec;
        float totalTime;

        public AfterProceduralMenu(bool pass, float time, Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            totalTime = time;
            var backButton = new Button(_content.Load<Texture2D>("Controls/backButton"))
            {
                Position = new Vector2(50, 50),
            };

            backButton.Click += backButton_Click;

            var playAgain = new Button(_content.Load<Texture2D>("Controls/playAgain"))
            {
                Position = new Vector2(410, 500),
            };

            playAgain.Click += playAgain_Click;

            _components = new List<Component>()
                {
                    backButton,
                    playAgain
                };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            var myTexture = _content.Load<Texture2D>("Controls/gameOver");
            var myRectangle = new Rectangle(360, 30, 571, 140);
            spriteBatch.Draw(myTexture, myRectangle, Color.White);
            var myTexture2 = _content.Load<Texture2D>("Controls/accuracy");
            var myRectangle2 = new Rectangle(390, 300, 410, 140);
            spriteBatch.Draw(myTexture2, myRectangle2, Color.White);




            string acc;
            acc = "Time: " + totalTime.ToString() + " seconds";
            //accuracy.ToString(acc);
            fontDec = _content.Load<SpriteFont>("Fonts/font");
            spriteBatch.DrawString(fontDec, acc, new Vector2 (830,350), Color.White );





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