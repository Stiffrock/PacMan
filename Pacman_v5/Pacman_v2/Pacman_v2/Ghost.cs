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

        Vector2 targetDirection;
        MyStack pathStack;
        Node targetNode;
        GameObject target;
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

        public void FindPath() //Initierar en bredden först sökning som returnerar en stack. Hittar en riktning från noden i stacken. Normaliserar värdet sedan värdet och sätyter det till spökets riktning.
        {
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


        void MoveToTargetNode() //Använder en counter för att gå ett antal pixlar i spökets givna riktning för att sedan byta nod.
        {
            Node n = getNode();

            if (counter == 20)
            {
                if(pathStack == null || pathStack.Count() == 0) // Om vi inte har någon stack skapar vi en ny med en ny väg till målet.
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
        }
        public void SetTarget(GameObject target)
        {
            this.target = target;
        }
        public override void Update()
        {
            MoveToTargetNode();           
        }
    }
}
