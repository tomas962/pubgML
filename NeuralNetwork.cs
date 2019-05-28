using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUBGStatistics
{
    /// <summary>
    /// Back propagation neural network
    /// </summary>
    class BPNeuralNetwork
    {
        private int numInput; // number input nodes
        private int numHidden;
        private int numOutput;

        private double[] inputs;
        private double[][] ihWeights; // input-hidden
        private double[] hBiases;
        private double[] hOutputs;

        private double[][] hoWeights; // hidden-output
        private double[] oBiases;
        private double[] outputs;

        private double[] min; //target min and max values use for output denormalization
        private double[] max; 

        private double[][] testData; //optional test data array
        private double[][] testTargets;

        private Random rnd;

        public BPNeuralNetwork(int numInput, int numHidden, int numOutput, double[] min, double[] max, double[][] testDataArray = null, double[][] testTargetArray = null)
        {
            this.numInput = numInput;
            this.numHidden = numHidden;
            this.numOutput = numOutput;

            this.inputs = new double[numInput];

            this.ihWeights = MakeMatrix(numInput, numHidden, 0.0);
            this.hBiases = new double[numHidden];
            this.hOutputs = new double[numHidden];

            this.hoWeights = MakeMatrix(numHidden, numOutput, 0.0);
            this.oBiases = new double[numOutput];
            this.outputs = new double[numOutput];

            this.min = min;
            this.max = max;

            if (testDataArray != null && testTargetArray != null)
            {
                testData = testDataArray;
                testTargets = testTargetArray;
            }
            else
            {
                testData = null;
                testTargets = null;
            }
            

            this.rnd = new Random(0);
            this.InitializeWeights(); // all weights and biases
        } // ctor

        private static double[][] MakeMatrix(int rows,
          int cols, double v) // helper for ctor, Train
        {
            double[][] result = new double[rows][];
            for (int r = 0; r < result.Length; ++r)
                result[r] = new double[cols];
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = v;
            return result;
        }

        //private static double[][] MakeMatrixRandom(int rows,
        //  int cols, int seed) // helper for ctor, Train
        //{
        //  Random rnd = new Random(seed);
        //  double hi = 0.01;
        //  double lo = -0.01;
        //  double[][] result = new double[rows][];
        //  for (int r = 0; r < result.Length; ++r)
        //    result[r] = new double[cols];
        //  for (int i = 0; i < rows; ++i)
        //    for (int j = 0; j < cols; ++j)
        //      result[i][j] = (hi - lo) * rnd.NextDouble() + lo;
        //  return result;
        //}

        private void InitializeWeights() // helper for ctor
        {
            // initialize weights and biases to small random values
            int numWeights = (numInput * numHidden) +
              (numHidden * numOutput) + numHidden + numOutput;
            double[] initialWeights = new double[numWeights];
            for (int i = 0; i < initialWeights.Length; ++i)
                initialWeights[i] = (0.001 - 0.0001) * rnd.NextDouble() + 0.0001;
            this.SetWeights(initialWeights);
        }

        public void SetWeights(double[] weights)
        {
            // copy serialized weights and biases in weights[] array
            // to i-h weights, i-h biases, h-o weights, h-o biases
            int numWeights = (numInput * numHidden) +
              (numHidden * numOutput) + numHidden + numOutput;
            if (weights.Length != numWeights)
                throw new Exception("Bad weights array in SetWeights");

            int k = 0; // points into weights param

            for (int i = 0; i < numInput; ++i)
                for (int j = 0; j < numHidden; ++j)
                    ihWeights[i][j] = weights[k++];
            for (int i = 0; i < numHidden; ++i)
                hBiases[i] = weights[k++];
            for (int i = 0; i < numHidden; ++i)
                for (int j = 0; j < numOutput; ++j)
                    hoWeights[i][j] = weights[k++];
            for (int i = 0; i < numOutput; ++i)
                oBiases[i] = weights[k++];
        }

        public double[] GetWeights()
        {
            int numWeights = (numInput * numHidden) +
              (numHidden * numOutput) + numHidden + numOutput;
            double[] result = new double[numWeights];
            int k = 0;
            for (int i = 0; i < ihWeights.Length; ++i)
                for (int j = 0; j < ihWeights[0].Length; ++j)
                    result[k++] = ihWeights[i][j];
            for (int i = 0; i < hBiases.Length; ++i)
                result[k++] = hBiases[i];
            for (int i = 0; i < hoWeights.Length; ++i)
                for (int j = 0; j < hoWeights[0].Length; ++j)
                    result[k++] = hoWeights[i][j];
            for (int i = 0; i < oBiases.Length; ++i)
                result[k++] = oBiases[i];
            return result;
        }

        public double[] ComputeOutputs(double[] xValues, double[] targets = null)
        {
            double[] hSums = new double[numHidden]; // hidden nodes sums scratch array
            double[] oSums = new double[numOutput]; // output nodes sums

            for (int i = 0; i < xValues.Length; ++i) // copy x-values to inputs
                this.inputs[i] = xValues[i];
            // note: no need to copy x-values unless you implement a ToString.
            // more efficient is to simply use the xValues[] directly.

            for (int j = 0; j < numHidden; ++j)  // compute i-h sum of weights * inputs
                for (int i = 0; i < numInput; ++i)
                    hSums[j] += this.inputs[i] * this.ihWeights[i][j]; // note +=

            for (int i = 0; i < numHidden; ++i)  // add biases to hidden sums
                hSums[i] += this.hBiases[i];

            for (int i = 0; i < numHidden; ++i)   // apply activation
                this.hOutputs[i] = ReLU(hSums[i]); // hard-coded

            for (int j = 0; j < numOutput; ++j)   // compute h-o sum of weights * hOutputs
                for (int i = 0; i < numHidden; ++i)
                    oSums[j] += hOutputs[i] * hoWeights[i][j];

            for (int i = 0; i < numOutput; ++i)  // add biases to output sums
                oSums[i] += oBiases[i];

            double[] softOut = /*Softmax(*/oSums; // all outputs at once for efficiency
            Array.Copy(softOut, outputs, softOut.Length);

            double[] retResult = new double[numOutput]; // could define a GetOutputs 
            Array.Copy(this.outputs, retResult, retResult.Length);

            if(targets != null)//max=63
            {
                Console.WriteLine("Result value:\n" + (int)(retResult[0] * (this.max[0] - this.min[0]) + this.min[0]) + "\nTarget value:\n" + (targets[0] * (this.max[0] - this.min[0]) + this.min[0]) + "\n");
            }

            return retResult;
        }

        private static double HyperTan(double x)
        {
            if (x < -20.0) return /*-1.0*/0; // approximation is correct to 30 decimals
            else if (x > 20.0) return 1.0;
            else
            {
                var result = Math.Tanh(x);
                return result >= 0 ? result : 0;
            }
        }

        private static double ReLU(double x)
        {
            if (x <= 0) return 0;
            else return x;
        }

       
        //public void CrossValidationLievas(double[][] trainDataArray, double[][] targets, int maxEpochs, double learnRate, double momentum, BPNeuralNetwork nn)
        //{
        //    int validationCount = 10;
        //    (var data, var target) = CrossValidationSplitData(trainDataArray, targets, validationCount);
        //    for (int i = 0; i < validationCount; i++)
        //    {
        //        Console.WriteLine("Cross Validation Train - {0}", i);
        //        var trainData = data.Select((s, k) => new { s, k }).Where(w => w.k != i).SelectMany(s => s.s).ToArray();
        //        var targetData = target.Select((s, k) => new { s, k }).Where(w => w.k != i).SelectMany(s => s.s).ToArray();
        //        Train(trainData, targetData, maxEpochs, learnRate, momentum);

        //        Console.WriteLine("Cross Validation Test - {0}", i);
        //        for (int k = 0; k < data[i].Length; k++)
        //        {
        //            nn.ComputeOutputs(data[i][k], target[i][k]);
        //        }
        //    }
        //}

        public (double[] trainErrors, double[] validationErrors, double[] testErrors) Train(double[][] trainDataArray, double[][] targets, int maxEpochs,
          double learnRate, double momentum)
        {
            // train using back-prop
            // back-prop specific arrays
            double[][] hoGrads = MakeMatrix(numHidden, numOutput, 0.0); // hidden-to-output weight gradients
            double[] obGrads = new double[numOutput];                   // output bias gradients

            double[][] ihGrads = MakeMatrix(numInput, numHidden, 0.0);  // input-to-hidden weight gradients
            double[] hbGrads = new double[numHidden];                   // hidden bias gradients

            double[] oSignals = new double[numOutput];                  // local gradient output signals - gradients w/o associated input terms
            double[] hSignals = new double[numHidden];                  // local gradient hidden node signals

            // back-prop momentum specific arrays 
            double[][] ihPrevWeightsDelta = MakeMatrix(numInput, numHidden, 0.0);
            double[] hPrevBiasesDelta = new double[numHidden];
            double[][] hoPrevWeightsDelta = MakeMatrix(numHidden, numOutput, 0.0);
            double[] oPrevBiasesDelta = new double[numOutput];

            int epoch = 0;
            double[] xValues = new double[numInput]; // inputs
            //double[][] tValues = targets; // target values
            double derivative = 0.0;
            double errorSignal = 0.0;

            //int[] sequence = new int[trainData.Length];
            //for (int i = 0; i < sequence.Length; ++i)
            //    sequence[i] = i;

            int validationCount = 10;
            (var data, var target) = CrossValidationSplitData(trainDataArray, targets, validationCount);

            List<double> errors = new List<double>();
            List<double> validationErrors = new List<double>();
            List<double> testErrors = new List<double>();
            int errInterval = maxEpochs / 10; // interval to check error
            while (epoch < maxEpochs)
            {

                //current data section index for validation data extraction
                int currentValDataIdx = epoch % validationCount;

                //Split data for k-means cross validation
                var trainData = data.Select((s, k) => new { s, k }).Where(w => w.k != currentValDataIdx).SelectMany(s => s.s).ToArray();
                var targetData = target.Select((s, k) => new { s, k }).Where(w => w.k != currentValDataIdx).SelectMany(s => s.s).ToArray();
                var validationData = data.Select((s, k) => new { s, k }).Where(w => w.k == currentValDataIdx).SelectMany(s => s.s).ToArray();
                var validationTargets = target.Select((s, k) => new { s, k }).Where(w => w.k == currentValDataIdx).SelectMany(s => s.s).ToArray();

                //test network before training
                if (epoch == 0)
                {
                    double trainErr = Error(trainData, targetData);
                    double validationErr = Error(validationData, validationTargets);
                    double testErr = Error(testData, testTargets);
                    Console.WriteLine(string.Format("Testing on data before training the network \n    Training data error   = {0:0.00000000}\n    Validation data error = {1:0.00000000}\n    Test data error       = {2:0.00000000}", trainErr, validationErr, testErr));
                    //Console.ReadLine();
                }

                //Shuffle(sequence); // visit each training data in random order
                for (int ii = 0; ii < trainData.Length; ++ii)
                {
                    //int idx = sequence[ii];
                    //Array.Copy(trainData[idx], xValues, numInput);
                    //Array.Copy(trainData[idx], numInput, tValues, 0, numOutput);
                    ComputeOutputs(trainData[ii]); // copy xValues in, compute outputs 

                    // indices: i = inputs, j = hiddens, k = outputs

                    // 1. compute output node signals (assumes softmax)
                    for (int k = 0; k < numOutput; ++k)
                    {
                        errorSignal = targetData[ii][k] - outputs[k];  // Wikipedia uses (o-t)
                        //derivative = (1 - outputs[k]) * outputs[k]; // for softmax
                        derivative = 1; // for ReLU
                        oSignals[k] = errorSignal * derivative;
                    }

                    // 2. compute hidden-to-output weight gradients using output signals
                    for (int j = 0; j < numHidden; ++j)
                        for (int k = 0; k < numOutput; ++k)
                            hoGrads[j][k] = oSignals[k] * hOutputs[j];

                    // 2b. compute output bias gradients using output signals
                    for (int k = 0; k < numOutput; ++k)
                        obGrads[k] = oSignals[k] * 1.0; // dummy assoc. input value

                    // 3. compute hidden node signals
                    for (int j = 0; j < numHidden; ++j)
                    {
                        //derivative = (1 + hOutputs[j]) * (1 - hOutputs[j]); // for tanh
                        derivative = hOutputs[j] <= 0 ? 0 : 1; // for ReLU
                        double sum = 0.0; // need sums of output signals times hidden-to-output weights
                        for (int k = 0; k < numOutput; ++k)
                        {
                            sum += oSignals[k] * hoWeights[j][k]; // represents error signal
                        }
                        hSignals[j] = derivative * sum;
                    }

                    // 4. compute input-hidden weight gradients
                    for (int i = 0; i < numInput; ++i)
                        for (int j = 0; j < numHidden; ++j)
                            ihGrads[i][j] = hSignals[j] * inputs[i];

                    // 4b. compute hidden node bias gradients
                    for (int j = 0; j < numHidden; ++j)
                        hbGrads[j] = hSignals[j] * 1.0; // dummy 1.0 input

                    // == update weights and biases

                    // update input-to-hidden weights
                    for (int i = 0; i < numInput; ++i)
                    {
                        for (int j = 0; j < numHidden; ++j)
                        {
                            double delta = ihGrads[i][j] * learnRate;
                            ihWeights[i][j] += delta; // would be -= if (o-t)
                            ihWeights[i][j] += ihPrevWeightsDelta[i][j] * momentum;
                            ihPrevWeightsDelta[i][j] = delta; // save for next time
                        }
                    }

                    // update hidden biases
                    for (int j = 0; j < numHidden; ++j)
                    {
                        double delta = hbGrads[j] * learnRate;
                        hBiases[j] += delta;
                        hBiases[j] += hPrevBiasesDelta[j] * momentum;
                        hPrevBiasesDelta[j] = delta;
                    }

                    // update hidden-to-output weights
                    for (int j = 0; j < numHidden; ++j)
                    {
                        for (int k = 0; k < numOutput; ++k)
                        {
                            double delta = hoGrads[j][k] * learnRate;
                            hoWeights[j][k] += delta;
                            hoWeights[j][k] += hoPrevWeightsDelta[j][k] * momentum;
                            hoPrevWeightsDelta[j][k] = delta;
                        }
                    }

                    // update output node biases
                    for (int k = 0; k < numOutput; ++k)
                    {
                        double delta = obGrads[k] * learnRate;
                        oBiases[k] += delta;
                        oBiases[k] += oPrevBiasesDelta[k] * momentum;
                        oPrevBiasesDelta[k] = delta;
                    }

                } // each training item
                
                //if (epoch % errInterval == 0 && epoch < maxEpochs)
                {
                    double trainErr = Error(trainData, targetData);
                    double validationErr = Error(validationData, validationTargets);
                    double testErr = Error(testData, testTargets);
                    errors.Add(trainErr);
                    validationErrors.Add(validationErr);
                    testErrors.Add(testErr);
                    Console.WriteLine(string.Format("Epoch = {0,3} \n    Training data error   = {1:0.00000000}\n    Validation data error = {2:0.00000000}\n    Test data error       = {3:0.00000000}", epoch, trainErr, validationErr, testErr));
                    //Console.ReadLine();
                }
                epoch++;

            } // while
            Console.WriteLine("Average error: {0}", errors.Average());
            
            return (errors.ToArray(), validationErrors.ToArray(), testErrors.ToArray());
        } // Train

        private void Shuffle(int[] sequence) // instance method
        {
            for (int i = 0; i < sequence.Length; ++i)
            {
                int r = this.rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }
        } // Shuffle

        private double Error(double[][] trainData, double[][] targetData)
        {
            // average squared error per training item
            double sumSquaredError = 0.0;
            double[] xValues = new double[numInput]; // first numInput values in trainData
            //double[] tValues = new double[numOutput]; // last numOutput values

            // walk thru each training case. looks like (6.9 3.2 5.7 2.3) (0 0 1)
            for (int i = 0; i < trainData.Length; ++i)
            {
                Array.Copy(trainData[i], xValues, numInput);
                //Array.Copy(trainData[i], numInput, tValues, 0, numOutput); // get target values
                double[] yValues = this.ComputeOutputs(xValues); // outputs using current weights
                //Console.WriteLine("\nResult:\n" + yValues[0] + "\nTarget:\n" + targetData[i][0]);
                for (int j = 0; j < numOutput; ++j)
                {
                    double err = targetData[i][j] - yValues[j];
                    sumSquaredError += err * err;
                }
            }
            return sumSquaredError / trainData.Length;
        } // MeanSquaredError

        public double Accuracy(double[][] testData)
        {
            // percentage correct using winner-takes all
            int numCorrect = 0;
            int numWrong = 0;
            double[] xValues = new double[numInput]; // inputs
            double[] tValues = new double[numOutput]; // targets
            double[] yValues; // computed Y

            for (int i = 0; i < testData.Length; ++i)
            {
                Array.Copy(testData[i], xValues, numInput); // get x-values
                Array.Copy(testData[i], numInput, tValues, 0, numOutput); // get t-values
                yValues = this.ComputeOutputs(xValues);
                int maxIndex = MaxIndex(yValues); // which cell in yValues has largest value?
                int tMaxIndex = MaxIndex(tValues);

                if (maxIndex == tMaxIndex)
                    ++numCorrect;
                else
                    ++numWrong;
            }
            return (numCorrect * 1.0) / (numCorrect + numWrong);
        }

        private static int MaxIndex(double[] vector) // helper for Accuracy()
        {
            // index of largest value
            int bigIndex = 0;
            double biggestVal = vector[0];
            for (int i = 0; i < vector.Length; ++i)
            {
                if (vector[i] > biggestVal)
                {
                    biggestVal = vector[i];
                    bigIndex = i;
                }
            }
            return bigIndex;
        }
        static (List<double[][]> dataCrossVal, List<double[][]> targetCrossVal) CrossValidationSplitData(double[][] dataArray, double[][] targetArray, int crossValCount)
        {
            List<double[][]> dataCrossVal = new List<double[][]>();
            List<double[][]> targetCrossVal = new List<double[][]>();

            var dataRows = dataArray.Select(row => row.ToArray());
            var targetRows = targetArray.Select(row => row.ToArray());

            for (int k = 0; k < crossValCount; k++)
            {
                var data = dataRows.Select((s, i) => new { s, i }).Where(w => (w.i >= k * dataArray.Length / crossValCount) && (w.i < (k + 1) * dataArray.Length / crossValCount)).Select(s => s.s).ToArray();
                dataCrossVal.Add(data);
                var target = targetRows.Select((s, i) => new { s, i }).Where(w => (w.i >= k * targetArray.Length / crossValCount) && (w.i < (k + 1) * targetArray.Length / crossValCount)).Select(s => s.s).ToArray();
                targetCrossVal.Add(target);
            }

            return (dataCrossVal, targetCrossVal);
        }

        
    } // NeuralNetwork
}
