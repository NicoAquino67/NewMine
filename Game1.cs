using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewMine;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Model _model;
    private Model _skyboxModel;
    private Texture2D _skyboxTexture;
    private Matrix _world = Matrix.CreateTranslation(0,0,0);
    private Matrix _view = Matrix.CreateLookAt(new Vector3(0,10,10),Vector3.Zero, Vector3.Up);
    private Matrix _projection;
    private Player _player;

    private List<Block> _blocks = new List<Block>();

    //ToDo: Bugfix: , texturas de bloques, personaje.
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.PiOver4, 
            GraphicsDevice.Viewport.AspectRatio, 0.1f, 500f);
        _player = new Player(new Vector3(3,6,-10), 
            GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height);
        base.Initialize();
    }

    protected override void LoadContent(){
        try{
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _model = Content.Load<Model>("blocks/dirt/dirt");
            _skyboxModel = Content.Load<Model>("skybox/Skybox");
            _skyboxTexture = Content.Load<Texture2D>("skybox/Textures/skybox");
            for (int x = -10; x < 10; x++)
            {
                for (int y = 0; y < 1; y++)
                {
                    for (int z = -10; z < 10; z++)
                    {
                        var position = new Vector3(x * 2, y * 2, z * 2);
                        _blocks.Add(new Block(position, _model));
                    }
                }
            }
        }
        catch(Exception ex){
                Debug.WriteLine($"Error in LoadContent: {ex.Message}");
                Exit();
        }
    }
    
    private void DrawSkybox()
    {
        GraphicsDevice.DepthStencilState = DepthStencilState.None;
        foreach (var mesh in _skyboxModel.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects)
            {   
                effect.Texture = _skyboxTexture;
                effect.World = Matrix.CreateScale(100f) * Matrix.CreateTranslation(_player.Position);
                effect.View = _view;
                effect.Projection = _projection;
                effect.TextureEnabled = true;
            }
            mesh.Draw();
        }
        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
    }

    private void DrawBlocks(){
        
        foreach (var block in _blocks)
        {
            foreach (var mesh in block.BlockModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = Matrix.CreateTranslation(block.Position);
                    effect.View = _view;
                    effect.Projection = _projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }
    }

    protected override void Update(GameTime gameTime)
    {
        try {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            _player.Update(gameTime);
            base.Update(gameTime);
        }
        catch (Exception ex) {
                Debug.WriteLine($"error in update: {ex.Message}");
                Exit();
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        try{
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _view = _player.GetViewMatrix();
            DrawSkybox();
            DrawBlocks();
            base.Draw(gameTime);
        }
        catch(Exception ex) {
            Debug.WriteLine($"Error in Draw: {ex.Message}");
            Exit();
        }
    }
}
