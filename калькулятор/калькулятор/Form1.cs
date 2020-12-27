using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace калькулятор
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void left_Click(object sender, EventArgs e)
        {
            textBox1.Text += "(";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void right_Click(object sender, EventArgs e)
        {
            textBox1.Text += ")";
        }

        private void plus_Click(object sender, EventArgs e)
        {
            textBox1.Text += "+";
        }

        private void minus_Click(object sender, EventArgs e)
        {
            textBox1.Text += "-";
        }

        private void mult_Click(object sender, EventArgs e)
        {
            textBox1.Text += "*";
        }

        private void divided_Click(object sender, EventArgs e)
        {
            textBox1.Text += "/";
        }

        private void po_Click(object sender, EventArgs e)
        {
            textBox1.Text += "^";
        }

        private void del_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);

        }

        private void calc_Click(object sender, EventArgs e)
        {
            string formula = textBox1.Text;
            textBox1.Clear();
            textBox1.Text = System.Convert.ToString(calculus(formula));
        }
        private bool operation(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
        }
        private int prioritet(char op)
        {
            if (op < 0) return 3;
            else
            {
                if (op == '+' || op == '-') return 1;
                else if (op == '*' || op == '/') return 2;
                else if (op == '^') return 4;
                else return -1;
            }
        }
        private void action(List<double> value, char op)
        {
            if (op < 0)
            {                            //для унарных операций
                double unitar = value.Last();
                value.RemoveAt(value.Count - 1);
                if (-op == '-') value.Add(-unitar);
            }
            else
            {
                //для бинарных операций
                    double right = value.Last();
                    value.RemoveAt(value.Count - 1);
                   
                        double left = value.Last();
                        value.RemoveAt(value.Count - 1);
                        if (op == '+') value.Add(left + right);
                        else if (op == '-') value.Add(left - right);
                        else if (op == '*') value.Add(left * right);
                        else if (op == '/') value.Add(left / right);
                        else if (op == '^')
                        {
                            value.Add(Math.Pow(left, right));
                        }

                
            }
        }
        private double calculus(string formula)
        {
            bool unary = true;        //создадим булевскую переменную, для распределения операторов на унарные и бинарные
            List<double> value = new List<double>();        //заведем массив для целых чисел
            List<char> op = new List<char>();           //и соответственно для самых операторов
            for (int i = 0; i < formula.Length; i++)
            {
                if (formula[i] == '(')
                {    //если текущий элемент — открывающая скобка, то положим её в стек
                    op.Add('(');
                    unary = true;
                }
                else if (formula[i] == ')')
                {
                    while (op.Last() != '(')
                    {  //если закрывающая скобка - выполняем все операции, находящиеся внутри этой скобки
                        action(value, op.Last()) ;
                        if (op.Count > 1)
                        {
                            op.RemoveAt(value.Count - 1);
                        }
                    }
                    op.RemoveAt(value.Count - 1);
                    unary = false;
                }
                else if (operation(formula[i]))
                { //если данный элемент строки является одни из выше перечисленных операндов,то
                    char zn = formula[i];
                    if (unary == true) {
                        double a = System.Convert.ToDouble(zn);
                        a *= -1;
                        zn = System.Convert.ToChar(a);
                         } //придает отрицательное значение, для распознавания функции унарности оператора 
                    while (op.Count()!=0 && prioritet(op.Last()) >= prioritet(zn))
                    {
                        action(value, op.Last());   //выполняем сами алгебраические вычисления, где все уже операции упорядочены  
                        op.RemoveAt(value.Count - 1);              //в одном из стеков по строгому убыванию приоритета, если двигаться от вершины
                    }
                    op.Add(zn);
                    unary = true;
                }
                else
                {
                    string number="";      //заведем строку для найденных числовых операндов
                    while (i < formula.Length && Char.IsDigit(formula[i])) number += formula[i++];//распознаем их с помощью библиотечной функции строк
                    i--;
                    value.Add(System.Convert.ToDouble(number));//поместим в наш стек с числовыми выражениями
                    unary = false;
                }
            }
            while (op.Count()!=0)
            {     //выполним еще не использованные операции в стеке 
                action(value, op.Last());
                op.RemoveAt(value.Count - 1);
            }
            return value.Last(); //получим на выходе значение выражения
        }
    }
}
