using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_v2
{
    class Node // Nod klassen som får ett x, y index från arrayen. Två bools, en som säger om den går att passera (är en floor tile) och en som håller reda på om noden har blivit besökt.
    {
        public Node parent;
        public bool passable, visited;
        public int X, Y;


        public Node(bool passable, int Cost, int X, int Y)
        {
            this.passable = passable;
            this.X = X;
            this.Y = Y;
        }

    }
}
