﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace PUBGStatistics
{
    public partial class Diagrams : Form
    { 
        public Diagrams(double[][] data, double[][] targets, double min, double max)
        {
            InitializeComponent();

            var data1 = Get1stClass(data, targets, min, max);
            var data2 = Get2ndClass(data, targets, min, max);
            var data3 = Get3rdClass(data, targets, min, max);

            ChartValues<ObservablePoint> points1 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> points2 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> points3 = new ChartValues<ObservablePoint>();
            cartesianChart1.Series = new SeriesCollection
            {
                new ScatterSeries
                {
                    Title = "< 50 Kills",
                    Values = points1,
                    //PointGeometry = DefaultGeometries.Triangle,
                    MinPointShapeDiameter = 5,
                    MaxPointShapeDiameter = 10
                },
                new ScatterSeries
                {
                    Title = "> 50 && < 150 Kills",
                    Values = points2,
                    PointGeometry = DefaultGeometries.Square,
                    MinPointShapeDiameter = 5,
                    MaxPointShapeDiameter = 10
                },
                new ScatterSeries
                {
                    Title = "> 150 Kills",
                    Values = points3,
                    PointGeometry = DefaultGeometries.Triangle,
                    MinPointShapeDiameter = 5,
                    MaxPointShapeDiameter = 10
                }
            };
            for (int i = 0; i < 1000; i++)
            {
                points1.Add(new ObservablePoint
                {
                    X = data1[i][0],
                    Y = data1[i][1]
                });
            }
            for (int i = 0; i < 1000; i++)
            {
                points2.Add(new ObservablePoint
                {
                    X = data2[i][0],
                    Y = data2[i][1]
                });
            }
            for (int i = 0; i < 1000; i++)
            {
                points3.Add(new ObservablePoint
                {
                    X = data3[i][0],
                    Y = data3[i][1]
                });
            }
            //foreach (var item in data)
            //{
            //    points.Add(new ObservablePoint
            //    {
            //        X = item[0],
            //        Y = item[1]
            //    });
            //}

        }

        /// <summary>
        /// Chart for validation errors
        /// </summary>
        public Diagrams(double[] trainErr, double[] validationErr, double[] testErr)
        {
            InitializeComponent();

            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Training",
                    Values = new ChartValues<double>(trainErr),
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Validation",
                    Values = new ChartValues<double> (validationErr),
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Testing",
                    Values = new ChartValues<double> (testErr),
                    PointGeometry = null
                }
            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Epoch"           
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Mean Squared Error",
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;

            label1.Text = "Kryžminės validacijos rezultatai, naudojant 10 dalių (10 means cross validation)";

        }
        double[][] Get1stClass(double[][] data, double[][] targets, double min, double max)//(50 - min) / (max - min);
        {
            return data.Select((s, i) => new { s, i }).Where(w => targets[w.i][0] < ((50 - min) / (max - min)) ).Select(s => s.s).ToArray();
        }
        double[][] Get2ndClass(double[][] data, double[][] targets, double min, double max)
        {
            return data.Select((s, i) => new { s, i }).Where(w => targets[w.i][0] > ((50 - min) / (max - min)) && targets[w.i][0] < ((150 - min) / (max - min))).Select(s => s.s).ToArray();
        }
        double[][] Get3rdClass(double[][] data, double[][] targets, double min, double max)
        {
            return data.Select((s, i) => new { s, i }).Where(w => targets[w.i][0] > ((150 - min) / (max - min))).Select(s => s.s).ToArray();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
