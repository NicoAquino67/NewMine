using System.Net.Http;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewMine;

public class Block 
{
    public Vector3 Position { get; set; }
    public Model BlockModel { get; set; }
    
    public Block(Vector3 position, Model blockModel){
        Position = position;
        BlockModel = blockModel;
    }
    //ToDo: Generacion de mundo a partir de ruido.
}