using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DM4
{
    public class Graph
    {
        int[,] contigutiMatrix;
        public Point[] points;
        public bool isOrgGraph { get; private set; }
        public int Count { get; private set; }
        bool[] used;
        List<Stack<int>> ways = new List<Stack<int>>();
        Stack<int> way = new Stack<int>();
        public Graph(int[,] contigutiMatrix, bool IsOrg)
        {
            this.contigutiMatrix = contigutiMatrix;
            Count = contigutiMatrix.GetLength(0);
            points = new Point[Count];
            isOrgGraph = IsOrg;
            used = new bool[contigutiMatrix.GetLength(0)];
        }
        void DrawPoints(Graphics g)
        {

            double angle = 360 / points.Length;
            double z = 0;
            int i = 0;
            while (i < points.Length)
            {
                points[i] = new Point(0, 0);
                points[i].X = 500 + (int)(Math.Round(Math.Cos(z / 180 * Math.PI) * 200));
                points[i].Y = 500 - (int)(Math.Round(Math.Sin(z / 180 * Math.PI) * 200));

                //g.FillEllipse(new SolidBrush(Color.Blue), points[i].X, points[i].Y, 15, 15);

                z = z + angle;
                i++;
            }

        }
        public void Draw(Graphics g)
        {
            
            DrawPoints(g);
            for (int i = 0; i < contigutiMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < contigutiMatrix.GetLength(1); j++)
                {

                    if (contigutiMatrix[i, j] == 1)
                    {

                        Pen pen = new Pen(Brushes.Green, 3f);

                        if (isOrgGraph)
                            pen.CustomEndCap = new AdjustableArrowCap(3, 6);
                        g.DrawLine(pen, points[i], points[j]);
                        if (j == i)
                        {
                            g.DrawArc(pen, points[i].X - 20, points[i].Y - 20, 40, 40, 0, 360);

                        }
                    }


                }
                g.DrawString(Convert.ToChar(65 + i).ToString(), new Font("", 16), new SolidBrush(Color.Black), points[i]);
            }

        }
        public void DrowGamiltonCycle(Graphics g, int number)
        {
            List<int> list;
            if (ways.Count > 0)
            {
                list = ways[number].ToList();
                for (int i = 0; i < list.Count - 1; i++)
                {
                    int j = i + 1;
                    if (i+1 == contigutiMatrix.GetLength(0))
                        j = 0;
                    Console.WriteLine(" I1 " + i + " J1 " + j);
                    Pen pen = new Pen(Brushes.Red, 3f);
                    if (isOrgGraph)
                        pen.CustomEndCap = new AdjustableArrowCap(3, 6);
                    Console.WriteLine(" I " + i + " J " + j);
                    g.DrawLine(pen, points[list[j]], points[list[i]]);
                }
            }
               
        } 
        public List<Stack<int>> GamiltonCicles(int vertex)
        {
            for (int i = 0; i < used.Length; i++)
            {
                used[i] = false;
            }
            way.Clear();
            ways.Clear();
            used[vertex] = true;
            way.Push(vertex);
            Dfs(vertex, vertex);

           
            /*for (int i = 0; i < ways.Count; i++)
            {
                if (ways[i].Count > 0)
                {
                    while (ways[i].Count > 0)
                    {
                        Console.Write(ways[i].Pop() + " ");
                    }
                    Console.WriteLine();
                }

            }*/
            return ways;
        }
        void Dfs(int v, int st)
        {
            int index = -1;
            for (int j = 0; j < contigutiMatrix.GetLength(0); j++)
            {
                if (contigutiMatrix[v, j] == 1 && used[j] == false)
                {
                    used[j] = true;
                    Console.WriteLine(j);
                    way.Push(j);
                    index = j;
                    Dfs(j, st);
                    if (index >= 0)
                    {
                        used[index] = false;
                        Console.WriteLine("way " + way.Peek());
                        way.Pop();
                    }

                }

            }

            if (way.Count == contigutiMatrix.GetLength(0))
            {
                Console.WriteLine(v + " | " + st);
                if (contigutiMatrix[v, st] == 1)
                {
                    Console.WriteLine("end");
                    var clone = new Stack<int>(way.Reverse());
                    clone.Push(st);
                    Console.WriteLine();
                    ways.Add(clone);
                }

            }

        }
    }
    
}
