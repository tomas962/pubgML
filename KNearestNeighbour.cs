using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PUBGStatistics
{
    class KNearestNeighbor
    {
        static public int Classify(double[] testData, double[][] trainDataArray, double[][] trainTargetArray, int K, double min, double max)
        {
            double class1 = (50 - min) / (max - min);
            double class2 = (150 - min) / (max - min);

            List<int> c = new List<int>() { 0, 0, 0 };
            List<double> nearest = FindNearest(testData[0], testData[1], trainDataArray, trainTargetArray, K);
            foreach (var kills in nearest)
            {
                if (kills < class1)
                {
                    c[0]++;
                }
                else if (kills < class2)
                {
                    c[1]++;
                }
                else
                {
                    c[2]++;
                }
            }
            return c.Select((s, i) => new { s, i }).OrderByDescending(o => o.s).Select(s => s.i + 1).First();
        }
        static List<double> FindNearest(double x, double y, double[][] trainDataArray, double[][] trainTargetArray, int K)
        {
            //var nearest = trainDataArray
            //    .Select((s, i) => new { Distance = Distance(x, y, s[0], s[1]), i })
            //    .OrderBy(o => o)
            //    .Take(K)
            //    .Select(s => trainTargetArray[s.i][0])
            //    .ToList();

            var distances = trainDataArray
                .Select((s, i) => new { Distance = Distance(x, y, s[0], s[1]), i });

            var orderedDist = distances.OrderBy(o => o.Distance);
            var KDist = orderedDist.Take(K);

            var nearest = KDist.Select(s => trainTargetArray[s.i][0]).ToList();
            return nearest;
        }
        static double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
    }
}
