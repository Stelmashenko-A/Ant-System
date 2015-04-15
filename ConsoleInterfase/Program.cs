using System.Collections.Generic;
using DiGraph.MMAS;

namespace ConsoleInterfase
{
    class Program
    {
        static void Main(string[] args)
        {
            var array2D = new double[,] {{0, 3, 1, 8}, {3, 0, 4, 4}, {1, 4, 0, 7}, {8, 4, 7, 0}};
            var minMaxAntSystem = new MinMaxAntSystem {Alfa = 0.5, Beta = 0.5, Ro = 0.1};
            List<int> res;
            double path;
            minMaxAntSystem.FindPath(array2D, out res, out path);

        }
    }
}
