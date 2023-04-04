using MathNet.Numerics;
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

namespace LR2
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
      
        public List< double> T1(double mean, double std, double a,int N)
        {
           
           
            double k1, k2; int n;
            double[] x = new double[N];
     
            k2 = Math.Exp(-a);
            k1 = Math.Sqrt(std * (1.0 - k2 * k2));
            //x[0] = GaussRandom(0, std);
            //for (n = 1; n < N; n++)
            //{
            //    e = GaussRandom(0, 1);
            //    x[n] = k1 * e + k2 * x[n - 1];
            //}

            var func_result = new List<double>();
     
            func_result.Add(GaussRandom(mean, std,(_) => true));
            for (n = 1; n < N; n++)
            {
               
                func_result.Add(k1 * GaussRandom(mean, 1,(_) => true) + k2 * func_result[n - 1]);
            }
            return func_result;



        }
        double GaussRandom(double mean, double std, Predicate<double> state)
        {
            double retval = default;
            while (true)
            {
                retval = MH::Normal.Sample(RND, mean, std);
                if (state.Invoke(retval)) { break; }
            }
            return retval;
        }

        public double[] Otsenka_1(double std, int N, int M, double a)
        {

            double k1, k2; int n;
            double[] x = new double[N];

            k2 = Math.Exp(-a);
            k1 = Math.Sqrt(std * (1.0 - k2 * k2));
            int m;
            double[] r = new double[M];
            for (m = 0; m < M; m++)
            {
                r[m] = 0.0;
                for (n = 0; n < (N- m - 1); n++)
                    r[m] = r[m] + 1.0 / (N - m) * x[n] * x[n + m];

            }
            return r;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
         //   chart1.ChartAreas[0].AxisY.Minimum = 0;
            chrt.Series seriesOne =
              new chrt.Series()
              {
                  Color = Color.MediumVioletRed,
                  Name = "График",
                  ChartType = SeriesChartType.Spline,
                  BorderWidth = 4,
              };
          
           
           int N = (int)numericUpDown2.Value;
            var result = T1(2, 5, -Math.Log(0.95), N);

            foreach (var item in result)
            {
                seriesOne.Points.Add(item);
            }
            chart1.Series.Add(seriesOne);
            chrt.Series seriesFour =
      new chrt.Series()
      {
          Color = Color.CadetBlue,
          Name = "Оценка",
          ChartType = SeriesChartType.Spline,
          BorderWidth = 4,
      };
            int M = (int)numericUpDown3.Value;

            var res = Otsenka_1(5, N, M, -Math.Log(0.95));
            foreach (var item in res)
            {

                seriesFour.Points.Add(item);

            }
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = N - 1;

            chart1.Series.Add(seriesFour);
        }

        public List< double> T2(double b, int N)
        {
            var Xn = T1(0, 1, -Math.Log(0.02),N);
            var Yn = new List<double>();

            for (int n = 1; n < N; n++) { Yn.Add(2 * Xn[n] + b * Xn[n - 1]); }
            return Yn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();

          
            chrt.Series seriesTwo =
              new chrt.Series()
              {
                  Color = Color.MediumPurple,
                  Name = "График",
                  ChartType = SeriesChartType.Spline,
                  BorderWidth = 4,
              };
            int N = (int)numericUpDown1.Value;

            var result = T2(1,N);

            foreach (var item in result) { 

                seriesTwo.Points.Add(item);
               
            }
            chart2.Series.Add(seriesTwo);
            chrt.Series seriesThree =
           new chrt.Series()
           {
               Color = Color.Black,
               Name = "Оценка",
               ChartType = SeriesChartType.Spline,
               BorderWidth = 4,
           };
            int M = (int)numericUpDown4.Value;

            var res = Otsenka_4(1, N, M, 0.05);
            foreach (var item in res)
            {

                seriesThree.Points.Add(item);

            }
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = N - 1;
        
            chart2.Series.Add(seriesThree);
        }
        public double[] Otsenka_4(double std, int N,int M, double a)
        {
            double[] r = new double[M];
            int N_realiz;
            double[] x = new double [N],  e = new double[N];

            double[] c = new double[M]; double aa;
            int n, P, m, k;
            for (n = 0; n < N; n++)
                e[n] = GaussRandom(0, 1,(_) => true);
            P = (int)( 2 / a);
            for (k = 0; k <= P; k++)
            {
                if (k != 0)
                    c[k] = Math.Sqrt(std) / Math.Sqrt(Math.PI * a) * Math.Sin(a * k) / k;
                else c[k] = Math.Sqrt(std) / Math.Sqrt(Math.PI * a) * a;
            }
            for (n = 0; n < N; n++)
            {
                x[n] = 0.0;
                for (k = -P; k <= P; k++)
                {
                    if (k < 0) aa = c[-k];
                    else aa = c[k];
                    if (((n - k) >= 0) && ((n - k) < N))
                        x[n] = aa * e[n - k] + x[n];
                }
            }
            for (n = 0; n < (N - 2 * P); n++)
                x[n] = x[n + P];
            N_realiz = N - 2 * P;
            for (m = 0; m < M; m++)
            {
                r[m] = 0.0;
                for (n = 0; n < (N_realiz - m - 1); n++)
                    r[m] = r[m] + 1.0/ (N_realiz - m) * x[n] * x[n + m];

            }
            return r;
        }

    }
}
