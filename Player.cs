using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewMine {
    public class Player{
        
        //posicion del jugador en el mundo
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; private set; }
        private float yaw, pitch;
        public float Speed;

        private float mouseSensitivity = 0.002f;
        private int ScreenWidth, ScreenHeight;

        public Player(
                Vector3 initialPosition, 
                int screenWidth, 
                int screenHeight) {
            Position = initialPosition;
            Rotation = Quaternion.Identity;
            yaw = 0f;
            pitch = 0f;
            this.ScreenWidth = screenWidth;
            this.ScreenHeight = screenHeight;
        }
        public Matrix GetViewMatrix(){
            //genera la matriz de vista a partir de la posicion y rotacion.
            Vector3 forward = Vector3.Transform(Vector3.Forward, Rotation);//hacia adelante
            Vector3 target = Position + forward;
            return Matrix.CreateLookAt(Position, target, Vector3.Up);
        }
        public void Update(GameTime gameTime){
            
            //lee el raton
            MouseState mouseState = Mouse.GetState();
            int deltaX = mouseState.X - (ScreenWidth/2);
            int deltaY = mouseState.Y - (ScreenHeight/2);
            
            //calcula los angulos de rotacion
            yaw -= deltaX * mouseSensitivity; //rotacion en X
            pitch -= deltaY * mouseSensitivity; //rotacion en Y

            // Limitar el pitch para evitar que la cámara gire bruscamente
            pitch = MathHelper.Clamp(
                pitch, 
                -MathHelper.PiOver2 + 0.1f,
                MathHelper.PiOver2 - 0.1f);

            Rotation = Quaternion.CreateFromYawPitchRoll(yaw,pitch,0f);
            
            //vuelve a centrar el raton
            Mouse.SetPosition(ScreenWidth / 2, ScreenHeight / 2);
        }        
    }
}