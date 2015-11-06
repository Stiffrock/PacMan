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

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;       
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        List<String> maptxt;       
        Texture2D spriteSheet;
        Texture2D wallTile;
        Texture2D food;

        Map map;
      
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            spriteSheet = Content.Load<Texture2D>("SpriteSheet");
            wallTile = Content.Load<Texture2D>("wallTile");
            food = Content.Load<Texture2D>("centerDot");
            maptxt = new List<String>();
            map = new Map(spriteSheet, wallTile, food, maptxt, spriteFont, GraphicsDevice);
            map.CreateMap(maptxt);               
        }

        protected override void UnloadContent()
        {        }

        protected override void Update(GameTime gameTime)
        { 
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {Exit();}
            
            map.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
           
            GraphicsDevice.Clear(Color.Black);
            
            map.Draw(spriteBatch);


            base.Draw(gameTime);
        }
    }
}
