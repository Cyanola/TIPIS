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

namespace LR4
{
    using MH = MathNet.Numerics.Distributions;
    using chrt = System.Windows.Forms.DataVisualization.Charting;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random RND = new Random();
        double GaussRandom(Predicate<double> state, double a, double b)
        {
            double retval = default;
            while (true)
            {
                retval = MathNet.Numerics.Distributions.Normal.Sample(RND, a, b);
                if (state.Invoke(retval)) { break; }
            }
            return retval;
        }

        public List<(double, double, double)> T1(int N, double a, double b)
        {
            double[] y = new double[N];
            double[] m_rek = new double[N];
            double[] m_ist = new double[N];
            int i;
            var result = new List<(double, double, double)>();

            for (i = 0; i < N; i++)
            {
                for (i = 1; i < N; i++) m_ist[i] = Math.Exp(0 * 0 / 2.0) / Math.Sqrt(2 * Math.PI);

                for (i = 1; i < N; i++)
                {
                    y[i] = GaussRandom((_) => true, a, b);
                    if (y[i] <= 0) y[i] = 0;
                    else y[i] = Math.Pow(y[i], 2);
                }

                m_rek[1] = y[1];

                for (i = 2; i < N; i++) m_rek[i] = (i - 1.0) / i * m_rek[i - 1] + 1.0 / i * y[i];

            }

            for (int k = 0; k < N; k++)
            {
                var item = (y[k], m_rek[k], m_ist[k]);
                result.Add(item);
                Console.WriteLine($"y[{k}]= {y[k]}\nm_rek[{k}]= {m_rek[k]}\nm_ist[{k}]= {m_ist[k]}\n");
            }

            return result;
        }

        public List<(double, double, double)> T3(int N, double a, double b)
        {
            double[] y = new double[N];
            double[] m_rek = new double[N];
            double[] m_ist = new double[N];
            int i;
            var result = new List<(double, double, double)>();

            for (i = 0; i < N; i++)
            {
                for (i = 1; i < N; i++) m_ist[i] = Math.Exp(0 * 0 / 2.0) / Math.Sqrt(2 * Math.PI);

                for (i = 1; i < N; i++)
                {
                    y[i] = GaussRandom((_) => true, a, b);
                    Console.WriteLine("feee" + y[i]);
                    y[i] = Math.Pow(y[i], 2);
                }

                m_rek[1] = y[1];

                for (i = 2; i < N; i++) m_rek[i] = (i - 1.0) / i * m_rek[i - 1] + 1.0 / i * y[i];
            }

            for (int k = 0; k < N; k++)
            {
                var item = (y[k], m_rek[k], m_ist[k]);
                result.Add(item);
                Console.WriteLine($"y[{k}]= {y[k]}\nm_rek[{k}]= {m_rek[k]}\nm_ist[{k}]= {m_ist[k]}\n");
            }

            return result;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            chrt.Series seriesOne =
              new chrt.Series()
              {
                  Color = Color.IndianRed,
                  Name = "График 1",
                  ChartType = SeriesChartType.Spline,
                  BorderWidth = 4,
              };

            chrt.Series seriesTwo =
             new chrt.Series()
             {
                 Color = Color.SteelBlue,
                 Name = "График 2",
                 ChartType = SeriesChartType.Spline,
                 BorderWidth = 4,
             };
            int N = (int)numericUpDown2.Value;
            var result = T1(N - 1, 0, 1);
            var result2 = T1(N - 1, 0, 1);

            foreach (var item in result)
            {
                seriesOne.Points.Add(new DataPoint(item.Item1, item.Item2));
            }
            foreach (var item in result2)
            {
                seriesTwo.Points.Add(new DataPoint(item.Item1, item.Item3));
            }

            chart1.Series.Add(seriesOne);
            chart1.Series.Add(seriesTwo);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart3.Series.Clear();

            chrt.Series seriesOne =
              new chrt.Series()
              {
                  Color = Color.SkyBlue,
                  Name = "График 1",
                  ChartType = SeriesChartType.Spline,
                  BorderWidth = 4,
              };

            chrt.Series seriesTwo =
             new chrt.Series()
             {
                 Color = Color.Pink,
                 Name = "График 2",
                 ChartType = SeriesChartType.Spline,
                 BorderWidth = 4,
             };
            int N = (int)numericUpDown2.Value;
            var result = T3(N - 1, 0, 1);
            var result2 = T3(N - 1, 0, 1);

            foreach (var item in result)
            {
                seriesOne.Points.Add(new DataPoint(item.Item1, item.Item2));
            }
            foreach (var item in result2)
            {
                seriesTwo.Points.Add(new DataPoint(item.Item1, item.Item3));
            }

            chart3.Series.Add(seriesOne);
            chart3.Series.Add(seriesTwo);
        }

        public List<(double, double, double)> T5_system1(int N, double a, double b)
        {
            double[] y = new double[N];
            double[] m_rek = new double[N];
            double[] m_ist = new double[N];
            int i;
            var result = new List<(double, double, double)>();

            for (i = 0; i < N; i++)
            {
                for (i = 1; i < N; i++) m_ist[i] = Math.Exp(-0.5 * 0.5 / 2.0) / Math.Sqrt(2 * Math.PI);

                for (i = 1; i < N; i++)
                {
                    y[i] = GaussRandom((_) => true, a, b);
                    if (y[i] <= 0.5) y[i] = y[i];
                    else y[i] = -y[i];
                }

                m_rek[1] = y[1];

                for (i = 2; i < N; i++) m_rek[i] = (i - 1.0) / i * m_rek[i - 1] + 1.0 / i * y[i];

            }

            for (int k = 0; k < N; k++)
            {
                var item = (y[k], m_rek[k], m_ist[k]);
                result.Add(item);
           
            }

            return result;
        }

        public List<(double, double, double)> T5_system2(int N, double a, double b)
        {
            double[] y = new double[N];
          double[]  m_rek = new double[N];
            double[] m_ist = new double[N];
            int i;
            var result = new List<(double, double, double)>();

            for (i = 0; i < N; i++)
            {
                for (i = 1; i < N; i++) m_ist[i] = Math.Exp(0 * 0 / 2.0) / Math.Sqrt(2 * Math.PI);

                for (i = 1; i < N; i++)
                {
                    y[i] = GaussRandom((_) => true, a, b);
                    if (y[i] <= 0) y[i] = 0;
                    else y[i] = y[i];
                }

                m_rek[1] = y[1];

                for (i = 2; i < N; i++) m_rek[i] = (i - 1.0) / i * m_rek[i - 1] + 1.0 / i * y[i];

            }

            for (int k = 0; k < N; k++)
            {
                var item = (y[k], m_rek[k], m_ist[k]);
                result.Add(item);
          
            }

            return result;
        }
        public List<(double, double, double)>Difference(List<(double, double, double)> list1, List<(double, double, double)> list2)
        {
            return list1.Zip(list2, (item1, item2) => (item1.Item1, item1.Item2 - item2.Item2, item1.Item3)).ToList();
        }
        public List<(double, double, double)> Difference_2(List<(double, double, double)> list1, List<(double, double, double)> list2)
        {
            return list1.Zip(list2, (item1, item2) => (item1.Item1, item1.Item2, item1.Item3 - item2.Item3)).ToList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();

            chrt.Series seriesOne =
              new chrt.Series()
              {
                  Color = Color.SkyBlue,
                  Name = "Система 1",
                  ChartType = SeriesChartType.Spline,
                  BorderWidth = 4,
              };

            chrt.Series seriesTwo =
             new chrt.Series()
             {
                 Color = Color.Pink,
                 Name = "Система 2",
                 ChartType = SeriesChartType.Spline,
                 BorderWidth = 4,
             };

            chrt.Series seriesThree =
             new chrt.Series()
             {
                 Color = Color.DarkGreen,
                 Name = "Разность",
                 ChartType = SeriesChartType.Spline,
                 BorderWidth = 4,
             };

            chrt.Series seriesFour =
             new chrt.Series()
             {
                 Color = Color.IndianRed,
                 Name = "Теоретическая\n истинная\n разность",
                 ChartType = SeriesChartType.Spline,
                 BorderWidth = 4,
             };
            int N1 = (int)numericUpDown1.Value;
            int N2 = (int)numericUpDown6.Value;
            var result = T5_system1(N1 - 1, 0, 1);
            var result2 = T5_system2(N2 - 1, 0, 1);
            var result3 = Difference(result2, result);
            var result4 = Difference_2(result2, result);

            foreach (var item in result)
            {
                seriesOne.Points.Add(new DataPoint(item.Item1, item.Item2));
            }
            foreach (var item in result2)
            {
                seriesTwo.Points.Add(new DataPoint(item.Item1, item.Item2));
            }
            foreach (var item in result3)
            {
                seriesThree.Points.Add(new DataPoint(item.Item1, item.Item2));
                seriesFour.Points.Add(new DataPoint(item.Item1, item.Item3));
            }
            foreach (var item in result4)
            {
            
                seriesFour.Points.Add(new DataPoint(item.Item1, item.Item3));
            }

            chart2.Series.Add(seriesOne);
            chart2.Series.Add(seriesTwo);
            chart2.Series.Add(seriesThree);
            chart2.Series.Add(seriesFour);

        }


        public Dictionary<double, double> T8(int N, double a, double b, int MM, int L)
        {
            double[] s = new double[N], k = new double[N], x = new double[N];
            double porog = 15;
            for (int i = 0; i < N; i++) s[i] = Math.Sin(-2.0 * Math.PI * i / N);
            for (int i = 0; i < N; i++) k[i] = s[N - 1 - i];
            var result = new Dictionary<double, double>();
            var d_prav = new double[MM];
            for (int n = 0; n < MM; n++)
            {
                var A = 0.2 + 0.05 * n;
                for (int j = 0; j <L; j++)
                {
                    for (int i = 0; i < N; i++) x[i] = GaussRandom((_) => true) + A * s[i];
                    var z = Solg();
                    if (z >= porog) d_prav[n] += 1.0 / L;
                }
                result.Add(A, d_prav[n]);
            }
            double Solg()
            {
                var sym = default(double);
                for (int i = 0; i < N; i++) sym = sym + x[i] * k[N - 1 - i];
                return sym;
            }
            return result;
            double GaussRandom(Predicate<double> state)
            {
                double retval = default;
                while (true)
                {
                    retval = MH::Normal.Sample(RND, a, b);
                    if (state.Invoke(retval)) { break; }
                }
                return retval;
            }
            double sogl()
            {
                var sym = default(double);
                for (int i = 0; i < N; i++) sym = sym + x[i] * k[N - 1 - i];
                return sym;
            }
        }
    }
}
