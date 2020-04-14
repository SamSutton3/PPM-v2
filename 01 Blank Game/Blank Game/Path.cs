using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PPM_Maze
{
    public class Path
    {
        private int width;
        public static int height;

        public static  Vector2 startPos;

        private Texture2D Texture;

        private Camera2D _camera = Game1._camera;
        

        public Path(int W, int H,Texture2D texture, int Y = 100)
        {
            width = W;
            height = H;
            startPos.Y = Y;
            Texture = texture;

        }

        public Boolean isPlayerInBounds(Vector2 location)
        {
            if (location.Y < startPos.Y || location.Y > (startPos.Y + height))
            {
                return false;
            }
            else { return true; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            var viewMatrix = _camera.GetViewMatrix();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, _camera.GetViewMatrix());
            //spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap);
            spriteBatch.Draw(Texture, startPos, new Rectangle((int)startPos.X,(int)startPos.Y,width,height), Color.White);
            spriteBatch.End();
        }
    }
}
