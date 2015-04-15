using System;

namespace DiGraph
{
    public class Vertice<TVerticeMark> : IVertice<TVerticeMark>
    {
        public TVerticeMark Mark { get; set; }

        public int Number { get; set; }

        public IVertice<TVerticeMark> AdjecentVertices()
        {
            throw new NotImplementedException();
        }
    }
}
