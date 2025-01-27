using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewMine {
    public class Player{
        
        private Vector3 _position;
        public Vector3 Position { 
            get{return _position; }
            set{_position = value;} }
        
        public float _speed;
        private Vector3 lookAt;
        public float Yaw { get; set; } //Rotacion eje Y (Horizontal)
        public float Pitch { get; set;}//Rotacion eje X (Vertical)
        public Matrix ViewMatrix { get; private set;}
        public int screenWidth, screenHeight;
        private MouseState previousMouseState; // Almacenar el estado previo del mouse

        public Player(Vector3 initialPosition){
            Position = initialPosition;
            Yaw = 0f;
            Pitch = 0f;

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            previousMouseState = Mouse.GetState();
            UpdateViewMatrix();
        }
        public void Update(GameTime gameTime){
            //Input del jugador
            var keybState = Keyboard.GetState();

            //Movimiento basico
            if(keybState.IsKeyDown(Keys.W)) _position.Z -= _speed;
            if(keybState.IsKeyDown(Keys.S)) _position.Z += _speed;
            if(keybState.IsKeyDown(Keys.A)) _position.X -= _speed;
            if(keybState.IsKeyDown(Keys.D)) _position.X += _speed;

            //Movimiento Vertical
            if (keybState.IsKeyDown(Keys.Space)) _position.Y += _speed;
            if (keybState.IsKeyDown(Keys.LeftShift)) _position.Y -= _speed;

            //Control de camara
            //Centrar Mouse
            MouseState currentMouseState = Mouse.GetState();
            int centerX = screenHeight/2;
            int centerY = screenWidth/2;

            //calcular el desplazamiento del mouse desde el centro
            float deltaX = currentMouseState.X - previousMouseState.X;
            float deltaY = currentMouseState.Y - previousMouseState.Y;

            //ajuste de sensibilidad
            float sensitivity = 0.0001f;
            Yaw -= deltaX * sensitivity;
            Pitch -= deltaY * sensitivity;
            
            //limitar el angulo vertical
            Pitch = MathHelper.Clamp(Pitch, -MathHelper.PiOver2 + 0.1f, MathHelper.PiOver2 - 0.1f);
            //centrar el Mouse nuevamente en la ventana
            Mouse.SetPosition(centerX,centerY);

            //actualizamos el estado previo del mouse
            previousMouseState = Mouse.GetState();
            UpdateViewMatrix();
        }
        private void UpdateViewMatrix(){
            Vector3 forward = new Vector3(
                (float)(Math.Cos(Pitch)* Math.Cos(Yaw)),
                (float)(Math.Sin(Pitch)),
                (float)(Math.Cos(Pitch)* Math.Sin(Yaw)));
                lookAt = _position + forward;
                ViewMatrix = Matrix.CreateLookAt(_position,lookAt,Vector3.Up);
        }
        public Matrix GetViewMatrix(){
            var direction = Vector3.Transform(Vector3.Forward,
            Matrix.CreateFromYawPitchRoll(Yaw,Pitch,0));
            var target = Position + direction;
            return Matrix.CreateLookAt(Position,target,Vector3.Up);
        }
    }
}