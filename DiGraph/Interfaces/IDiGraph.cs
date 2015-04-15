using System.Collections.Generic;
using DiGraph.MMAS;

namespace DiGraph
{
    public interface IDiGraph<TArcMarc, out TArc, TVerticeMark, TVertice>
        where TArc : IArc<TArcMarc, TVerticeMark, TVertice>
        where TVertice : IVertice<TVerticeMark>
    {
        IEnumerable<TVertice> Adj(TVertice vertice);
       // IEnumerable<Vertice> AdjInvert(Vertice vertice);
        
        TArc Linked(TVertice start, TVertice end);
        bool IsLinked(TVertice start, TVertice end);

       // IDiGraph<ArcMarc, Arc, VerticeMark, Vertice>
            //Subgraph(IEnumerable<Vertice> verteces, IEnumerable<Arc> arcs);
        IEnumerable<TVertice> Vertices { get; }
        IEnumerable<TArc> Arcs { get; }
    }
}
