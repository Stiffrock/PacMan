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
    class Pacman : GameObject
    {
        public Vector2 startPos;
        private SpriteEffects spriteEffects;
        public int scale;

        public Pacman(Texture2D tex, Vector2 pos) : base (tex, pos)
        {
            this.tex = tex;
            this.pos = pos;
            this.startPos = pos;
            this.rotation = 0;
            this.score = score;
            this.scale = 1;
            this.lifeCount = 3;
            this.srcRec = new Rectangle(16,0,16,16);
            this.rec = new Rectangle((int)pos.X, (int)pos.Y, 16, 16);           
        }
        
        public Node getNode()
        {
            int arrayIndexX = (int)(pos.X / 20);
            int arrayIndexY = (int)(pos.Y / 20);

            return Map.nodeArray[arrayIndexX, arrayIndexY];
        }

        public override void SpriteTimer(GameTime gameTime)
        {
 	        base.SpriteTimer(gameTime);

            if(frameTimer <= 0 && srcRecCount == 0 && currentDirection != Direction.Stop)
            {
                frameTimer = frameInterval;
                srcRec.X = 32;
                srcRecCount++;

            }
            if (frameTimer <= 0 && srcRecCount == 1 && currentDirection != Direction.Stop)
            {
                frameTimer = frameInterval;
                srcRec.X = 16;
                srcRecCount++;
            }

            if (frameTimer <= 0 && srcRecCount == 2 && currentDirection != Direction.Stop)
            {        
                frameTimer = frameInterval;
                srcRec.X = 0;
                srcRecCount++;
            }

            if (frameTimer <= 0 && srcRecCount == 3 && currentDirection != Direction.Stop)
            {               
                frameTimer = frameInterval;
                srcRec.X = 16;
                srcRecCount = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           spriteBatch.Draw(tex, new Rectangle(rec.X+8, rec.Y+8, 16, 16), srcRec, Color.White, rotation, new Vector2(8,8), spriteEffects, 1);
        //   spriteBatch.Draw(tex, new Vector2(pos.X+8, rec.+8), srcRec, Color.White, rotation, new Vector2(8, 8), scale, spriteEffects, 1);
        }

        public override void Update()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down))
            {
                SetDirection(Direction.Right);
                rotation = MathHelper.ToRadians(0);
                spriteEffects = SpriteEffects.None;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down))
            {
                SetDirection(Direction.Left);
                rotation = MathHelper.ToRadians(0);
                spriteEffects = SpriteEffects.FlipHorizontally;              
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Down))
            {
                SetDirection(Direction.Up);
                rotation = MathHelper.ToRadians(-90);
                spriteEffects = SpriteEffects.None;            
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                SetDirection(Direction.Down);
                rotation = MathHelper.ToRadians(-90);
                spriteEffects = SpriteEffects.FlipHorizontally;            
            }

            if (currentDirection == Direction.Right)
            {
                rec.X += 1;           
            }
            if (currentDirection == Direction.Left)
            {
                rec.X -= 1;
            }
            if (currentDirection == Direction.Up )
            {
                rec.Y -= 1;
            }
            if (currentDirection == Direction.Down)
            {
                rec.Y += 1;
            }
        }
    }
}
