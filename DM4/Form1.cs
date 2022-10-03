using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DM4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TextBox[,] tex;
        int[,] contigutiMatrix;
        Label[,] box;
        Graph graph = new Graph(new int[1,1], false);
        int n;
		

		private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = true;
            for (int i = 0; i < n; i++)
            {
                this.Controls.Remove(box[i, 0]);
                this.Controls.Remove(box[i, 1]);
                for (int j = 0; j < n; j++)
                {
                    this.Controls.Remove(tex[i, j]);

                }
            }
            n = Convert.ToInt32(textBox1.Text);
            tex = new TextBox[n, n];
            contigutiMatrix = new int[n, n];
            box = new Label[n, 2];
            int top = 5; //y
            int left = 450;//x
            for (int i = 0; i < n; i++)
            {
                box[i, 0] = new Label();
                box[i, 0].Left = left;
                box[i, 0].Top = top;
                box[i, 0].Font = new Font("", 16);
                box[i, 0].Size = new Size(30, 30);
                box[i, 0].Text = Convert.ToChar(65 + i).ToString();
                this.Controls.Add(box[i, 0]);

                left += 50 + 20;
                top = 5;
            }
            top = 40; //y
            left = 400;//x
            for (int i = 0; i < n; i++)
            {
                box[i, 1] = new Label();
                box[i, 1].Left = left;
                box[i, 1].Top = top;
                box[i, 1].Font = new Font("", 16);
                box[i, 1].Size = new Size(30, 30);
                box[i, 1].Text = Convert.ToChar(65 + i).ToString();
                this.Controls.Add(box[i, 1]);

                top += 40;
                left = 400;
            }
            top = 40; //y
            left = 450;//x
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    tex[i, j] = new TextBox();
                    tex[i, j].Left = left;
                    tex[i, j].Top = top;
                    //tex[i, j].Name = "tex" + i + j;
                    tex[i, j].Multiline = true;
                    tex[i, j].Font = new Font("", 16);
                    tex[i, j].Size = new Size(30, 30);
                    tex[i, j].Text = 0.ToString();
                    contigutiMatrix[i, j] = 0;
                    this.Controls.Add(tex[i, j]);
                    left += tex[i, j].Height + 40;
                }
                left = 450;
                top += 40;
            }
            label1.Text = "Гамильтоновы циклы: ";
        }

        private void button2_Click(object sender, EventArgs e)
        {       
            for (int i = 0; i < tex.GetLength(0); i++)
            {
                for (int j = 0; j < tex.GetLength(1); j++)
                {

                    if (tex[i, j].Text != "0")
                    {
                        tex[i, j].Text = "1";
                        contigutiMatrix[i, j] = 1;
                        if (radioButton1.Checked)
                        {
                            tex[j, i].Text = "1";
                            contigutiMatrix[j, i] = 1;
                        }
                    }
                    else
                        contigutiMatrix[i, j] = 0;
                }

            }
            button4.Enabled = true;
            label1.Text = "Гамильтоновы циклы: ";
            graph = new Graph(contigutiMatrix, radioButton2.Checked);
            Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "Гамильтоновы циклы: ";
            try
            {
                List<Stack<int>> ways = graph.GamiltonCicles(Convert.ToInt32(Convert.ToChar(textBox2.Text) - 65));
                Refresh();
                if (ways.Count > 0)
                {
                    for (int i = 0; i < ways.Count; i++)
                    {
                        var list = ways[i].ToList();
                        if (list.Count > 0)
                        {
                            label1.Text += "\r\n";
                            int j = list.Count - 1;
                            label1.Text += (i + 1) + ") ";
                            while (j >= 0)
                            {

                                label1.Text += Convert.ToChar(65 + list[j]);
                                j -= 1;
                            }
                            label1.Text += "\r\n";
                        }

                    }
                }
                else
                {
                    label1.Text = "Нет гамильтоновых циклов";
                }
               
            }
            catch
            {
                MessageBox.Show("Некорректый ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int k = n * 2;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    contigutiMatrix[i, j] = 0;
                }
            }
            if (radioButton2.Checked)
            {

                while (k > 0)
                {
                    contigutiMatrix[r.Next(0, n), r.Next(0, n)] = 1;
                    k--;
                }
                graph = new Graph(contigutiMatrix, true);
            }
            k = n;
            if (radioButton1.Checked)
            {

                while (k > 0)
                {
                    int i = r.Next(0, n), j = r.Next(0, n);
                    if (i != j && Convert.ToInt32(tex[i, j].Text) != 1)
                    {
                        contigutiMatrix[i, j] = 1;
                        contigutiMatrix[j, i] = 1;
                        k--;
                    }

                }
                graph = new Graph(contigutiMatrix, false);
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    tex[i, j].Text = contigutiMatrix[i, j].ToString();

                }
            }
            button4.Enabled = true;
            label1.Text = "Гамильтоновы циклы: ";
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graph.Draw(e.Graphics);
            try
            {
                graph.DrowGamiltonCycle(e.Graphics, Convert.ToInt32(textBox3.Text) - 1);
            }
            catch
            {
                graph.DrowGamiltonCycle(e.Graphics, 0);
            }
        }
    }
}
