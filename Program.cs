using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace PUBGStatistics
{
    class Program
    {
        const double dataSplitPercentage = 0.8; //percentage of training data - remaining data is testing data
        const int hiddenNeuronCount = 10;
        const int outputNeuronCount = 1;
        const int trainingEpochCount = 100;
        const double learningRate = 0.1;
        const double learningMomentum = 1;
        const int dataStartLine = 0; //line from which to start reading data from file
        const int dataEndLine = int.MaxValue; //line from which to stop reading from file
        //const string dataFile = "../../Data/statsnocommas.csv";
        const string dataFile = "../../Data/stats.csv";

        static void Main(string[] args)
        {
            List<List<double>> data = ReadDataAsList(dataFile, ';');
            var pca = PCA.Compute(data);
            //read data from file
            (double[][] dataArray, double[][] targetArray) = ReadDataAsArray(dataFile, ';', new int[] { 0, 1, 38 }, new int[]{ 6}, dataStartLine, dataEndLine);

            //normalize all data values (scale between 0 and 1)
            (double[][] normalizedData, _, _) = NormalizeData(dataArray);
            (double[][] normalizedTargets, double[] min, double[] max) = NormalizeData(targetArray);

            //split data into two sets - training and testing = 80% and 20%
            (double[][] trainDataArray, double[][] trainTargetArray, double[][] testDataArray, double[][] testTargetArray) = SplitData(normalizedData, normalizedTargets, dataSplitPercentage);
            //(double[][] trainDataArray, double[][] trainTargetArray, double[][] testDataArray, double[][] testTargetArray) = SplitData(dataArray, targetArray, 0.8);

            //create network
            var nn = new BPNeuralNetwork(dataArray[0].Length, hiddenNeuronCount, outputNeuronCount, min, max);

            //train network
            nn.Train(trainDataArray, trainTargetArray, trainingEpochCount, learningRate, learningMomentum);

            string[] propertyNames = ReadPropertyNames("../../Data/property_names.csv", ',');
            for (int i = 0; i < propertyNames.Length; i++)
                Console.WriteLine(i + " | " + propertyNames[i]);

            //Test the network
            for (int i = 0; i < testDataArray.Length; i++)
            {
                nn.ComputeOutputs(testDataArray[i], testTargetArray[i]);
            }
            Console.ReadLine();
        }

        static string[] ReadPropertyNames(string fileName, char separator)
        {
            string[] lines = File.ReadAllLines(fileName);
            string[] names = lines[0].Split(separator);
            return names;
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
        static (double[][] dataArray, double[][] targetArray) ReadDataAsArray(string fileName, char separator, int[] columnsToSkip, int[] targetColumns, int startLine = 0, int endLine = int.MaxValue, double split = 1)
        {

            List<List<double>> data = new List<List<double>>();
            List<List<double>> targets = new List<List<double>>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                string[] values;
                int lineNr = 0;
                int idx = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    //start reading from startLine
                    if (lineNr < startLine)
                    {
                        lineNr++;
                        continue;
                    }

                    //end reading on endLine
                    if (lineNr > endLine)
                        break;

                    data.Add(new List<double>());
                    targets.Add(new List<double>());

                    values = line.Split(separator);

                    for (int i = 0; i < values.Length; i++)
                    {
                        //check if column should be skipped
                        if (columnsToSkip.Contains(i))
                            continue;

                        //check if column is target column = add to target array
                        if (targetColumns.Contains(i))
                            targets[idx].Add(double.Parse(values[i]));

                        //if not target, add to data array
                        else
                            data[idx].Add(double.Parse(values[i]));
                    }
                    idx++;
                    lineNr++;
                }
            }

            var dataRows = data.Select(row => row.ToArray());
            var targetRows = targets.Select(row => row.ToArray());

            double[][] dataArray = dataRows.ToArray();
            double[][] targetsArray = targetRows.ToArray();

            return (dataArray, targetsArray);
        }

        static (double[][] trainDataArray, double[][] trainTargetArray, double[][] testDataArray, double[][] testTargetsArray) SplitData(double[][] dataArray, double[][] targetArray, double split)
        {
            var dataRows = dataArray.Select(row => row.ToArray());
            var trainDataRows = dataRows.Take((int)(dataRows.Count() * split));
            var targetRows = targetArray.Select(row => row.ToArray());
            var trainTargetRows = targetRows.Take((int)(targetRows.Count() * split));
            var testDataRows = dataRows.Skip((int)(dataRows.Count() * split));
            var testTargetRows = targetRows.Skip((int)(targetRows.Count() * split));

            double[][] trainDataArray = trainDataRows.ToArray();
            double[][] trainTargetsArray = trainTargetRows.ToArray();

            double[][] testDataArray = testDataRows.ToArray();
            double[][] testTargetsArray = testTargetRows.ToArray();

            return (trainDataArray, trainTargetsArray, testDataArray, testTargetsArray);
        }

        static List<List<double>> NormalizeData(List<List<double>> inputData)
        {
            
            var data = new List<List<double>>(inputData);
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

        static (double[][], double[] min, double[] max) NormalizeData(double[][] inputData)
        {

            var data = inputData;
            //Find minimum and maximum for each attribute value
            double[] min = new double[data[0].Length];
            double[] max = new double[data[0].Length];
            for (int i = 0; i < data[0].Length; i++)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    min[i] = min[i] > data[j][i] ? data[j][i] : min[i];
                    max[i] = max[i] < data[j][i] ? data[j][i] : max[i];
                }
            }

            //Normalize values using "Min max normalization"
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    data[i][j] = (data[i][j] - min[j]) / (max[j] - min[j]);
                }
            }
            return (data, min, max);
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
