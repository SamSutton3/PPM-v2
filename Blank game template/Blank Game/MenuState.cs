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
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            Game1.toggleMenuMusic();

            var levelPlayButton = new Button(_content.Load<Texture2D>("Controls/levelPlay"))
            {
                Position = new Vector2(300, 275),
                
            };

            levelPlayButton.Click += LevelPlayButton_Click;

            var continuousPlayButton = new Button(_content.Load<Texture2D>("Controls/continuousPlay"))
            {
                Position = new Vector2(300, 425),
            };

            continuousPlayButton.Click += ContinuousPlayButton_Click;


            var settingsButton = new Button(_content.Load<Texture2D>("Controls/settingsButton"))
            {
                Position = new Vector2(380, 575),
            };

            settingsButton.Click += SettingsButton_Click;

            var quitButton = new Button(_content.Load<Texture2D>("Controls/exitButton"))
            {
                Position = new Vector2(620, 575),
            };

            quitButton.Click += QuitButton_Click;

            _components = new List<Component>()
                {
                    levelPlayButton,
                    continuousPlayButton,
                    settingsButton,
                    quitButton,
                };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            var myTexture = _content.Load<Texture2D>("Controls/gameTitle2");
            var myRectangle = new Rectangle(340, 10, 500, 240);
            spriteBatch.Draw(myTexture, myRectangle, Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void LevelPlayButton_Click(object sender, System.EventArgs e)
        {
            if (Game1.audio)
            {
                Game1.buttonClick.Play();
            }
            _game.ChangeState(new levelSelect(_game, _graphicsDevice, _content));

        }
        private void ContinuousPlayButton_Click(object sender, System.EventArgs e)
        {
            if (Game1.audio)
            {
                Game1.buttonClick.Play();
            }
            _game.ChangeState(new ProceduralLevel(_game, _graphicsDevice, _content));
        }
        private void SettingsButton_Click(object sender, System.EventArgs e)
        {
            if (Game1.audio)
            {
                Game1.buttonClick.Play();
            }
            _game.ChangeState(new Settings(_game, _graphicsDevice, _content));
        }
        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            if (Game1.audio)
            {
                Game1.buttonClick.Play();
            }
            _game.Exit();

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
