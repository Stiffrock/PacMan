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
    class Food_Tile : Tile
    {     
        public Food_Tile(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            this.pos.X += 5;
            this.pos.Y += 5;
            this.rec = new Rectangle((int)pos.X, (int)pos.Y, 16, 16);
            this.srcRec = new Rectangle(0, 0, 4, 4);          
        }
    }
}
