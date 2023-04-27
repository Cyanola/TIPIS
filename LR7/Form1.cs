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

namespace LR7
{
    using chrt = System.Windows.Forms.DataVisualization.Charting;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.View = System.Windows.Forms.View.SmallIcon;
        }

        //N=2

        double result;

        int V = 4;
        public double Calculate(int v, int Intensity)
        {
            double sum = 0;

            result = 0;
            result = Math.Pow(Intensity, v) / (double)Factorial(v);
            for (int j = 0; j <= v; j++)
            {
                sum += ((Math.Pow(Intensity, j)) / (double)Factorial(j));
            }
            result /= sum;

            return result;
        }
        private double Factorial(double n)
        {
            if (n == 1 || n == 0) return 1;
            return Factorial(n - 1) * n;
        }
        private double CalculatePInfinity(double v, int i, out double p0_infinity)
        {
           p0_infinity = default(double);
            for(int k = 0; k <= v; k++)
            {
                p0_infinity += (Math.Pow(v, k) / this.Factorial(k));
            }
            p0_infinity = 1 / (1.0 + p0_infinity);
            var pi_infinity = (Math.Pow(v, i) /this.Factorial((double)i)) * p0_infinity;
            return pi_infinity;
        }
        public sealed class FoundedModel : object
        {
            public (double P0, double PINf) Values { get; set; }
            public double I { get; set; }
            public double V { get; set; }
        }
        private FoundedModel[] CheckCroosing(int VN)
        {
            var result = new List<FoundedModel>();
            for(int i = 1; i <= VN; i++)
            {
                for(int j = 1; j <= VN; j++)
                {
                    var current = this.CalculatePInfinity(j, i,  out var p0_inf);
                    var previous = this.CalculatePInfinity(j, i - 1, out var p0_inf_prev);
                    if (Math.Round(current, 3) == Math.Round(previous, 3) && Math.Round(p0_inf, 3) == Math.Round(p0_inf_prev, 3))
                    {
                        result.Add(new FoundedModel { Values = (Math.Round(current, 3), Math.Round(p0_inf, 3)), I = i,  V = j });
                    }
                }
            }
            return result.ToArray();
        }
        //xd
        public sealed class QualityCharacteristics : Object
        {
            public double Pb { get; set; } = default; // Вероятность потери вызова
            public double Pt { get; set; } = default; // Вероятность потерь по времени
            public double Ph { get; set; } = default; // Вероятность потерь по нагрузке
            //xd
            public double Y { get; set; } = default; // Обслуженную нагрузку Y;
            public double R { get; set; } = default; // Избыточную нагрузку R;
            public double A { get; set; } = default; // Потенциальную нагрузку A.

            public QualityCharacteristics() : base() { }
        }
        //xd
        public QualityCharacteristics CalculateCharacter(int N)
        {
            var lq = 10 * (N + 1) / (double)(N + 4);
            var pivalue = GetPiValue(N);
            return new QualityCharacteristics()
            {
                Pb = pivalue, Pt = pivalue, Ph = lq / (double)(lq + N), 
                Y = lq * (1 - pivalue), R = lq * pivalue, A = lq
            };
            double GetPiValue(int i)
            {
                double value = 0;
                for (int j = 0; j <= N; j++) value += (Math.Pow(lq, j) / this.Factorial(j));
                return (Math.Pow(lq, i) / this.Factorial(i)) / value;
            }
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            listView1.Items.Clear();
            //chrt.Series seriesZero =
            //  new chrt.Series()
            //  {
            //      Color = Color.Green,
            //      Name = "Канал 0",
            //      ChartType = SeriesChartType.Spline,
            //      BorderWidth = 4,
            //  };

            chrt.Series seriesOne =
              new chrt.Series()
              {
                  Color = Color.Red,
                  Name = "Канал 1",
                  ChartType = SeriesChartType.Spline,
                  BorderWidth = 4,
              };
            chrt.Series seriesTwo =
             new chrt.Series()
             {
                 Color = Color.Violet,
                 Name = "Канал 2",
                 ChartType = SeriesChartType.Spline,
                 BorderWidth = 4,
             };
            chrt.Series seriesThree =
             new chrt.Series()
             {
                 Color = Color.HotPink,
                 Name = "Канал 3",
                 ChartType = SeriesChartType.Spline,
                 BorderWidth = 4,
             };
            chrt.Series seriesFour =
             new chrt.Series()
             {
                 Color = Color.SteelBlue,
                 Name = "Канал 4",
                 ChartType = SeriesChartType.Spline,
                 BorderWidth = 4,
             };
            for (int i = 0; i <= 15; i++)
            {
                //seriesZero.Points.Add(new DataPoint(i, Calculate(0, i)));
                seriesOne.Points.Add(new DataPoint(i, Calculate(1, i)));
                seriesTwo.Points.Add(new DataPoint(i, Calculate(2, i)));
                seriesThree.Points.Add(new DataPoint(i, Calculate(3, i)));
                seriesFour.Points.Add(new DataPoint(i, Calculate(4, i)));

            }
            //chart1.Series.Add(seriesZero);
            chart1.Series.Add(seriesOne);
            chart1.Series.Add(seriesTwo);
            chart1.Series.Add(seriesThree);
            chart1.Series.Add(seriesFour);

            foreach(var item in this.CheckCroosing(V))
            {
                Console.WriteLine($"({item.Values.P0}, {item.Values.PINf}):\ti: {item.I}; v: {item.V}");
            }

            var c = this.CalculateCharacter(2);
          
            listView1.Items.Add("Вероятность потери вызова " + c.Pb.ToString());
            listView1.Items.Add("Вероятность потерь по времени " + c.Pt.ToString());
            listView1.Items.Add("Вероятность потерь по нагрузке " + c.Ph.ToString());
            listView1.Items.Add("Обслуженная нагрузка Y " + c.Y.ToString());
            listView1.Items.Add("Избыточная нагрузка R " + c.R.ToString());
            listView1.Items.Add("Потенциальная нагрузка " + c.A.ToString());

            dataGridFull(dataGridView1, true);

            dataGridFull(dataGridView2, false);
        }
        private void dataGridFull(DataGridView dataGrid, bool flag)
        {
            dataGrid.Rows.Clear();
            dataGrid.Font = new Font("Comic Sans MS", 9);


            dataGrid.AllowUserToResizeColumns = false;
            dataGrid.AllowUserToResizeRows = false;
            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dataGrid.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dataGrid.EnableHeadersVisualStyles = false;
            dataGrid.DefaultCellStyle.SelectionBackColor = Color.LightCoral;
            dataGrid.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGrid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.LightCoral;
            dataGrid.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dataGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dataGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightCoral;

            if (flag)
            {
                var p0_inf = default(double);
                dataGrid.RowCount = V + 1;
                dataGrid.ColumnCount = 11;
                dataGrid.RowHeadersWidth = 70;
                dataGrid.ColumnHeadersHeight = 50;
                dataGrid.Columns[0].HeaderCell.Value = $"i";

                dataGrid.Columns[1].HeaderCell.Value = $"Pi(∞), v=0";
                dataGrid.Columns[2].HeaderCell.Value = $"P0(∞), v=0";
                dataGrid.Columns[3].HeaderCell.Value = $"Pi(∞), v=1";
                dataGrid.Columns[4].HeaderCell.Value = $"P0(∞), v=1";
                dataGrid.Columns[5].HeaderCell.Value = $"Pi(∞), v=2";
                dataGrid.Columns[6].HeaderCell.Value = $"P0(∞), v=2";
                dataGrid.Columns[7].HeaderCell.Value = $"Pi(∞), v=3";
                dataGrid.Columns[8].HeaderCell.Value = $"P0(∞), v=3";
                dataGrid.Columns[9].HeaderCell.Value = $"Pi(∞), v=4";
                dataGrid.Columns[10].HeaderCell.Value = $"P0(∞), v=4";

                for (int i = 0; i < V + 1; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        dataGrid.Rows[i].Cells[0].Value = i;
                        dataGrid.Rows[i].Cells[1].Value = this.CalculatePInfinity(0, i, out p0_inf);
                        dataGrid.Rows[i].Cells[2].Value = p0_inf;
                        dataGrid.Rows[i].Cells[3].Value = this.CalculatePInfinity(1, i, out p0_inf);
                        dataGrid.Rows[i].Cells[4].Value = p0_inf;
                        dataGrid.Rows[i].Cells[5].Value = this.CalculatePInfinity(2, i, out p0_inf);
                        dataGrid.Rows[i].Cells[6].Value = p0_inf;
                        dataGrid.Rows[i].Cells[7].Value = this.CalculatePInfinity(3, i, out p0_inf);
                        dataGrid.Rows[i].Cells[8].Value = p0_inf;
                        dataGrid.Rows[i].Cells[9].Value = this.CalculatePInfinity(4, i, out p0_inf);
                        dataGrid.Rows[i].Cells[10].Value = p0_inf;
                        dataGrid.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                        dataGrid.Columns[j].Width = 160;
                        dataGrid.Rows[i].Height = 30;
                        //dataGrid.Rows[i].HeaderCell.Value = $"{i + 1}";

                    }
                }
                dataGrid.Columns[0].Width = 40;
                dataGrid.Columns[1].Width = 60;
                dataGrid.Columns[2].Width = 60;
            }
            else if (flag == false)
            {
                dataGrid.RowCount = V;
                dataGrid.ColumnCount = 3;
                dataGrid.RowHeadersWidth = 70;
                dataGrid.ColumnHeadersHeight = 60;
                dataGrid.Columns[0].HeaderCell.Value = $"i";

                dataGrid.Columns[1].HeaderCell.Value = $"V";
                dataGrid.Columns[2].HeaderCell.Value = $"P0";
                dataGrid.Columns[2].HeaderCell.Value = $"Точки пересечений, Pi(∞)";

                int a = 0;
                foreach (var item in this.CheckCroosing(V))
                {
                    if (a <= V)
                    {
                        dataGrid.Rows[a].Cells[0].Value = item.I;
                        dataGrid.Rows[a].Cells[1].Value = item.V;
                        dataGrid.Rows[a].Cells[2].Value = $"({item.Values.P0}; {item.Values.PINf})";
                        a++;
                    }
                }
                    for (int i = 0; i < V; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {                
                        
                        dataGrid.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                    
                        dataGrid.Rows[i].Height = 30;
                        //dataGrid.Rows[i].HeaderCell.Value = $"{i + 1}";

                    }
                }
                dataGrid.Columns[0].Width = 40;
                dataGrid.Columns[1].Width = 40;
                dataGrid.Columns[2].Width = 100;
          
            }
        }

    }
}
