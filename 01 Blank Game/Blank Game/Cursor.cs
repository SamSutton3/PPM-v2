﻿using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PPM_Maze
{
    public class Cursor
    {
        public Vector2 worldLocation { get; set; }
        private Texture2D Texture;
        public Vector2 spritePos;
        //public Camera2D _camera = Game1._camera;

        public Cursor(Texture2D texture, Vector2 screenLocation, Camera2D camera)
        {
            worldLocation = Vector2.Add(screenLocation, camera.Position); 
            Texture = texture;
            spritePos = new Vector2(screenLocation.X - 15, screenLocation.Y - 15);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, _camera.GetViewMatrix());
            spriteBatch.Draw(Texture, spritePos, Color.White);
            spriteBatch.End();
        }
    }
}
