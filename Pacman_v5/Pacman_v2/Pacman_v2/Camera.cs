using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman_v2
{
    class Camera
    {
        public static Matrix transform;
        public static Vector2 centre;
        private Viewport view;
        int windowSizeX = 800;
        int windowSizeY = 744;
        float X, Y;

        public float zoom = 2.5f;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(Vector2 playerPos, GameWindow gameWindow)
        {
            //uppdaterar X position när pacman rör sig i områden som inte skulle röra kameran utanför banan(inte äpple xD)
            X = playerPos.X * zoom + 12 - windowSizeX / 2;
            //uppdaterar Y position när pacman rör sig i områden som inte skulle röra kameran utanför banan
            Y = playerPos.Y * zoom + 150 - windowSizeY / 2;
      
            centre = new Vector2(X, Y);

            transform = Matrix.CreateScale(new Vector3(zoom, zoom, 0))
            * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }

        public Matrix GetTransform()
        {
            return transform;
        }
        public Vector2 GetCameraPos
        {
            get
            {
                return centre;
            }
        }
    }

}

