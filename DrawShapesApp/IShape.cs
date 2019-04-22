using System;

namespace DrawShapesApp
{
    public interface IShape
    {
        void Draw();

        void ReadCoordinates();

        string GetName();

        int GetId();

        void GetShapeDetails();
    }
}