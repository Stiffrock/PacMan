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
using System.IO;

namespace Pacman_v2
{
    class Ghost : GameObject
    {
        Random rand;
        int arrayIndexX;
        int arrayIndexY;

        List<Node> path;
        int pathLength;
        Node targetNode;
        int pathLeft;
        int counter;


        public Ghost(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            this.srcRec = new Rectangle(0, 16, 16, 16);
            this.pos = pos;
            this.rand = new Random();           
            this.dirList = new List<Direction>();
            dirList.Add(Direction.Right);
            dirList.Add(Direction.Left);
            dirList.Add(Direction.Up);
            dirList.Add(Direction.Down);
        }

        public override void SpriteTimer(GameTime gameTime)
        {
            base.SpriteTimer(gameTime);
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                srcRec.X = (srcRecCount % 8) * 16;
                srcRecCount ++;
            }
        }  
  

        public Node getNode()
        {
            arrayIndexX = (int)(pos.Y / 20);
            arrayIndexY = (int)(pos.X / 20);

            try
            {
                return Map.nodeArray[arrayIndexX, arrayIndexY];
            }
            catch (System.IndexOutOfRangeException)
            {
                return null;
            }
         
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rec, srcRec, Color.White);
        }

        public void FindPath()
        {
            Node node = getNode();
            for (int i = 0; i < Map.nodeArray.GetLength(0); i++)
            {
                for (int j = 0; j < Map.nodeArray.GetLength(1); j++)
                {
                    if (Map.nodeArray[i, j].passable == true)
                    {
                        targetNode = Map.nodeArray[i, j];
                        if (targetNode == PacPos)
                        {
                            
                        }
                    }
                }
            }
           
            //path = pathFinder.FindPath(getNode(), Map.nodeArray[10, 1]);
            path = pathFinder.FindPath(getNode(), targetNode);

            if (path == null)
                return;
            pathLength = path.Count();
            pathLeft = pathLength - 1;
            targetNode = path[pathLeft];
            
        }

        void GoToNextNode()
       {
           if (pathLeft != 0)
           {
               --pathLeft;
           }
           targetNode = path[pathLeft];
       
        
           MoveToTargetNode();
           Console.WriteLine("GoToNextNode");
        }

        void MoveToTargetNode()
        {         
            if (path == null)
                return;
           // Node currentNode = getNode();
            Node currentNode = targetNode.parent;
            Vector2 direction = new Vector2(targetNode.X, targetNode.Y) - new Vector2(currentNode.X, currentNode.Y);
            direction.Normalize();
         //   pos += direction;

            if ( pathLeft != 0)
            {
                rec.X += (int)direction.Y;
                rec.Y += (int)direction.X;
                counter += 1;

            }
     
            if (counter == 19 && pathLeft != 0)
            {
                currentNode = targetNode;
                GoToNextNode();
                counter = 0;
                Console.WriteLine(pathLeft.ToString());
            }

         /*   if (currentNode == targetNode)
                GoToNextNode();
            if (rec.X == targetNode.Y * 20)
                GoToNextNode();*/
 

        }

        public override void Update()
        
        {
            MoveToTargetNode();
            //if (currentDirection == Direction.Default)
            //{
            //    SetDirection(Direction.Down);
            //}

            // if (currentDirection == Direction.Stop)
            //{
            //    int i = rand.Next(0, 4);
            //    SetDirection(dirList[i]);
            //}        

            //if (currentDirection == Direction.Right)
            //{
            //    rec.X += 1;
            //}
            //if (currentDirection == Direction.Left)
            //{
            //    rec.X -= 1;
            //}
            //if (currentDirection == Direction.Up)
            //{
            //    rec.Y -= 1;
            //}
            //if (currentDirection == Direction.Down)
            //{
            //    rec.Y += 1;
            //}
            
        }
    }
}
