using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blank_Game
{
    class UI
    {
        Vector2 healthBarPosition = new Vector2(0,0);
        Vector2 healthBarWidthHeight = new Vector2(700, 100);
        Vector2 timerPosition = new Vector2(800,0);

        int healthValue;
        int maxHealth;
        double healthRatio;
        String time;
        SpriteFont spriteFont;

        Rectangle healthBarRect;
        Rectangle currentHealthRect;

        Texture2D whiteTexture;
        public UI(Texture2D pixel,SpriteFont font ,int hpMax)
        {
            whiteTexture = pixel;
            spriteFont = font;
            healthBarRect = new Rectangle((int)healthBarPosition.X,(int)healthBarPosition.Y,(int)healthBarWidthHeight.X,(int)healthBarWidthHeight.Y);
            maxHealth = hpMax;
            healthRatio = 1;
            time = "0";
        }

        public void updateUI(int newHealth, String newTime)
        {
            healthValue = newHealth;
            time = newTime;
            healthRatio = (newHealth / maxHealth);
            currentHealthRect = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y,(int)(healthBarWidthHeight.X * healthRatio),(int)healthBarWidthHeight.Y);
        }

        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(whiteTexture, healthBarRect, Color.Red);
            spriteBatch.Draw(whiteTexture, currentHealthRect, Color.Green);
            spriteBatch.DrawString(spriteFont, time, timerPosition , Color.White);

        }
    }
}
