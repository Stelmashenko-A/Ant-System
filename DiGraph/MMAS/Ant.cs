using System;
using System.Collections.Generic;
using System.Linq;
using DiGraph.Utility;

namespace DiGraph.MMAS
{
    public class Ant
    {
        private readonly double _alfa;

        private readonly double _beta;

        private readonly double _ro;
        public AntsGraph Graph { get; set; }
        
        //public AntVertice CurrentVertice { get; private set; }

        public Ant(AntsGraph graph, AntVertice start, double alfa, double beta, double ro)
        {
            _alfa = alfa;
            _beta = beta;
            _ro = ro;
            Graph = graph;
            VerticeMemory = new List<AntVertice> {start};
            ArcMemory = new List<AntArc>();
        }

        public List<AntVertice> VerticeMemory { get; private set; }
        public List<AntArc> ArcMemory { get; private set; }
        public void AddVerticeToMemory(AntVertice memory)
        {
            VerticeMemory.Add(memory);
        }

        public double CalculateAttraction(PathLength path, Pheromone pheromone)
        {
            var d1 = Math.Pow(path.Value, _alfa);
            var d2 = Math.Pow(pheromone.Value, _beta);
            return d2*d1;
        }

        public bool TryFindNextStep()
        {
            List<AntArc> arcs = Graph.LinkeArcs(VerticeMemory.Last());
            var probability = new List<MutablePair<AntArc, double>>();
            foreach (var VARIABLE in arcs)
            {
                if (!VerticeMemory.Contains(VARIABLE.Start) || !VerticeMemory.Contains(VARIABLE.End))
                {
                    probability.Add(new MutablePair<AntArc, double>(VARIABLE, CalculateAttraction(VARIABLE.Mark.PathLength, VARIABLE.Mark.Pheromone)));
                }
            }
            if (probability.Count == 0) return false;
            double totalProbilility = probability.Sum(variable => variable.Second);
            for (int i = 0; i < probability.Count; i++)
            {
                probability[i].Second /= totalProbilility;
            }
            var arc = RandomSelector<AntArc>.Select(probability);
            ArcMemory.Add(arc);
            VerticeMemory.Add(VerticeMemory.Last() == arc.Start ? arc.End : arc.Start);
            return true;
        }

        public void MarkCurrentArc(List<AntArc> bestSolution)
        {
            ArcMemory.Last().Mark.Pheromone.Value = (1 - _ro) * ArcMemory.Last().Mark.Pheromone.Value;
            if (bestSolution.Contains(ArcMemory.Last()))
            {
                ArcMemory.Last().Mark.Pheromone.Value += 1 / ArcMemory.Last().Mark.PathLength.Value;
            }
        }

        public bool IsSurvived
        {
            get
            {
                return Graph.Vertices.Count() == VerticeMemory.Count &&
                       Graph.IsLinked(VerticeMemory.Last(), VerticeMemory.First());
            }
        }
    }
}
