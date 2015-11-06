﻿using System;
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

        List<Node> path;
        Vector2 targetDirection;
        MyStack pathStack;
        int pathLength;
        Node targetNode;
        GameObject target;
        int pathLeft;
        int counter = 20;


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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rec, srcRec, Color.White);
        }

        public void FindPath()
        {
            //Node node = getNode();
            //for (int i = 0; i < Map.nodeArray.GetLength(0); i++)
            //{
            //    for (int j = 0; j < Map.nodeArray.GetLength(1); j++)
            //    {
            //        if (Map.nodeArray[i, j].passable == true)
            //        {
            //            targetNode = Map.nodeArray[i, j];
            //            if (targetNode == PacPos)
            //            {
                            
            //            }
            //        }
            //    }
            //}
           
            //path = pathFinder.FindPath(getNode(), Map.nodeArray[10, 1]);
            //path = pathFinder.FindPath(getNode(), targetNode);
            //targetNode = Map.nodeArray[1, 1];
            if (target == null)
                return;

            Node n = getNode();
            Node m = target.getNode();
            pathStack = pathFinder.FindPath(n, target.getNode());
            //pathStack = pathFinder.FindPath(n, Map.nodeArray[2, 14]);

            if (pathStack == null || pathStack.Count() == 0)
                return;
            targetNode = (Node)pathStack.Pop();
            targetDirection = new Vector2(targetNode.X, targetNode.Y) - new Vector2(n.X, n.Y);

            if (targetDirection != Vector2.Zero)
                targetDirection.Normalize();
            else
                targetDirection = Vector2.Zero;
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
            if (targetNode == null)
            {
                //FindPath();
                return;
            }

            Node n = getNode();
            if (counter == 20)
            {
                if(pathStack == null || pathStack.Count() == 0)
                    FindPath();

                rec.X = targetNode.Y * 20;
                rec.Y = targetNode.X * 20;

                if (pathStack != null && pathStack.Count() != 0)
                    targetNode = (Node)pathStack.Pop();

                targetDirection = new Vector2(targetNode.X, targetNode.Y) - new Vector2(n.X, n.Y);
                if (targetDirection != Vector2.Zero)
                    targetDirection.Normalize();
                counter = 0;
            }
            else
            {
                rec.X += (int)targetDirection.Y;
                rec.Y += (int)targetDirection.X;
                counter++;
            }
            //if ( pathLeft != 0)
            //{
            //    rec.X += (int)targetDirection.Y;
            //    rec.Y += (int)targetDirection.X;
            //    counter += 1;

            //}
     
            //if (counter == 19 && pathLeft != 0)
            //{
            //    currentNode = targetNode;
            //    GoToNextNode();
            //    counter = 0;
            //    Console.WriteLine(pathLeft.ToString());
            //}

         /*   if (currentNode == targetNode)
                GoToNextNode();
            if (rec.X == targetNode.Y * 20)
                GoToNextNode();*/
 

        }
        public void SetTarget(GameObject target)
        {
            this.target = target;
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
