using System;
using System.IO;
using System.Text.RegularExpressions;

namespace WinFormsApp1123
{
    public partial class Decision : Form
    {
        TextBox[,] t;
        Label[,] l;
        int n;
        int m;
        int shag;
        public Decision()
        {
            InitializeComponent();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("������!\n������� �������� ������� ����������");
                return;
            }
            n = Convert.ToInt32(comboBox1.SelectedItem.ToString());//����������� ������� �����������
            m = Convert.ToInt32(comboBox1.SelectedItem.ToString());//����������� ������� �����������
            m += 1;//���������� �� ����, ��� ��� ������� �����������
            int i, j, c, d;
            int x = 18, y = 25;

            if (t != null)
            {
                panel1.Controls.Clear();
                Controls.Add(button2);
            }
            t = new TextBox[n, m];
            for (i = 0; i < n; i++)//���� ��� ������������� ��������� ������� �����������
            {
                for (j = 0; j < m; j++)
                {
                    t[i, j] = new TextBox();//�������� ������� ������ ���������
                    panel1.Controls.Add(t[i, j]);//���������� ���������� �� ������
                    t[i, j].Location = new Point(x += 50, y);//������ ����������������� ����������
                    t[i, j].Size = new Size(25, 20);//������ ������
                    t[i, j].TabStop = false;//� ��� ��� �� ��� ���
                }
                y += 40;//��������� �� ������, ����� ������ ���������� ���������� �� ������ ������
                x = 18;//������������ ��� � ���������� ��������, ����� ���������� ������������ � ������ ���� �����
            }
            y = 28;
            x = 45;
            l = new Label[n, m];
            for (c = 0; c < n; c++)//�� �� ����� ������ ��� ������� 
            {
                for (d = 0; d < m - 1; d++)
                {
                    l[c, d] = new Label();//�������������
                    panel1.Controls.Add(l[c, d]);//���������� �� ������
                    l[c, d].Location = new Point(x += 50, y);//������ �������
                    l[c, d].Size = new Size(40, 40);//������ ������
                    int z = d + 1;
                    if (d == m - 2)//���� ��� �������, ������ ����� ����������� �������, �� �������� ���� �����
                        l[c, d].Text = "x" + z + " =";
                    else//���� ���, �� +
                        l[c, d].Text = "x" + z + " +";
                    l[c, d].TabStop = false;// �� ���
                }
                y += 41;//��������� �� ������
                x = 45;//������ ��������� �������� ����
            }


        }
        public void global_FormClosed(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public string Gauss(int stroka, int stolbec, double[,] matr)
        {
            int i, k, q;
            double v;
            string answer = "";
            for (q = 0; q < stroka; q++)
            {
                //������ ������� ��������� ���������
                v = matr[q, q]; // ����� ������ ������� �� ������� ���������
                richTextBox1.AppendText("\n���" + (shag++) + ".�������� ������ " + (q + 1) + " �� �������[" + (q + 1) + "," + (q + 1) +
               "]= " + matr[q, q] + "\n\n");
                for (k = 0; k < stolbec; k++) // ����� ������ ������ �� ������� 1.1
                    matr[q, k] /= v;
                for (int z = 0; z < stroka; z++)
                //���������� ��������� �� 2-�� ����
                {
                    for (int x = 0; x < stolbec; x++)
                    {
                        richTextBox1.AppendText((Convert.ToString(matr[z, x])).PadRight(2) + " ");
                    }
                    richTextBox1.AppendText("\n\n");
                }
                for (i = q + 1; i < stroka; i++) //�������� ����� ��� ��������� ������� ����o����
                {
                    v = matr[i, q]; // ����� ������ ������� ������ ������, ��� ������� ����������
                    for (k = q; k < stolbec; k++)
                    {
                        matr[i, k] = matr[i, k] - matr[q, k] * v; //�������� �� ������ �������� ������, ����������� �� ������ ������� ������ � ��������������� ������
                                                                  // �� ��������� ������ ������ �������� �������� ������ ������ ���������� �� ������ ������� ������ ������ � ��������������� ������

                    }
                    richTextBox1.AppendText("\n���" + (shag++) + ".������� �� ������ " + (i + 1) + " ������ " + (q + 1) + " ���������� �� ����� " + v + "\n\n");
                    for (int z = 0; z < stroka; z++)  //���������� ���������

                    {
                        for (int x = 0; x < stolbec; x++)
                        {
                            richTextBox1.AppendText((Convert.ToString(matr[z, x])).PadRight(2) + " ");
                        }
                        richTextBox1.AppendText("\n\n");
                    }
                }
            } // ��������� ����
            for (q = 0; q < stroka; q++)
            {
                for (i = 0; i < (stroka - 1) - q; i++)
                {
                    v = matr[i, (stolbec - 1) - q - 1];
                    for (k = stolbec - 1 - q - 1; k < stolbec; k++)
                        matr[i, k] = matr[i, k] - matr[(stroka - 1) - q, k] * v; // �� ������, �� �� ���� ���������� �� �� �����, ��� � �� 113 ������
                    richTextBox1.AppendText("\n���" + (shag++) + ". ������� �� ������ " + (i + 1) + " ������ " + (stroka - q) + ", ���������� �� ����� " + v + "\n\n");
                    for (int z = 0; z < stroka; z++)//���������� ���������

                    {
                        for (int x = 0; x < stolbec; x++)
                        {
                            richTextBox1.AppendText((Convert.ToString(matr[z, x])).PadRight(2) + " ");
                        }
                        richTextBox1.AppendText("\n\n");
                    }
                }
            }
            richTextBox1.AppendText("�����������, ����� ���������� ������� �����\n\n");
            for (i = 0; i < stroka; i++)
                answer += "x" + (i + 1) + " = " + Math.Round(matr[i, stolbec - 1],2) +
               "\r\n";
            return answer;
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

    
        private void button2_Click_1(object sender, EventArgs e)
        {
            bool boolka = false;
            foreach (TextBox textbox in panel1.Controls.OfType<TextBox>())//�������� �� ���� � ���������� ����������� ������
            {
                
                if (!Regex.IsMatch(textbox.Text, @"!|@|#|$|%")) //���� � ����������� ���������� ������ �����, ������� ���������� ������������� �������� ���, �� ���� �� ����� ������� � �����-�� �� �����������
                {
                    boolka = true;
                }

            }
            if (!boolka)//���� � ����������� ������ �����, �� ���������� �������� ���
            {
                if (String.IsNullOrEmpty(comboBox1.Text))//�������� �� ������� ����������
                {
                    MessageBox.Show("���� ��� ��������� ��������� �� ���� ��������� ���������. ����������, ���������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        shag = 2;
                        int i, j;
                        richTextBox1.Clear();
                        string[,] v = new string[n, m];//������� ��� ������ �������
                        double[,] v1 = new double[n, m];//������� ��� ������ �������
                        for (i = 0; i < n; i++)//���������� ������� v � v1 ���������� �� �����������
                            for (j = 0; j < m; j++)
                            {
                                v1[i, j] = Convert.ToDouble(t[i, j].Text);
                                v[i, j] = Convert.ToString(t[i, j].Text);
                            }
                        richTextBox1.AppendText("������ :\n����� ������� ������� ���������:\n");
                        for (i = 0; i < n; i++)
                        {
                            string s = "";
                            for (j = 0; j < m; j++)
                            {
                                if (t[i, j].Text == "")//�������� �� ������� ����������
                                {
                                    MessageBox.Show("���� �������� �� ��� ������������");
                                    return;
                                }
                                if (j == m - 1)//����� �������� ��� ���������, ������� ��������� ����� �����
                                    s += v[i, j];
                                else//����� ���������, �������������� �� ��������� �������������
                                {
                                    int z = j + 1;
                                    if (v1[i, j] < 0)
                                        s += v[i, j] + " * x" + z + " - ";
                                    if (v1[i, j] == 0)
                                        s += " ";
                                    if (j == m - 2)
                                    {
                                        s += v[i, j] + " * x" + z + " = ";
                                        continue;
                                    }
                                    if (v1[i, j] > 0)
                                        s += v[i, j] + " * x" + z + " + ";
                                }
                            }
                            richTextBox1.AppendText(s + "\n");//����� s, ��� ���������
                        }
                        richTextBox1.AppendText("��� 1:\n������������ ����������� �������:\n\r");
                        for (i = 0; i < n; i++)//����� ������� ����� ��������
                        {
                            string s = "";
                            for (j = 0; j < m; j++)
                            {
                                if (j == m - 1)
                                    s += " " + (v[i, j]).PadRight(2);//���� ������� �������������, �� �������� 2 �������, ����� �������� ���������� �������
                                else
                                    s += (v[i, j].PadRight(3)) + " ";
                            }
                            richTextBox1.AppendText(s + "\n");
                        }
                        richTextBox1.AppendText("���������, ����� ������ ������, ����������� ����� ������, ���������� � �����. ����� �������, ����� ����� � ������ ������ ������ ���� ������, ��� � ����������.\n");
                        richTextBox1.AppendText(Gauss(n, m, v1));

                    }
                    catch
                    {
                        MessageBox.Show("���� ��� ��������� ��������� ��������� �� ���� ���������. ����������, ���������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
            }
            else
            {
                MessageBox.Show("���� ������� �������. ����������, ���������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (TextBox textbox in panel1.Controls.OfType<TextBox>())
            {

                if (Regex.IsMatch(textbox.Text, @"!|@|#|$|%"))
                {
                    textbox.TextChanged += (obj, sender) =>
                    {
                        MessageBox.Show("1");
                    };
                }

            }
            foreach (TextBox textbox in panel1.Controls.OfType<TextBox>())
            {

                if (Regex.IsMatch(textbox.Text, @"!|@|#|$|%"))
                {
                    MessageBox.Show("1");
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (TextBox textbox in panel1.Controls.OfType<TextBox>())
            {

                textbox.Text = "";

            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}