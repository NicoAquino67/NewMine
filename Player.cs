using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NewMine {
    public class Player{
        
        private Vector3 _position;
        public Vector3 Position { 
            get{return _position; }
            set{_position = value;} }
        
        public float Yaw { get; set; } //Rotacion eje Y (Horizontal)
        public float Pitch { get; set;}//Rotacion eje X (Vertical)
        public const float RotationSpeed = 0.1f;
        public float _speed = 0.3f;

        public Player(Vector3 initialPosition){
            Position = initialPosition;
        }
        public void Update(GameTime gameTime){
            var keybState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            //Movimiento basico
            if(keybState.IsKeyDown(Keys.W))
            _position.Z -= _speed;
            if(keybState.IsKeyDown(Keys.S))
            _position.Z += _speed;
            if(keybState.IsKeyDown(Keys.A))
            _position.X -= _speed;
            if(keybState.IsKeyDown(Keys.D))
            _position.X += _speed;
            //Movimiento Vertical
            if (keybState.IsKeyDown(Keys.Space))
                _position.Y += _speed;
            if (keybState.IsKeyDown(Keys.LeftShift))
                _position.Y -= _speed;
            //Control de camara
            if (Mouse.GetState().RightButton == ButtonState.Pressed){
                var deltaX = mouseState.X - Mouse.GetState().X;
                var deltaY = mouseState.Y - Mouse.GetState().Y;
                Yaw -= deltaX * RotationSpeed;
                Pitch = MathHelper.Clamp(Pitch - deltaY * RotationSpeed,
                -MathHelper.PiOver2,
                MathHelper.PiOver2);
            }
        }
        public Matrix GetViewMatrix(){
            var direction = Vector3.Transform(Vector3.Forward,
            Matrix.CreateFromYawPitchRoll(Yaw,Pitch,0));
            var target = Position + direction;
            return Matrix.CreateLookAt(Position,target,Vector3.Up);
        }
    }
}