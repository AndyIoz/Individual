using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Individual
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<PointD> Points { get; set; }
        public List<PointD> PointsNeuman { get; set; }

        public static double Step { get; set; }
        public static int Count { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Points = new List<PointD>();
            //PointsNeuman = new List<PointD>();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                dg1.Rows.Clear();
                dg2.Rows.Clear();
                Count = int.Parse(toolStripTextBox1.Text)-1;
                Step = 1 / (double)Count;

                Points = new List<PointD>();
                PointsNeuman = new List<PointD>();

                // Clear Series
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();

                //Generating Points
                GaussDistr(Points);
                SelectNeuman(PointsNeuman, Points);

                foreach(PointD p in Points)
                {
                    dg1.Rows.Add(p.Y.ToString());
                    chart1.Series[0].Points.AddXY(p.X, p.Y);
                }

                foreach(PointD p in PointsNeuman)
                {
                    dg2.Rows.Add(p.X.ToString(), p.Y.ToString());
                    chart1.Series[1].Points.AddXY(p.X, p.Y);
                }
                pirson.Text = String.Format("{0:N4}", Pirson());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SelectNeuman(List<PointD> output, List<PointD> gauss)
        {
            Random r = new Random();
            /*
                double x = r.NextDouble();
                double y = r.NextDouble();
             */
            for(int i = 0; i < Count; i++)
            {
                while (true)
                {
                    double x = r.NextDouble();
                    double y = r.NextDouble();
                    if (x >= gauss[i].X && x < gauss[i + 1].X)
                    {
                        if(y<gauss[i].Y && (y<gauss[i+1].Y || y > gauss[i + 1].Y))
                        {
                            output.AddXY(x, y);
                            break;
                        }
                    }
                }
            }
            //gauss.RemoveAt(Count);
        }
        public void GaussDistr(List<PointD> list)
        {
            //Random r = new Random();
            for(double i = 0; i <= 1; i+=Step)
            {
                //double x = r.NextDouble();
                list.AddXY(i, Gauss(i));
            }
        }
        private static double Gauss(double x)
        {
            return Math.Exp(-(x * x) / 2) / Math.Sqrt(2 * Math.PI);
        }

        private double Pirson()
        {
            double s1=0, s=0;
            for (int i = 0; i < Count; i++)
            {
                s1 += Math.Pow(Points[i].Y - PointsNeuman[i].Y, 2);
                s += Points[i].Y;
            }
            return s1 / s;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Author author = new Author();
            author.ShowDialog();
        }
    }
}
