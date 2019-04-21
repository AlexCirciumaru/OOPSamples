using System;
using System.Runtime.Serialization;

namespace DrawShapesApp
{
    [System.Serializable]
    public class ShapeNotFoundException : Exception
    {
        public ShapeNotFoundException() { }

        public ShapeNotFoundException(string message) : base(message) { }

        public ShapeNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        protected ShapeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}