using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NewMine {
    public class Player{
        
        private Vector3 _position;
        public Vector3 Position { get{return _position; }set{_position = value;} }
        public float _speed = 0.3f;

        public Player(Vector3 initialPosition){
            Position = initialPosition;
        }
        public void Update(GameTime gameTime){
            var keybState = Keyboard.GetState();

            if(keybState.IsKeyDown(Keys.W))
            _position.Z -= _speed;
            if(keybState.IsKeyDown(Keys.S))
            _position.Z += _speed;
            if(keybState.IsKeyDown(Keys.A))
            _position.X -= _speed;
            if(keybState.IsKeyDown(Keys.D))
            _position.X += _speed;
            if (keybState.IsKeyDown(Keys.Space))
                _position.Y += _speed;
            if (keybState.IsKeyDown(Keys.LeftShift))
                _position.Y -= _speed;
        }
    }
}