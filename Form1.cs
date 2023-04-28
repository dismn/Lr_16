using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lr_16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //кнопка "Обчислення"
        private void button1_Click(object sender, EventArgs e)
        {
            int @base = int.Parse(textBox1.Text); //основа
            string numberString = textBox2.Text; //число, яке потрібно піднести до ступеню
            int exponent = int.Parse(textBox3.Text); //показник ступеню


            //конвертація числа у вигляді списку цифр
            List<int> digits = new List<int>();
            foreach (char c in numberString)
            {
                digits.Add(int.Parse(c.ToString()));
            }

            //Піднесення числа до ступеня та конвертування результату у Р-ічну систему систему числення
            NumberSystem number = new NumberSystem(@base, digits);
            NumberSystem result = number.Power(exponent);


            textBox4.Text = result.ToString();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //Кнопка "Додавання"
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int baseNumber = int.Parse(baseTextBox.Text);
            int number1 = int.Parse(number1TextBox.Text);
            int number2 = int.Parse(number2TextBox.Text);

            NumberSystem a = new NumberSystem(baseNumber, number1);
            NumberSystem b = new NumberSystem(baseNumber, number2);

            NumberSystem result = NumberSystem.Add(a, b);

            lblResult.Text = result.ToString();

        }

        //Кнопка "Віднімання"
        private void Sub_Click(object sender, EventArgs e)
        {
            int baseNumber = int.Parse(baseTextBox.Text);
            int number1 = int.Parse(number1TextBox.Text);
            int number2 = int.Parse(number2TextBox.Text);

            NumberSystem ns1 = new NumberSystem(baseNumber, number1);
            NumberSystem ns2 = new NumberSystem(baseNumber, number2);

            NumberSystem difference = ns1.Subtract(ns2);


            lblResult.Text = difference.ToString();
        }

        //Кнопка "Множення"
        private void multiply_Click(object sender, EventArgs e)
        {
            int baseNumber = int.Parse(baseTextBox.Text);
            int num1 = int.Parse(number1TextBox.Text);
            int num2 = int.Parse(number2TextBox.Text);

            NumberSystem ns1 = new NumberSystem(baseNumber, num1);
            NumberSystem ns2 = new NumberSystem(baseNumber, num2);

            NumberSystem result = ns1.Multiply(ns2);

            lblResult.Text = result.ToString();

        }

        //Кнопка "Ділення"
        private void button2_Click(object sender, EventArgs e)
        {
            int baseNumber = int.Parse(baseTextBox.Text);
            int num_1 = int.Parse(number1TextBox.Text);
            int num_2 = int.Parse(number2TextBox.Text);

            NumberSystem dividend = new NumberSystem(baseNumber, num_1);
            NumberSystem divisor = new NumberSystem(baseNumber, num_2);

            NumberSystem quotient = NumberSystem.Divide(dividend, divisor);
            lblResult.Text = quotient.ToString();
        }
           
        private void Check_Click(object sender, EventArgs e)
        {
            

            


        }
    }
}
