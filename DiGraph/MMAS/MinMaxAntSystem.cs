using System;
using System.Collections.Generic;
using System.Linq;
using DiGraph.Utility;

namespace DiGraph.MMAS
{
    public class MinMaxAntSystem
    {
        public double Alfa { get; set; }
        public double Beta { get; set; }
        public double Ro { get; set; }
        private
            AntsGraph _graph;
        public List<AntArc> BestArcSolution { get; set; }

        public List<AntVertice> BestVeticeSolution { get; set; }

        private List<Ant> _ants = new List<Ant>();

        public void FindPath(double[,] matrix, out List<int> path, out double length )
        {
            _graph = new AntsGraph(matrix);
            BestArcSolution = new List<AntArc>();
            length = double.MaxValue;
            
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < (int)Math.Sqrt(matrix.Length); j++)
                {
                    _ants.Add(new Ant(_graph, _graph.Vertices.ElementAt(j), Alfa, Beta, Ro));
                }
                foreach (var ant in _ants)
                {
                    while (ant.TryFindNextStep())
                    {
                        ant.MarkCurrentArc(BestArcSolution);
                    }
                    if (!ant.IsSurvived) continue;
                    var pathLength = ant.ArcMemory.Sum(variable => variable.Mark.PathLength.Value);
                    if (ant.VerticeMemory != null)
                    {
                        pathLength += _graph.Linked(ant.VerticeMemory.First(), ant.VerticeMemory.Last()).Mark.PathLength.Value;
                        if (!(pathLength < length)) continue;
                        length = pathLength;
                        BestArcSolution = ant.ArcMemory;
                        BestVeticeSolution = ant.VerticeMemory;
                    }
                }
            }
            path = BestVeticeSolution.Select(variable => variable.Number).ToList();
        }

    }
}
