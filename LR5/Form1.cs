using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random RND = new Random();

        //N=2
        //lambda = 10*(N+1)/(N+4);
        double Lambda;
        double lambda = 10 * 3 / 6.0;
        //T1 = N+1;
        //T2=N+4;
 static   int T1 = 3;
    static  int T2 = 6;
        public double[] TK;
        double[] Zi;
        double[] Ri;
        public void Table1(int N, DataGridView dataGrid)
        {
            dataGrid.Rows.Clear();
            dataGrid.Font = new Font("Comic Sans MS", 9);
            dataGrid.RowCount = N;
            dataGrid.ColumnCount = 3;
            dataGrid.RowHeadersWidth = 70;
            dataGrid.ColumnHeadersHeight = 50;

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

            dataGrid.Columns[0].HeaderCell.Value = $"Ri";

            dataGrid.Columns[1].HeaderCell.Value = $"Zi";
            dataGrid.Columns[2].HeaderCell.Value = $"Tk";
           Ri = new double[N];
            for (int i = 0; i  < Ri.Length; i++)
            {
               Ri[i]= RND.NextDouble();
            }
           Zi  = new double[N];

            for (int i = 0; i < Zi.Length; i++)
            {
                Zi[i] =( -1.0 / lambda) * Math.Log(Ri[i]);
            }

            var Tk = new List<double>();
            for(; (Tk.Count == 0) ? true : Tk[Tk.Count - 1] <= T2;)
            {
                Tk.Add(T1);
                
                for(int i = 0; i < Tk.Count && i < Zi.Length; i++) Tk[Tk.Count - 1] += Zi[i];
        
            }       

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <3; j++)
                {
                    dataGrid.Rows[i].Cells[0].Value = Ri[i];
                    dataGrid.Rows[i].Cells[1].Value = Zi[i];
   
                    dataGrid.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                    dataGrid.Columns[j].Width = 160;
                    dataGrid.Rows[i].Height = 30;
                    dataGrid.Rows[i].HeaderCell.Value = $"{i+1}";

                }
            }
           for(int i = 0; i<Tk.Count; i++)
            {
                dataGrid.Rows[i].Cells[2].Value = Tk[i];
            }
            TK = Tk.ToArray();
            Lambda = lambda;
        }


        double tau = (T2 -T1) / 24.0;
        public void Table23(double[] Tk, DataGridView dataGrid, DataGridView dataGrid1)
        {
            dataGrid.Rows.Clear();
            dataGrid.Font = new Font("Comic Sans MS", 9);
            dataGrid.RowCount = 2;
            dataGrid.ColumnCount = 24;
            dataGrid.RowHeadersWidth = 150;
            dataGrid.ColumnHeadersHeight = 30;

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

            dataGrid.Rows[0].HeaderCell.Value = $"N интервала";
            dataGrid.Rows[1].HeaderCell.Value = $"x(t)";


            dataGrid1.Rows.Clear();
            dataGrid1.Font = new Font("Comic Sans MS", 9);
            dataGrid1.RowCount = 2;
            dataGrid1.ColumnCount = 24;
            dataGrid1.RowHeadersWidth = 150;
            dataGrid1.ColumnHeadersHeight = 30;

            dataGrid1.AllowUserToResizeColumns = false;
            dataGrid1.AllowUserToResizeRows = false;
            dataGrid1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGrid1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dataGrid1.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dataGrid1.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dataGrid1.EnableHeadersVisualStyles = false;
            dataGrid1.DefaultCellStyle.SelectionBackColor = Color.LightCoral;
            dataGrid1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGrid1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.LightCoral;
            dataGrid1.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dataGrid1.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dataGrid1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightCoral;

            dataGrid1.Rows[0].HeaderCell.Value = $"x(t)";
            dataGrid1.Rows[1].HeaderCell.Value = $"Nk";
            List<double> result1 = new List<double>();
            List<double> result2 = new List<double>();
            double result3 = default;
            for (double i = T1; i < T2; i += tau)
            {
                var count = default(int);
                for (int o =0;o< Tk.Length; o++) { if (Tk[o] < i) count++; }
                result1.Add(count);
            }
            for (int index = 0; index < 24; index++)
            {
                var count = default(int);
                foreach (var item in result1) { if (item == index) count++; }
                result2.Add(count);

                result3 += result1[index] * result2[index];
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 23; j++)
                {
                    dataGrid.Rows[0].Cells[j].Value = j + 1;
                    dataGrid.Rows[1].Cells[j].Value = result2.ElementAt(j);

                    dataGrid.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                    dataGrid.Columns[j].Width = 50;
                    dataGrid.Rows[0].Height = 30;
                    dataGrid.Rows[1].Height = 30;

                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 23; j++)
                {
                    dataGrid1.Rows[0].Cells[j].Value = j+1;
                    dataGrid1.Rows[1].Cells[j].Value = result1.ElementAt(j);

                    dataGrid1.Rows[i].Cells[j].Style.BackColor = SystemColors.Control;
                    dataGrid1.Columns[j].Width = 50;
                    dataGrid1.Rows[0].Height = 30;
                    dataGrid1.Rows[1].Height = 30;
        
                }
            }
            Lambda = result3 / (T2 - T1);
        }//xd
        private double Factorial(double x)
        {
            if (x == 1) return 1;
            return this.Factorial(x - 1) * x;
        }
       
        private void Calculate_lambda()
        {
            int t = T2 - T1;
            double P0 = Math.Round(Math.Pow(Math.E, -lambda * t), 10, MidpointRounding.AwayFromZero);
            label5.Text = string.Format("{0:F10}", P0);
            double P1 = Math.Round(Math.Pow(Math.E, -lambda * t) *lambda * t, 10, MidpointRounding.AwayFromZero);
            label11.Text = string.Format("{0:F10}", P1);


            double P4 = Math.Round(Math.Pow(Math.E, -lambda) * Math.Pow(lambda, 4) / this.Factorial(4), 10, MidpointRounding.AwayFromZero);
            label13.Text = string.Format("{0:F10}", P4);
            double P2 = Math.Round(Math.Pow(Math.E, -lambda) * Math.Pow(lambda, 2) / this.Factorial(2), 10, MidpointRounding.AwayFromZero);
           double P3 = Math.Round(Math.Pow(Math.E, -lambda) * Math.Pow(lambda, 3) / this.Factorial(3), 10, MidpointRounding.AwayFromZero);
            double sum = P0 + P1 + P2 + P3 + P4;
            double Pmore5 = 1 - sum;
            label16.Text = string.Format("{0:F10}", Pmore5);

            double sum1 = P0 + P1 + P2;
            label19.Text = string.Format("{0:F10}", sum1);
            double P5 = Math.Round(Math.Pow(Math.E, -lambda) * Math.Pow(lambda, 5) / this.Factorial(5), 10, MidpointRounding.AwayFromZero);
            double P6 =  Math.Round(Math.Pow(Math.E, -lambda) * Math.Pow(lambda, 6) / this.Factorial(6), 10, MidpointRounding.AwayFromZero);
            double P7 = Math.Round(Math.Pow(Math.E, -lambda) * Math.Pow(lambda, 7) / this.Factorial(7), 10, MidpointRounding.AwayFromZero);
            sum += P5 + P6 + P7;
            label22.Text = string.Format("{0:F10}", sum);
            label25.Text = string.Format("{0:F10}", F());

        }
        double F()
        {
            var x = new List<double>() ;
            for(int i =0; i<Zi.Length; i++) {
                if (Zi[i] >=0.1 && Zi[i] <=0.5) x.Add(Zi[i]);
            }
            return (double)(x.Count)/(double)(Zi.Length);
        }
        //xdxdxdxdxdxd

        private void Calculate_Lambda()
        {
            int t = T2 - T1;
            double P0 = Math.Round(Math.Pow(Math.E, -Lambda * t), 15, MidpointRounding.AwayFromZero);
            label28.Text = string.Format("{0:F15}", P0);
            double P1 = Math.Round(Math.Pow(Math.E, -Lambda * t) * Lambda * t, 15, MidpointRounding.AwayFromZero);
            label31.Text = string.Format("{0:F15}", P1);


            double P4 = Math.Round(Math.Pow(Math.E, -Lambda) * Math.Pow(Lambda, 4) / this.Factorial(4), 10, MidpointRounding.AwayFromZero); ;
            label30.Text = string.Format("{0:F10}", P4);
            double P2 = Math.Round(Math.Pow(Math.E, -Lambda) * Math.Pow(Lambda, 2) / this.Factorial(2), 10, MidpointRounding.AwayFromZero);
            double P3 = Math.Round(Math.Pow(Math.E, -Lambda) * Math.Pow(Lambda, 3) / this.Factorial(3), 10, MidpointRounding.AwayFromZero);
            double sum = P0 + P1 + P2 + P3 + P4;
            double Pmore5 = 1 - sum;
            label39.Text = string.Format("{0:F10}", Pmore5);

            double sum1 = P0 + P1 + P2;
            label33.Text = string.Format("{0:F10}", sum1);
            double P5 = Math.Round(Math.Pow(Math.E, -Lambda) * Math.Pow(Lambda, 5) / this.Factorial(5), 10, MidpointRounding.AwayFromZero);
            double P6 = Math.Round(Math.Pow(Math.E, -Lambda) * Math.Pow(Lambda, 6) / this.Factorial(6), 10, MidpointRounding.AwayFromZero);
            double P7 = Math.Round(Math.Pow(Math.E, -Lambda) * Math.Pow(Lambda, 7) / this.Factorial(7), 10, MidpointRounding.AwayFromZero);
            sum += P5 + P6 + P7;
            label35.Text = string.Format("{0:F10}", sum);
            label37.Text = string.Format("{0:F10}", F());

        }
        void Calc_ZiLambda(int N)
        {
            Zi = new double[N];

            for (int i = 0; i < Zi.Length; i++)
            {
                Zi[i] = (-1.0 / Lambda) * Math.Log(Ri[i]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Table1((int)numericUpDown1.Value, dataGridView1);
            Table23(TK, dataGridView2,dataGridView3);
            label2.Text = string.Format("{0:F6}", Lambda);
            Calculate_lambda();
            Calc_ZiLambda((int)numericUpDown1.Value);
            Calculate_Lambda();
        }

      
    }
}
