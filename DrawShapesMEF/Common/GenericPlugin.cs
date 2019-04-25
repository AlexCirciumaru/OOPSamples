using System;
using Lesson2.Abstractions;

namespace Lesson2.Common
{
    public class GenericPlugin<T> : IShapePlugin where T : IReadableShape, IDrawableShape, new() 
    {
        private string shapeName = "";

        public GenericPlugin(string shapeName)
        {
            this.shapeName = shapeName;
        }
        public string GetName()
        {
            return shapeName;
        }

        public IDrawableShape ReadShape()
        {
            var newShape = new T();
            newShape.Read();
            return newShape;
        }
    }
}