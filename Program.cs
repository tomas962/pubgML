using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUBGStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataFile = "../../Data/statsnocommas.csv";
            List<List<double>> data = ReadDataAsList(dataFile, ';');
            var normalizedData = NormalizeData(data);
            //WriteData(stats);
            Stat a = new Stat();
            Console.WriteLine();
            Console.ReadKey();        
        }

        static List<Stat> ReadDataAsObjects(string fileName)
        {
            List<Stat> stats = new List<Stat>();
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    var param = line.Split(';');

                    Stat stat = new Stat(param[0].ToString(), int.Parse(param[1]), double.Parse(param[2]), double.Parse(param[3]), double.Parse(param[4]), int.Parse(param[5]), int.Parse(param[6]),
                        double.Parse(param[7]), int.Parse(param[8]), double.Parse(param[9]), int.Parse(param[10]), double.Parse(param[11]), double.Parse(param[12]), double.Parse(param[13]),
                    double.Parse(param[14]), double.Parse(param[15]), double.Parse(param[16]), double.Parse(param[17]), double.Parse(param[18]), double.Parse(param[19]),
                    double.Parse(param[20]), double.Parse(param[21]), int.Parse(param[22]), int.Parse(param[23]), int.Parse(param[24]), int.Parse(param[25]),
                    int.Parse(param[26]), double.Parse(param[27]), int.Parse(param[28]), int.Parse(param[29]), int.Parse(param[30]), int.Parse(param[31]),
                    int.Parse(param[32]), int.Parse(param[33]), int.Parse(param[34]), double.Parse(param[35]), double.Parse(param[36]), double.Parse(param[37]), int.Parse(param[38]),
                    double.Parse(param[39]), double.Parse(param[40]), double.Parse(param[41]), double.Parse(param[42]), double.Parse(param[43]), double.Parse(param[44]),
                    int.Parse(param[45]), int.Parse(param[46]), double.Parse(param[47]));

                    stats.Add(stat);
                }
            }
            return stats;
        }

        static List<List<double>> ReadDataAsList(string fileName, char separator)
        {
            List<List<double>> data = new List<List<double>>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                string[] values;
                int lineNr = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    data.Add(new List<double>());
                    
                    values = line.Split(separator);
                    //Skip first value, because it is string (player name)
                    for (int i = 1; i < values.Length; i++)
                    {
                        data[lineNr].Add(double.Parse(values[i]));
                    }
                    lineNr++;
                }
            }
            return data;
        }
        static List<List<double>> NormalizeData(List<List<double>> data)
        {
            //Find minimum and maximum for each attribute value
            double[] min = new double[data[0].Count];        
            double[] max = new double[data[0].Count];
            for (int i = 0; i < data[0].Count; i++)
            {
                for (int j = 0; j < data.Count; j++)
                {
                    min[i] = min[i] > data[j][i] ? data[j][i] : min[i]; 
                    max[i] = max[i] < data[j][i] ? data[j][i] : max[i];
                }
            }

            //Normalize values using "Min max normalization"
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    data[i][j] = (data[i][j] - min[j]) / (max[j] - min[j]);
                } 
            }
            return data;
        }

        static void WriteData(List<Stat> stats)
        {
            foreach (var stat in stats)
            {
                Console.WriteLine(stat);
            }
        }

    }
}
