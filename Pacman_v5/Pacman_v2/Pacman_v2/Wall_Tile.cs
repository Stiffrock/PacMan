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
    class Wall_Tile : Tile
    {             
        public Wall_Tile(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            this.srcRec = new Rectangle(0, 0, 16, 16);          
        }
    }
}
