using Lesson2.Abstractions;
using Lesson2.Common;
using System.Composition;

namespace Lesson2.Implementations.SquarePlugin
{
    [Export(typeof(IShapePlugin))]
    public class SquarePlugin : GenericPlugin<Square>
    {
        public SquarePlugin() : base("Square")
        {

        }
    }
}