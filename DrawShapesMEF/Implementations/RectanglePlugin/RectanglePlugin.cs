using Lesson2.Abstractions;
using Lesson2.Common;
using System.Composition;

namespace Lesson2.Implementations.RectanglePlugin
{
    [Export(typeof(IShapePlugin))]
    public class RectanglePlugin : GenericPlugin<Rectangle>
    {
        public RectanglePlugin() : base("Rectangle")
        {

        }
    }
}