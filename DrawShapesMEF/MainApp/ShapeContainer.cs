using System;
using System.Collections.Generic;
using Lesson2.Abstractions;

namespace Lesson2.Implementations
{
    public class ShapeContainer : IDrawableShape
    {
        private List<IDrawableShape> shapes = new List<IDrawableShape>();

        public void Add(IDrawableShape shape)
        {
            shapes.Add(shape);    
        }

        public void Remove(IDrawableShape shape)
        {

        }

        public void Draw()
        {
            foreach(var shape in shapes)
            {
                shape.Draw();
            }
        }
    }
}