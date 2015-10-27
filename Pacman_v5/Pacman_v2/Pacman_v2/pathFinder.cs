using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_v2
{
    static class pathFinder
    {



        static private List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            int x = node.X;
            int y = node.Y;
                if (x != 14)
                neighbours.Add(Map.nodeArray[x + 1, y]);
                if (y != 24)
                neighbours.Add(Map.nodeArray[x, y + 1]);
                if (x != 0)
                neighbours.Add(Map.nodeArray[x - 1, y]);
                if (y != 0)
                neighbours.Add(Map.nodeArray[x, y - 1]);
            return neighbours;
        }



        static public List<Node> FindPath(Node startNode, Node goalNode)
        {
            Node currentNode = startNode;
          //  List<Node> visitedNodes = new List<Node>();
            List<Node> candidates = new List<Node>();
            candidates.Add(currentNode);

            for (int a = 0; a < 1000; a++)
            {                    
                if (currentNode == goalNode)
                {
                    List<Node> path = new List<Node>();
                    for (int i = 0; i < 500; i++)
                    {
                        if (currentNode == startNode)
                            break;
                        path.Add(currentNode);
                        currentNode = currentNode.parent;
                    }
                    return path;
                }

                List<Node> neighbours = GetNeighbours(currentNode);
                for (int i = 0; i < neighbours.Count(); i++)
                {
                   /* if (visitedNodes.Count == 0)
                    {                     
                        candidates.Add(neighbours[i]);
                        neighbours[i].parent = currentNode;
                    }*/

                    if (!neighbours[i].visited && neighbours[i].passable)
                    {
                        candidates.Add(neighbours[i]);
                        neighbours[i].parent = currentNode;
                    }
                /*    for (int j = 0; j < visitedNodes.Count(); j++)
                    {
                        if (neighbours[i] == visitedNodes[j]
                            || !neighbours[i].passable)
                            continue;

                        candidates.Add(neighbours[i]);
                        neighbours[i].parent = currentNode;
                    }*/

         
                }

                //visitedNodes.Add(currentNode);
                currentNode.visited = true;
                candidates.Remove(currentNode);
                currentNode = candidates[0];
            }

            return null;
        }
    }
}
