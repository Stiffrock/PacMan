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
    class GameObject
    {
        public enum Direction
        { Up, Down, Left, Right, Default, Stop};

        public Direction currentDirection = Direction.Stop;
        public List<Direction> dirList;
        public Texture2D tex;
        public Vector2 pos;
        public float rotation;
        public double frameTimer, frameInterval;
        public int srcRecCount;
        public Rectangle rec, srcRec;
        public int score;
        public int bonusscore;
        public int lifeCount;
        public Node PacPos;

        
        public GameObject(Texture2D tex, Vector2 pos)
        {
            this.tex = tex;
            this.pos = pos;
            this.score = 0;
            this.frameTimer = 100;
            this.frameInterval = 200;
            this.srcRecCount = 0;
            this.srcRec = new Rectangle(0, 0, 0, 0);
            this.rec = new Rectangle((int)pos.X, (int)pos.Y, 16, 16);
        }

        public void SetDirection(Direction dir)
        {
            currentDirection = dir;
        }
                
        public Node getNode()
        {
            int arrayIndexX = (int)((rec.Y+5) / 20f);
            int arrayIndexY = (int)((rec.X+5) / 20f);

            try
            {
                return Map.nodeArray[arrayIndexX, arrayIndexY];
            }
            catch (System.IndexOutOfRangeException)
            {
                return null;
            }
        }

        public virtual void SpriteTimer(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
        }
            
        public virtual void Draw(SpriteBatch spriteBatch)
        {        }

        public virtual void Update()
        { }

        public virtual void CheckTileCol(List<Tile> tileList)
        {
            foreach (Tile item in tileList)
            {
                if (rec.Intersects(item.rec))
                {
                    tileList.Remove(item);
                    score++;
                    break;
                }
            }
        }
        public virtual void CheckBonusCol(List<Tile> tileList)
        {
            foreach (Tile item in tileList)
            {
                if (rec.Intersects(item.rec))
                {
                    tileList.Remove(item);
                    bonusscore += 50;
                    break;
                }
            }
        }
        public virtual void CheckUberCol(List<Tile> tileList)
        {
            foreach (Tile item in tileList)
            {
                if (rec.Intersects(item.rec))
                {
                    tileList.Remove(item);
                    
                    break;
                }
            }
        }
        public virtual void CheckSpecialCol(List<Tile> tileList) // Checks collision with pacman and special tile and sets pacmans rectangle to pos.
        {
            foreach (Tile item in tileList)
            {
                if (rec.Intersects(item.rec))
                {
                    if (item == tileList[1]) // Right and left special tile
                    {
                        rec.X = (int)tileList[2].pos.X +16;
                        rec.Y = (int)tileList[2].pos.Y-5;
                        break;
                    }
                    if (item == tileList[2])   
                    {
                        rec.X = (int)tileList[1].pos.X -24;
                        rec.Y = (int)tileList[1].pos.Y-5;
                        break;
                    }

                    if (item == tileList[0])    // Up and down special tile
                    {
                        rec.X = (int)tileList[3].pos.X - 5;
                        rec.Y = (int)tileList[3].pos.Y - 24;
                        break;
                    }
                    if (item == tileList[3])
                    {
                        rec.X = (int)tileList[0].pos.X - 5;
                        rec.Y = (int)tileList[0].pos.Y + 16;
                        break;
                    }
                
                }
            }
        }
        public virtual void CheckObjCol(List<GameObject> objList)
        {
            foreach (GameObject item in objList)
            {
                if (rec.Intersects(item.rec))
                {
                    if (lifeCount != 0)
                    {
                        lifeCount -= 1;
                        rec.X = (int)pos.X;
                        rec.Y = (int)pos.Y;
                        break;                      
                    }           
                }
            }
        }

        public virtual void CheckDirCol(List<Tile> list) //Check if pacmans direction collides with wall
        {
            foreach (Tile item in list)
            {
                if (rec.Intersects(item.rec))
                {
                    if (currentDirection == Direction.Right)
                    {                       
                        rec.X -= 1;
                        SetDirection(Direction.Stop);
                    }
                    if (currentDirection == Direction.Left)
                    {                    
                        rec.X += 1;
                        SetDirection(Direction.Stop);
                    }
                    if (currentDirection == Direction.Up)
                    {                       
                        rec.Y += 1;
                        SetDirection(Direction.Stop);
                    }
                    if (currentDirection == Direction.Down)
                    {                     
                        rec.Y -= 1;
                        SetDirection(Direction.Stop);
                    }
                }
            }
        }
    }
}
