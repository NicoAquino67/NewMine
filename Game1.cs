using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewMine;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Model _model;
    private Matrix _world = Matrix.CreateTranslation(0,0,0);
    private Matrix _view = Matrix.CreateLookAt(new Vector3(0,10,10),Vector3.Zero, Vector3.Up);
    private Matrix _projection;
    private Player _player;

    private List<Block> _blocks = new List<Block>();


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 500f);
        _player = new Player(new Vector3(0,5,-10));
        base.Initialize();
    }

    private Texture2D _backgroundTexture;
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _model = Content.Load<Model>("Cube");
        // TODO: use this.Content to load your game content here
        _backgroundTexture = Content.Load<Texture2D>("Skybox");
        for (int x = -5; x < 5; x++){
            for (int y = 0;y < 2; y++){
                for (int z = -5; z < 5; z++){
                    var position = new Vector3(x*2,y*2,z*2);
                    _blocks.Add(new Block(position, _model));
                }
            }
        }
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _player.Update(gameTime);
        Mouse.SetPosition(Window.ClientBounds.Width / 2,Window.ClientBounds.Height/2);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _view = _player.GetViewMatrix();
        foreach(var block in _blocks){
            foreach (var mesh in block.BlockModel.Meshes){
                foreach (BasicEffect effect in mesh.Effects){
                    effect.World = Matrix.CreateTranslation(block.Position);
                    effect.View = _view;
                    effect.Projection = _projection;
                    effect.EnableDefaultLighting();
                }
            mesh.Draw();
            }
        }
        // TODO: Add your drawing code here
        
        base.Draw(gameTime);
    }
}
