using System.Collections.Generic;
using System.Linq;

namespace DiGraph.MMAS
{
    public class AntsGraph:DiGraph<ArcMark,AntArc,int,AntVertice>
    {
        public AntsGraph(double[,] matrix) : base(matrix)
        {
            foreach (var VARIABLE in base.Arcs)
            {
                ArcMark mark = new ArcMark();
                Pheromone ph= new Pheromone();
                ph.Value = 1;
                mark.Pheromone = ph;
                PathLength pl=new PathLength(matrix[VARIABLE.End.Number, VARIABLE.Start.Number]);
                mark.PathLength=pl;
                VARIABLE.Mark = mark;
            }
        }
        public List<AntArc> LinkeArcs(AntVertice vertice)
        {
            return _linkedArcs[vertice].ToList();
        }
    }
}
