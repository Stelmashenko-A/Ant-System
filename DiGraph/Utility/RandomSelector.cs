using System;
using System.Collections.Generic;

namespace DiGraph.Utility
{
    static class RandomSelector<T>
    {
        public static T Select(List<MutablePair<T, double>> data)
        {

            double probability = 0;
            var listProbability = new List< double>();
            foreach (var variable in data)
            {
                probability += variable.Second;
                listProbability.Add(probability);
                
            }
            var random = new Random(DateTime.Now.Millisecond);
            probability = random.NextDouble();
            var resultIndex = 0;
            for (var i = 0; i < data.Count; i++)
            {
                if (listProbability[i] < probability)
                {
                    resultIndex = i;
                }
                break;
            }
            return data[resultIndex].First;
        }
    }
    
}
