﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PPM_Maze
{
    public class Obstacle
    {
        /*
         * To implement:
         * Vector2 position
         * width + height
         * shape?
         * Function to check for collision
         * draw obstacle good colour/ bad colour depending on collision
         */
        protected Vector2 position;
        protected Vector2 widthHeight;
        
        public Rectangle bounds;
        
        
        public Obstacle()
        {

        }
        public Obstacle(int posX, int posY, int w, int h)
        {
            position.X = posX;
            position.Y = posY;
            widthHeight.X = w;
            widthHeight.Y = h;
            bounds = new Rectangle(posX, posY, w, h);

        }

        public Vector2 getWidthHeight()
        {
            return widthHeight;
        }
        
        public Vector2 getPosition()
        {
            return position;
        }

        public Boolean isPlayerInBounds(Vector2 location)
        {
            if ((location.X > position.X && location.X < position.X + widthHeight.X) && 
                (location.Y > position.Y && location.Y < position.Y + widthHeight.Y))
            {
                return true;
            }
            else { return false; }
        }

        public void updatePosition(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= Levels.cameraScrollSpeed*deltaTime;
        }

        public void Draw(SpriteBatch spritebatch, Texture2D whiteRect, Vector2 cursorPos , Camera2D camera)
        {
            var viewMatrix = camera.GetViewMatrix();
            spritebatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, camera.GetViewMatrix());
            if (isPlayerInBounds(cursorPos))
            {
                spritebatch.Draw(whiteRect, position, new Rectangle((int)position.X, (int)position.Y, (int)widthHeight.X, (int)widthHeight.Y), Levels.badColor);
            }
            else
            {
                spritebatch.Draw(whiteRect, position, new Rectangle((int)position.X, (int)position.Y, (int)widthHeight.X, (int)widthHeight.Y), Levels.goodColor);
            }
            spritebatch.End();
        }
    }

    public class Coin : Obstacle
    {
        private static Texture2D coinTexture;
        public bool isAvailable = true;

        public Coin(int posX, int posY)
        {
            widthHeight.X = 50;
            widthHeight.Y = 50;
            position.X = posX;
            position.Y = posY;
            
        }

        public static void setTexture(Texture2D texture)
        {
            coinTexture = texture;
        }

        public void checkIfCollected(Vector2 location)
        {
            if (isAvailable == true && isPlayerInBounds(location))
            {
                if (Game1.audio)
                {
                    Game1.playCoinGrab();
                }
                isAvailable = false;
            }
        }

        public void Draw(SpriteBatch spritebatch, Camera2D camera)
        {
            var viewMatrix = camera.GetViewMatrix();
            spritebatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, camera.GetViewMatrix());
            if(isAvailable)
            {
                spritebatch.Draw(Levels.coinTexture, position, Color.White);
            }
            spritebatch.End();
        }
    }
}
