using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TIPIS_LR1
{
    using MH = MathNet.Numerics.Distributions;
    using chrt = System.Windows.Forms.DataVisualization.Charting;
    public partial class Form1 : Form
    {
         public Form1(int N, Int32 M)
        {
            InitializeComponent();
            this.N = N; this.M = M;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private readonly Random RND = new Random();


        public Int32 N;// Число отрезков
         //-----------------------------------------------------------1 ЗАДАНИЕ-------------------------------------------

        private long M; // Число экспериментов

        public Dictionary<double, double> T1(double a, double b, int N, int M)
        {
            var f = new double[N];
            var d = (b - a) / N;

            var range = Enumerable.Range(0, N)
                .Select((e) => a + e * d).ToArray();

            for (long m = 0; m < M; m++)
            {
                double x = random(a, b);
                f[(int)((x - a) / (b - a) * N)] += 1;
            }
            var result = new Dictionary<double, double>();
            for (int n = 0; n < N; n++)
            {
                result.Add(range[n], f[n] / (M * d));
            }
            return result;

            double random(double _a, double _b)
                => _a + (_b - _a) * RND.NextDouble();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = -2;
            chart1.ChartAreas[0].AxisX.Maximum = 7;
     

            chrt.Series seriesOne =
                new chrt.Series()
                {
                    Color = Color.HotPink,
                    Name = "1 График",
                    ChartType = SeriesChartType.Spline,
                    BorderWidth = 4,
                };
            chrt.Series seriesTwo =
                 new chrt.Series()
                 {
                     Color = Color.Indigo,
                     Name = "2 График",
                     ChartType = SeriesChartType.Spline,
                     BorderWidth = 5,
                 };

            int M2 = (int)Mumeric2.Value;
            int M1 = (int)Mumeric1.Value;
            N = (int)Numeric1.Value;
            foreach (var ob in T1(-2, 7, N, M1))
            {
                seriesOne.Points.Add(new DataPoint(ob.Key, ob.Value));
            }
            foreach (var obj in T1(-2, 7, N, M2))
            {
                seriesTwo.Points.Add(new DataPoint(obj.Key, obj.Value));
            }
            chart1.Series.Add(seriesOne);
            chart1.Series.Add(seriesTwo);
        }

        //-----------------------------------------------------------2 ЗАДАНИЕ-------------------------------------------


        public Dictionary<double, double> T2()
        {
            double[] X = { 5, 25, 55, 7, 19, 21, 17 };

            var list = new Dictionary<double, int[]>()
            {
                [0.01] = new int[] { 5 },
                [0.02] = new int[] { 25, 55 },
                [0.05] = new int[] { 7 },
                [0.3] = new int[] { 19, 21, 17 },
            };
            var results = new Dictionary<double, double>();

            for (int i = 0; i < this.N; i++)
            {
                var rand = random();

                if (results.ContainsKey(rand)) results[rand]++;
                else results.Add(rand, 1);
            }
            return results.OrderBy((e) => e.Key)
                .Select((e) => new KeyValuePair<double, double>(e.Key, e.Value / this.N))
                .ToDictionary((e) => e.Key, (e) => e.Value);

            double random()
            {
                double random_num = default, x_exit = default;
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == 0) { random_num = RND.NextDouble(); }

                    var item = list.ElementAt<KeyValuePair<double, int[]>>(i);
                    if (random_num < item.Key)
                    {
                        var rand = RND.Next(item.Value.Length);
                        x_exit = item.Value[rand]; break;
                    }
                    if (x_exit == default && i >= list.Count - 1) i = -1;
                }
                return x_exit;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = 5;
            chart1.ChartAreas[0].AxisX.Maximum = 55;

            chrt.Series series =
                new chrt.Series()
                {
                    Color = Color.Chocolate,
                    Name = "График",
                    BorderWidth = 5,
                    Tag = "X",

                };

            foreach (var obj in new Form1((int)this.numericUpDown1.Value, 10).T2())
            {
                series.Points.Add(new DataPoint(obj.Key, obj.Value));
            }

            chart2.Series.Add(series);
        }

        ////-----------------------------------------------------------3 ЗАДАНИЕ-------------------------------------------
        public Dictionary <double, double> T3(double a, double b, double std, double mean, int N, int M)
        {
            var dist_random = this.T1(a, b, N, M);

            double d = (b - a) / N;
            double[] f = new double[N];

            for (int m = 0; m < M; m++)
            {
                var x = GaussRandom();

                int n = (int)((x - a) / (b - a) * N);
                f[n] = f[n] + 1;
            }
            for (int n = 0; n < N; n++)
            {
                f[n] = f[n] / (M * d);
                dist_random[dist_random.ElementAt(n).Key] += f[n];
            }
            return dist_random;

            double GaussRandom()
            {
                double retval = default;
                while (true)
                {
                    retval = MH::Normal.Sample(RND, mean, std);
                    if (retval <= b && retval >= a) { break; }
                }
                return retval;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            chart3.Series.Clear();
            int n = (int)numericUpDown2.Value;
            int m = (int)numericUpDown3.Value;
            chrt.Series series =
                new chrt.Series()
            {
                Color = Color.YellowGreen,
                BorderWidth = 5,
                ChartType = SeriesChartType.FastLine,
            };
            foreach (var obj in T3(5, 7, 2, 3, n, m))
            {
                series.Points.Add(new DataPoint(Math.Round(obj.Key, 4, MidpointRounding.AwayFromZero), obj.Value));
            }

            chart3.Series.Add(series);
        }
    }     
    }

