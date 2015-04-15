using System;
using System.Collections.Generic;
using System.Linq;
using DiGraph.MMAS;

namespace DiGraph
{
    public class DiGraph<TArcMarc, TArc, TVerticeMark, TVertice> : IDiGraph<TArcMarc, TArc, TVerticeMark, TVertice>
        where TArc : IArc<TArcMarc, TVerticeMark, TVertice>, new() where TVertice : IVertice<TVerticeMark>, new()
    {
        public IEnumerable<TVertice> Adj(TVertice vertice)
        {
            return _internalViewVertice[vertice];
        }

        public TArc Linked(TVertice start, TVertice end)
        {
            return _matrix[start.Number, end.Number];
        }

        public bool IsLinked(TVertice start, TVertice end)
        {
            return _matrix[start.Number, end.Number] != null;
        }

        public IEnumerable<TVertice> Vertices
        {
            get { return _vertices; }
        }

        public IEnumerable<TArc> Arcs
        {
            get { return _arcs; }
        }




        private Dictionary<KeyValuePair<TVertice, TVertice>, TArc> _internalViewArcs = new Dictionary<KeyValuePair<TVertice, TVertice>, TArc>();
        private List<TVertice> _vertices;
        private List<TArc> _arcs;
        private Dictionary<TVertice, List<TVertice>> _internalViewVertice;
        private TArc[,] _matrix;
        
        private int _verticesNumber;
        protected Dictionary<TVertice, HashSet<TArc>> _linkedArcs = new Dictionary<TVertice, HashSet<TArc>>();

        public DiGraph(double[,] matrix)
        {
            _vertices = new List<TVertice>();
            _arcs = new List<TArc>();
            _verticesNumber = (int) Math.Sqrt(matrix.Length);
            _matrix = new TArc[_verticesNumber, _verticesNumber];
            _internalViewVertice = new Dictionary<TVertice, List<TVertice>>();
            for (var i = 0; i < _verticesNumber; ++i)
            {
                var vertice = new TVertice {Number = i};
                _vertices.Add(vertice);
                _internalViewVertice.Add(_vertices.Last(), new List<TVertice>());
                for (var j = i+1; j < _verticesNumber; j++)
                {
                    
                    _matrix[i, j] = new TArc();
                    _matrix[j, i] = _matrix[i, j];
                    _arcs.Add(_matrix[i, j]);
                }

            }


            for (var i = 0; i < _verticesNumber; ++i)
            {
                for (var j = i+1; j < _verticesNumber; j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        _internalViewVertice[_vertices[i]].Add(_vertices[j]);
                    }
                }
            }
            for (int i = 0; i < _verticesNumber; i++)
            {
                for (int j = i+1; j < _verticesNumber; j++)
                {
                    
                    _matrix[i, j].Start = _vertices[i];
                    _matrix[i, j].End = _vertices[j];
                }
            }
            int u = 0;
            foreach (var variable in _arcs)
            {
                u++;
                if (!_linkedArcs.ContainsKey(variable.Start))
                {
                    _linkedArcs.Add(variable.Start, new HashSet<TArc>());
                }
                _linkedArcs[variable.Start].Add(variable);

                if (!_linkedArcs.ContainsKey(variable.End))
                {
                    _linkedArcs.Add(variable.End, new HashSet<TArc>());
                }
                _linkedArcs[variable.End].Add(variable);
            }
        }
    }
}
