using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Individual
{
    public static class Point
    {
        public static List<PointD> SortPoint(this List<PointD> list)
        {
            if (list.Count == 1 || list.Count == 0)
                return list;
            else
                return list.QuickSort(0, list.Count - 1);
        }

        public static double MaxY(this List<PointD> list)
        {
            double max = 0;
            foreach(PointD p in list)
            {
                if (p.Y > max)
                    max = p.Y;
            }
            return max;
        }

        public static double MinY(this List<PointD> list)
        {
            double min = 1;
            foreach (PointD p in list)
            {
                if (p.Y < min)
                    min = p.Y;
            }
            return min;
        }

        public static List<PointD> NeumanMeth(this List<PointD> list, List<PointD> points)
        {
            Random r = new Random();
            int count = Form1.Count;
            //list.Clear();
            for(int i = 0; i < count-1; i++)
            {
                bool fl = true;
                while (fl)
                {
                    double x = r.NextDouble();
                    double y = r.NextDouble();
                    if((x>=points[i].X && x <= points[i + 1].X) && (y < points[i].Y && y < points[i + 1].Y))
                    {
                        list.AddXY(x, y);
                        fl = false;
                    }
                }
            }
            return list;
        }

        public static void AddXY(this List<PointD> list, double x, double y)
        {
            list.Add(new PointD { X = x, Y = y });
        }

        public static List<PointD> NormRaspr(this List<PointD> list, int count)
        {
            Random r = new Random();
            for(int i = 0; i < count; i++)
            {
                double x = r.NextDouble();
                //list.AddXY(x, Gauss(x));
            }
            return list;
        }

        private static List<PointD> QuickSort(this List<PointD> list, int i, int j)
        {
            if (i < j)
            {
                int q = list.Partition(i, j);
                list.QuickSort(i, q);
                list.QuickSort(q + 1, j);
            }
            return list;
        }
        private static int Partition(this List<PointD> a, int p, int r)
        {
            double x = a[p].X;
            int i = p - 1;
            int j = r + 1;
            while (true)
            {
                do
                {
                    j--;
                }
                while (a[j].X > x);
                do
                {
                    i++;
                }
                while (a[i].X < x);
                if (i < j)
                {
                    var tmp = a[i];
                    a[i] = a[j];
                    a[j] = tmp;
                }
                else
                {
                    return j;
                }
            }
        }

        
    }

    public struct PointD
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
