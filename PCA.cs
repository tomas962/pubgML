﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System.Numerics;

namespace PUBGStatistics
{
    class PCA
    {
        static public List<List<double>> Compute(List<List<double>> data, int N)
        {
            //int N = 5; //components to take
            var mean = GetMean(data);
            var shiftedValue = ShiftValue(data, mean);
            var covariance = GetCovarianceMatrix(shiftedValue, mean);
            //PrintCov(covariance);
            var eigenV = GetEigen(covariance, N);

            var oldData = ConvToMatrix(data);

            var newData = oldData * eigenV;

            var newDataList = new List<List<double>>();

            for (int i = 0; i < data.Count; i++)
            {
                newDataList.Add(newData.Row(i).ToList());
            }
            return newDataList;
        }
        static void PrintCov(List<List<double>> cov)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(" {0, 5:f2} ", cov[i][j]);
                }
                Console.Write("...");
                Console.WriteLine();
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(" {0, 5} ", ".");
                }
                Console.WriteLine();
            }
            //foreach (var row in cov)
            //{
            //    foreach (var item in row)
            //    {
            //        Console.Write(" {0, 5:f2} ", item);
            //    }
            //    Console.WriteLine();
            //}
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
        static Matrix<double> GetEigen(List<List<double>> covariance, int N)
        {
            Matrix<double> matrix = ConvToMatrix(covariance);
            Evd<double> eigen = matrix.Evd();
            //Vector<Complex> eigenValues = eigen.EigenValues;
            //Console.WriteLine(eigenValues.ToString());
            Matrix<double> eigenVectors = eigen.EigenVectors;

            return eigenVectors.SubMatrix(0,eigenVectors.RowCount,0,N);
        }
    }
}
