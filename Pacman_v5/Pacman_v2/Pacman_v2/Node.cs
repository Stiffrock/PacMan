using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_v2
{


    class Node
    {
        public Node parent;
        public bool passable, visited;
        public int X, Y, Cost;


        public Node(bool passable, int Cost, int X, int Y)
        {
            this.passable = passable;
            this.X = X;
            this.Y = Y;
        }

    }
}
