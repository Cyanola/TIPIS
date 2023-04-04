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

namespace LR3
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
        double tau = 6.2831853071;
     double Primitive(double x)
        {
            // return (double)( (8 * Math.Sin((x / 2.0) - 7) - 64 * Math.Cos((x / 2.0) - 7)) / (325.0 * Math.Pow(Math.E, 4 * x)));
            //return (4 * tau - 4 * x - 139) / (20 * Math.Pow(Math.E, 4 * tau));
          return -139.0 / (20 * Math.Pow(Math.E, 4 * tau)) + x / 5 + 139.0 / 20.0;
        }
        public Dictionary<double, double> T1(int a, int b)
        {
          
            double[] Y_n = new double[b+1];
           
            var result  = new Dictionary<double, double>();
            for (int x = 0; x <= b; x++)
            {
                var t = GaussRandom((_) => true);
               
                Y_n[x] =Newton(t);
                result.Add(t, Y_n[x]);
            }
            return result;/*.OrderBy((e) => e.Key)*/
            //  .Select((e) => new KeyValuePair<double, double>(e.Key, e.Value / b))
            //  .ToDictionary((e) => e.Key, (e) => e.Value);

            double Newton(double t)
            {
                return Primitive(t) - Primitive(0);
            }
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
    }
      
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            //   chart1.ChartAreas[0].AxisY.Minimum = 0;
            chrt.Series seriesOne =
              new chrt.Series()
              {
                  Color = Color.IndianRed,
                  Name = "График",
                  ChartType = SeriesChartType.Spline,
                  BorderWidth = 4,
              };

            var result = T1(0, 5);

            foreach (var item in result)
            {
                seriesOne.Points.Add(new DataPoint(item.Key, item.Value));
            }
            chart1.Series.Add(seriesOne);
        }

        public Dictionary<double, double> T2(double a, double b, int L, int N)
        {
            double[] s = new double[L], k = new double[L], x = new double[N], y = new double[N];
            var result = new Dictionary<double, double>();
            int i, p, n;
         
            for (i = 0; i < L; i++)
            { s[i] = 1.0 * i / L; x[i] = s[i]; }

            for (i = 0; i < L; i++)
                k[i] = s[L - i - 1];
            for (i = 2 * L; i < 3 * L; i++)
                x[i] = x[i] + s[i - 2 * L];
            // Добавление шума во входную реализацию
            for (i = 0; i < N; i++)
                x[i] = x[i] + GaussRandom((_)=>true);
            // Согласованная фильтрация
            for (i = 0; i < N; i++)
            {
                y[i] = 0.0;
                for (p = 0; p < L; p++)
                {
                    if ((i - p) >= 0)
                        y[i] = y[i] + x[i - p] * k[p];
                }
                result.Add(x[i], y[i]);
            }
            return result;
            double GaussRandom( Predicate<double> state)
            {
                double retval = default;
                while (true)
                {
                    retval = MH::Normal.Sample(RND, a, b);
                    if (state.Invoke(retval)) { break; }
                }
                return retval;
            }
        }
            private void button2_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();
            //   chart1.ChartAreas[0].AxisY.Minimum = 0;
            chrt.Series seriesOne =
              new chrt.Series()
              {
                  Color = Color.OliveDrab,
                  Name = "График",
                  ChartType = SeriesChartType.Spline,
                  BorderWidth = 4,
              };
            int N = (int)(numericUpDown2.Value);
            int L= (int)(numericUpDown3.Value);
            var result = T2(0, 0.5, L, N);

            foreach (var item in result)
            {
                seriesOne.Points.Add(new DataPoint(item.Key, item.Value));
            }
            chart2.Series.Add(seriesOne);
        }

        public Dictionary<double, double> T3(double porog, int MM, int N,int a, int b)
        {

            double[] s = new double[N], k = new double[N], x = new double[N];

            for (int i = 0; i < N; i++) s[i] = Math.Sin(-2.0 * Math.PI * i / N);
            for (int i = 0; i < N; i++) k[i] = s[N - 1 - i];
            var result = new Dictionary<double, double>();
            var d_prav = new double[MM];
            for (int n = 0; n < MM; n++)
            {
                var A = 0.2 + 0.05 * n;
                for (int j = 0; j < N; j++)
                {
                    for (int i = 0; i < N; i++) x[i] = GaussRandom((_) => true) + A * s[i];
                    var z = Solg();
                    if (z >= porog) d_prav[n] += 1.0 / (double)N;
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
        
        }
        private void button3_Click(object sender, EventArgs e)
        {
            chart3.Series.Clear();
            //   chart1.ChartAreas[0].AxisY.Minimum = 0;
            chrt.Series seriesOne =
              new chrt.Series()
              {
                  Color = Color.Green,
                  Name = "График",
                  ChartType = SeriesChartType.Spline,
                  BorderWidth = 4,
              };
            int N = (int)numericUpDown1.Value;
            int L = (int)(numericUpDown4.Value);
            var result = T3(0.95, N, L, 0, 1);

            foreach (var item in result)
            {
                seriesOne.Points.Add(new DataPoint(item.Key, item.Value));
            }
            chart3.Series.Add(seriesOne);
        }
    }
}
