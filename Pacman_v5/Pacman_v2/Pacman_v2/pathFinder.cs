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

        static void ResetNodes()
        {
            foreach(Node n in Map.nodeArray)
            {
                n.visited = false;
                n.parent = null;
            }
        }

        static public MyStack FindPath(Node startNode, Node goalNode)
        {
            ResetNodes();
            Node currentNode = startNode;
            List<Node> candidates = new List<Node>();
            candidates.Add(currentNode);
            MyStack pathStack = new MyStack();

            for (int a = 0; a < 1000; a++)
            {                    
                if (currentNode == goalNode)
                {
                    List<Node> path = new List<Node>();

                    for (int i = 0; i < 500; i++)
                    {
                        if (currentNode == startNode)
                            break;
                        //path.Add(currentNode);
                        pathStack.Push(currentNode);
                        currentNode = currentNode.parent;
                    }
                 //   return path;
                    return pathStack;
                }

                List<Node> neighbours = GetNeighbours(currentNode);
                for (int i = 0; i < neighbours.Count(); i++)
                {

                    if (!neighbours[i].visited && neighbours[i].passable)
                    {
                        candidates.Add(neighbours[i]);
                        neighbours[i].parent = currentNode;
                    }       
                }

                currentNode.visited = true;
                candidates.Remove(currentNode);
                if(candidates.Count > 0)
                currentNode = candidates[0];
            }
            return null;
        }
    }
}
