using Lesson2.Abstractions;
using Lesson2.Common;
using System.Composition;

namespace Lesson2.Implementations.CirclePlugin
{
    [Export(typeof(IShapePlugin))]
    public class CirclePlugin : GenericPlugin<Circle>
    {
        public CirclePlugin():base("Circle")
        {
            

        }
    }
}