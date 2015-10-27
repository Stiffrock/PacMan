using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Pacman_v2
{
    class Tile
    {
        Texture2D tex;
        public Vector2 pos;
        public Rectangle srcRec, rec;

        public Tile(Texture2D tex, Vector2 pos)
        {
            this.tex = tex;
            this.pos = pos;
            this.rec = new Rectangle((int)pos.X, (int)pos.Y, 16, 16);
            this.srcRec = new Rectangle(0, 0, 0, 0);          
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height), srcRec, Color.White);
        }
    }
}
