using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pacman_v2
{
    class Uber_Tile : Tile
    {
        public Uber_Tile(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            this.srcRec = new Rectangle(48, 96, 16, 16);
        }

    }
}
