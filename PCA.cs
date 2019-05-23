using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.LinearAlgebra.Factorization;

namespace PUBGStatistics
{
    class PCA
    {
        static public List<List<double>> Compute(List<List<double>> data)
        {
            int N = 5; //components to take
            var mean = GetMean(data);
            var shiftedValue = ShiftValue(data, mean);
            var covariance = GetCovarianceMatrix(shiftedValue, mean);
            var eigenV = GetEigen(covariance);

            var newData = new List<List<double>>();
            for (int i = 0; i < N; i++)
            {
                newData.Add(eigenV.Column(i).ToArray().ToList());
            }
            return newData;
        }
        static List<double> GetMean(List<List<double>> matrix)
        {
            List<double> mean = new List<double>();
            for (int i = 0; i < matrix[0].Count; i++)
            {
                mean.Add(matrix[i].Mean());
            }
            return mean;
        }
        static List<List<double>> ShiftValue(List<List<double>> matrix, List<double> mean)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    matrix[i][j] += mean[j];
                }
            }
            return matrix;
        }

        static List<List<double>> GetCovarianceMatrix(List<List<double>> matrix, List<double> mean)
        {
            var covarianceM = new List<List<double>>();

            for (int i = 0; i < mean.Count; i++)
            {
                var covariance = new List<double>();
                for (int j = 0; j < mean.Count; j++)
                {
                    covariance.Add(matrix[i].Covariance(matrix[j]));
                }
                covarianceM.Add(covariance);
            }
            return covarianceM;
        }
        static Matrix<double> ConvToMatrix(List<List<double>> data)
        {
            var array = new double[data.Count, data[0].Count];

            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    array[i, j] = data[i][j];
                }
            }
            return DenseMatrix.OfArray(array);
        }
        static Matrix<double> GetEigen(List<List<double>> covariance)
        {
            Matrix<double> matrix = ConvToMatrix(covariance);
            Evd<double> eigen = matrix.Evd();
            //Vector<Complex> eigenValues = eigen.EigenValues;
            Matrix<double> eigenVectors = eigen.EigenVectors;

            return eigenVectors;
        }
    }
}
