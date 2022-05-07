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
                MessageBox.Show("Ошибка!\nЗадайте значение входных параметров");
                return;
            }
            n = Convert.ToInt32(comboBox1.SelectedItem.ToString());//размерность массива текстбоксов
            m = Convert.ToInt32(comboBox1.SelectedItem.ToString());//размерность массива текстбоксов
            m += 1;//увеличение на один, так как матрица расширенная
            int i, j, c, d;
            int x = 18, y = 25;

            if (t != null)
            {
                panel1.Controls.Clear();
                Controls.Add(button2);
            }
            t = new TextBox[n, m];
            for (i = 0; i < n; i++)//цикл для инициализации элементов массива текстбоксов
            {
                for (j = 0; j < m; j++)
                {
                    t[i, j] = new TextBox();//создание объекта класса текстбокс
                    panel1.Controls.Add(t[i, j]);//добавление текстбокса на панель
                    t[i, j].Location = new Point(x += 50, y);//задаем месторасположение текстбоксу
                    t[i, j].Size = new Size(25, 20);//задаем размер
                    t[i, j].TabStop = false;//а вот это не ебу что
                }
                y += 40;//смещаемся по игреку, чтобы начать отображать текстбоксы во второй строке
                x = 18;//приравниваем икс к начальному значению, чтобы текстбоксы отображались с левого краю формы
            }
            y = 28;
            x = 45;
            l = new Label[n, m];
            for (c = 0; c < n; c++)//то же самое только для лэйблов 
            {
                for (d = 0; d < m - 1; d++)
                {
                    l[c, d] = new Label();//инициализация
                    panel1.Controls.Add(l[c, d]);//добавление на панель
                    l[c, d].Location = new Point(x += 50, y);//задаем локацию
                    l[c, d].Size = new Size(40, 40);//задаем размер
                    int z = d + 1;
                    if (d == m - 2)//если это элемент, идущий перед расширением матрицы, то ставится знак равно
                        l[c, d].Text = "x" + z + " =";
                    else//если нет, то +
                        l[c, d].Text = "x" + z + " +";
                    l[c, d].TabStop = false;// не ебу
                }
                y += 41;//смещаемся по игреку
                x = 45;//задаем начальное значение иксу
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
                //делаем главную диагональ единицами
                v = matr[q, q]; // берем первый элемент на главной диагонали
                richTextBox1.AppendText("\nШаг" + (shag++) + ".Разделим строку " + (q + 1) + " на элемент[" + (q + 1) + "," + (q + 1) +
               "]= " + matr[q, q] + "\n\n");
                for (k = 0; k < stolbec; k++) // делим первую строку на элемент 1.1
                    matr[q, k] /= v;
                for (int z = 0; z < stroka; z++)
                //Записываем результат со 2-го шага
                {
                    for (int x = 0; x < stolbec; x++)
                    {
                        richTextBox1.AppendText((Convert.ToString(matr[z, x])).PadRight(2) + " ");
                    }
                    richTextBox1.AppendText("\n\n");
                }
                for (i = q + 1; i < stroka; i++) //обнуляем числа под единицами главной диагoнали
                {
                    v = matr[i, q]; // берем первый элемент второй строки, под главной диагональю
                    for (k = q; k < stolbec; k++)
                    {
                        matr[i, k] = matr[i, k] - matr[q, k] * v; //Отнимаем от строки решающую строку, помноженную на первый элемент строки с противоположным знаком
                                                                  // от элементов второй строки отнимаем элементы первой строки умноженную на первый элемент второй строки с противоположным знаком

                    }
                    richTextBox1.AppendText("\nШаг" + (shag++) + ".Отнимем от строки " + (i + 1) + " строку " + (q + 1) + " умноженную на число " + v + "\n\n");
                    for (int z = 0; z < stroka; z++)  //Записываем результат

                    {
                        for (int x = 0; x < stolbec; x++)
                        {
                            richTextBox1.AppendText((Convert.ToString(matr[z, x])).PadRight(2) + " ");
                        }
                        richTextBox1.AppendText("\n\n");
                    }
                }
            } // разобрать цикл
            for (q = 0; q < stroka; q++)
            {
                for (i = 0; i < (stroka - 1) - q; i++)
                {
                    v = matr[i, (stolbec - 1) - q - 1];
                    for (k = stolbec - 1 - q - 1; k < stolbec; k++)
                        matr[i, k] = matr[i, k] - matr[(stroka - 1) - q, k] * v; // не уверен, но по сути происходит то же самое, что и на 113 строке
                    richTextBox1.AppendText("\nШаг" + (shag++) + ". Отнимем от строки " + (i + 1) + " строку " + (stroka - q) + ", умноженную на число " + v + "\n\n");
                    for (int z = 0; z < stroka; z++)//Записываем результат

                    {
                        for (int x = 0; x < stolbec; x++)
                        {
                            richTextBox1.AppendText((Convert.ToString(matr[z, x])).PadRight(2) + " ");
                        }
                        richTextBox1.AppendText("\n\n");
                    }
                }
            }
            richTextBox1.AppendText("Результатом, будут оставшийся столбец чисел\n\n");
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
            foreach (TextBox textbox in panel1.Controls.OfType<TextBox>())//проверка на ввод в текстбоксы специальных знаков
            {
                
                if (!Regex.IsMatch(textbox.Text, @"!|@|#|$|%")) //если в текстбоксах попадаются данные знаки, блуевой переменной присваивается значение тру, то есть он нашел символы в каком-то из текстбоксов
                {
                    boolka = true;
                }

            }
            if (!boolka)//если в текстбоксах только цифры, то выполяется следущий код
            {
                if (String.IsNullOrEmpty(comboBox1.Text))//проверка на пустоту комбобокса
                {
                    MessageBox.Show("Один или несколько элементов не были правильно заполнены. Пожалуйста, повторите.", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        shag = 2;
                        int i, j;
                        richTextBox1.Clear();
                        string[,] v = new string[n, m];//Матрица для вывода решения
                        double[,] v1 = new double[n, m];//Матрица для самого решения
                        for (i = 0; i < n; i++)//заполнение матрицы v и v1 значениями из текстбоксов
                            for (j = 0; j < m; j++)
                            {
                                v1[i, j] = Convert.ToDouble(t[i, j].Text);
                                v[i, j] = Convert.ToString(t[i, j].Text);
                            }
                        richTextBox1.AppendText("Задача :\nНайти решение системы уравнений:\n");
                        for (i = 0; i < n; i++)
                        {
                            string s = "";
                            for (j = 0; j < m; j++)
                            {
                                if (t[i, j].Text == "")//проверка на пустоту текстбокса
                                {
                                    MessageBox.Show("Были введенны не все коэффициенты");
                                    return;
                                }
                                if (j == m - 1)//вывод элемента для уравнения, которое находится после равно
                                    s += v[i, j];
                                else//вывод уравнений, сформированных из введенных коэффициентов
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
                            richTextBox1.AppendText(s + "\n");//вывод s, это уравнение
                        }
                        richTextBox1.AppendText("Шаг 1:\nФормирование расширенной матрицы:\n\r");
                        for (i = 0; i < n; i++)//вывод матрицы перед решением
                        {
                            string s = "";
                            for (j = 0; j < m; j++)
                            {
                                if (j == m - 1)
                                    s += " " + (v[i, j]).PadRight(2);//если элемент предпоследний, то ставится 2 пробела, чтобы отделить ресширение матрицы
                                else
                                    s += (v[i, j].PadRight(3)) + " ";
                            }
                            richTextBox1.AppendText(s + "\n");
                        }
                        richTextBox1.AppendText("Стремимся, чтобы каждая строка, естественно кроме первой, начиналась с нулей. Таким образом, число нулей в каждой строке должно быть больше, чем в предыдущей.\n");
                        richTextBox1.AppendText(Gauss(n, m, v1));

                    }
                    catch
                    {
                        MessageBox.Show("Один или несколько элементов правильно не были заполнены. Пожалуйста, повторите.", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
            }
            else
            {
                MessageBox.Show("Были введены символы. Пожалуйста, повторите.", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
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