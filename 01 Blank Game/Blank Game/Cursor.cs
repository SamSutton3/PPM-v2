using System;
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
        public Vector2 Location { get; set; }
        private Texture2D Texture;
        public Vector2 spritePos;
        //public Camera2D _camera = Game1._camera;

        public Cursor(Texture2D texture, Vector2 location)
        {
            Location = location;
            Texture = texture;
            spritePos = new Vector2(location.X - 15, location.Y - 15);
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
