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
    class Map
    {
        public enum GameState
        {Menu, Running, Win, End, Restart, Default}

        public GameState currentState = GameState.Menu;      
        public Texture2D tex1, tex2, tex3;
        private List<String> list;
        public List<GameObject> ghostList = new List<GameObject>();
        public List<GameObject> objectList = new List<GameObject>();
        public List<Tile> wallList = new List<Tile>();
        public List<Tile> specialWall = new List<Tile>();
        public List<Tile> bonusList = new List<Tile>();
        public List<Tile> floorList = new List<Tile>();
        public List<Tile> uberList = new List<Tile>();
        public static Node[,] nodeArray;
        public GameObject ghost, pacman;
        public StreamReader streamReader;     
        public SpriteFont spriteFont;
        public int maxScore;
        Camera cam;
        String StatusAlert;
        GameWindow gameWindow;


        public Map(Texture2D tex1, Texture2D tex2, Texture2D tex3, List<String> list, SpriteFont spriteFont, GraphicsDevice graphics)
        {
            this.tex1 = tex1;
            this.tex2 = tex2;
            this.tex3 = tex3;
            this.list = list;
            this.maxScore = floorList.Count();
            this.spriteFont = spriteFont;
            this.streamReader = new StreamReader("map.txt");
            cam = new Camera(graphics.Viewport);
        }

        protected void ReadString(List<String> list) // Reads textfile and adds it to a list
        {
            while (!streamReader.EndOfStream)
                {
                    list.Add(streamReader.ReadLine());
                
                }                                            
        }
        public void CreateMap(List<String> list) // Reads the textfile and creates a tile or gameobject
        {
            ReadString(list);
            int Cost = 1;
            nodeArray = new Node[list.Count, list[1].Length];
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Length; j++)
                {
                    if (list[i][j] == 'x')
                    {
                        nodeArray[i, j] = new Node(false, Cost, i, j);
                        wallList.Add(new Wall_Tile(tex2, new Vector2((j * 20), (i * 20))));
                    }
                    if (list[i][j] == 'f')
                    {
                        nodeArray[i, j] = new Node(true, Cost, i, j);
                        floorList.Add(new Food_Tile(tex3, new Vector2((j * 20), (i * 20))));
                    }
                    if (list[i][j] == 's')
                    {
                        nodeArray[i, j] = new Node(true, Cost, i, j);
                        floorList.Add(new Food_Tile(tex2, new Vector2((j * 20), (i * 20))));
                        specialWall.Add(new Food_Tile(tex2, new Vector2((j * 20), (i * 20))));
                    }
                    if (list[i][j] == 'b')
                    {
                        nodeArray[i, j] = new Node(true, Cost, i, j);
                        floorList.Add(new Bonus_Tile(tex1, new Vector2((j * 20), (i * 20))));
                        bonusList.Add(new Bonus_Tile(tex1, new Vector2((j * 20), (i * 20))));
                    }
                    if (list[i][j] == 'u')
                    {
                        nodeArray[i, j] = new Node(true, Cost, i, j);
                        floorList.Add(new Uber_Tile(tex1, new Vector2((j * 20), (i * 20))));
                        uberList.Add(new Uber_Tile(tex1, new Vector2((j * 20), (i * 20))));
                    }
                    if (list[i][j] == 'g')
                    {
                        nodeArray[i, j] = new Node(true, Cost, i, j);
                        ghost = new Ghost(tex1, new Vector2((j * 20), (i * 20)));
                        ghostList.Add(ghost);
                        objectList.Add(ghost);
                    }
                    if (list[i][j] == 'p')
                    {
                        nodeArray[i, j] = new Node(true, Cost, i, j);
                        pacman = new Pacman(tex1, new Vector2((j * 20), (i * 20)));
                        objectList.Add(pacman);
                    }                 
                }
            }
            Ghost g = (Ghost)ghostList[0];
            g.FindPath();
            maxScore = floorList.Count();
        }
        public void Menu(SpriteBatch spriteBatch) 
        {
            if (currentState == GameState.Menu)
            {
                spriteBatch.DrawString(spriteFont, "Menu  \n \n Press 1 to Start: Map 1", new Vector2(150, 150), Color.Orange);
            }           
        }

       public void DrawMap(SpriteBatch spriteBatch)
        {
            if (currentState == GameState.Running)
            {
                foreach (GameObject item in objectList)
                {
                    item.Draw(spriteBatch);
                }
                foreach (Tile item in floorList)
                {
                    item.Draw(spriteBatch);
                }
                foreach (Tile item in wallList)
                {
                    item.Draw(spriteBatch);
                }                                
            }                              
        }

       public void DrawText(SpriteBatch spriteBatch)
       {
           if (currentState == GameState.Running || currentState == GameState.End || currentState == GameState.Win)
           {
               spriteBatch.DrawString(spriteFont, "" + StatusAlert, new Vector2(370, 100), Color.Orange);
               spriteBatch.DrawString(spriteFont, "Score:" + (pacman.score+pacman.bonusscore) * 10 + "\nRemaining life:" + pacman.lifeCount, new Vector2(150, 370), Color.Orange);
           }
       }                 

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cam.GetTransform());  
  
            DrawText(spriteBatch);
            Menu(spriteBatch);
            DrawMap(spriteBatch);
            spriteBatch.End();
        }
        public void ClearMap()
        {
            floorList = new List<Tile>();
            wallList = new List<Tile>();
            objectList = new List<GameObject>();
            ghostList = new List<GameObject>();
            
        }

        public virtual void StateHandler() //Sets a state if condition is filled
        {
            if (pacman.lifeCount == 0)
            {
                currentState = GameState.End;
            }
            if (pacman.score == maxScore - 4)
            {
                currentState = GameState.Win;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                currentState = GameState.Restart;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                currentState = GameState.Running;
            }
        }

        public void StateEffect()  // Reacts to different states 
        {
            if (currentState == GameState.Restart)
            {
                ClearMap();
                CreateMap(list);
                StatusAlert = "";
                pacman.score = 0;
                currentState = GameState.Running;
            }

            if (currentState == GameState.End)
            {
                ClearMap();

                StatusAlert = "Game Over \n \n press Enter to restart";
            }
            if (currentState == GameState.Win)
            {
                ClearMap();
                StatusAlert = "Map Clear \n \n press Enter to restart";
            }
        }
        public void Update(GameTime gameTime)
        {
            cam.Update(new Vector2((int)pacman.rec.X, (int)pacman.rec.Y), gameWindow);
            StateHandler();
            StateEffect();
            pacman.CheckSpecialCol(specialWall);
            pacman.CheckTileCol(floorList);
            pacman.CheckObjCol(ghostList);
            pacman.CheckBonusCol(bonusList);

            foreach (GameObject item in objectList)
            {
                item.Update();
                item.SpriteTimer(gameTime);
                item.CheckDirCol(wallList);
            }
            foreach (Ghost item in ghostList)
            {
                item.PacPos = pacman.getNode();
            }
        }


    }
}
