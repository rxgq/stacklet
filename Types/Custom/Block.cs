using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tgm.Types.Custom;

public class Block : Shape2D
{
    public Block(Vector2 position, Scene2D scene, char character, ConsoleColor color)
        : base(position, scene, character, color)
    {

    }
}
