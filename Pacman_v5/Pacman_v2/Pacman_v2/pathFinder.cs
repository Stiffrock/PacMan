using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_v2
{
    static class pathFinder
    {     
        static private List<Node> GetNeighbours(Node node) // Använder nodens index och söker de fyra positionerna runt sig (n, s, w, e) och lägger dem i en lista. Den gör bara detta om den är inom banans scope.
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

        static void ResetNodes() // Sätter besökta noder till obesökta och rensar alla kopplingar i noderna.
        {
            foreach(Node n in Map.nodeArray)
            {
                n.visited = false;
                n.parent = null;
            }
        }

        static public MyStack FindPath(Node startNode, Node goalNode) //En bredden först sökning som öppnar alla noder som läggs i neighbour och om de inte redan är besökta och går att passeera läggs den i en separat lista.
                                                                      //Om den träffar målnoden går den tillbaka längs vägen genom att kolla på nodens förälder och hela tiden pusha den noden till stacken.
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
                        pathStack.Push(currentNode);
                        currentNode = currentNode.parent;
                    }
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
