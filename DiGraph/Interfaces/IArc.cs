using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiGraph
{
    public interface IArc<T, VerticeMark, Vertice>
        where Vertice : IVertice<VerticeMark>
    {
        T Mark { get; }
        //IVertice<VerticeMark> Start { get; }
        //IVertice<VerticeMark> End { get; }
        Vertice Start { get; set; }
        Vertice End { get; set; }
    }
}
