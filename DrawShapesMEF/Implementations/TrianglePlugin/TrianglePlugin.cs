using Lesson2.Abstractions;
using Lesson2.Common;
using System.Composition;

namespace Lesson2.Implementations.TrianglePlugin
{
    [Export(typeof(IShapePlugin))]
    public class TrianglePlugin: GenericPlugin<Triangle>
    {
        public TrianglePlugin():base("Triangle")
        {

        }
    }
}
