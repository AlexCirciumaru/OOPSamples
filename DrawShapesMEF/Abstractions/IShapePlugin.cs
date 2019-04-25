
namespace Lesson2.Abstractions
{
    public interface IShapePlugin
    {
        IDrawableShape ReadShape();
        string GetName();
    }

}