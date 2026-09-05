using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class App : Form
    {
        bool degMode;
        bool InvMode;
        public App()
        {
            InitializeComponent();

            degMode = true;
            InvMode = true;

            screenBox.Text = "";
        }

        /* BUTTONs HOVERED */
        private void Button_Enter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            btn.Tag = btn.BackColor;
            btn.BackColor = Color.LimeGreen;
        }

        private void Button_Leave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Tag is Color originalColor)
            {
                btn.BackColor = originalColor;
            }
        }


        /* NUMBER BUTTONs CLICKED */
        private void Number_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            screenBox.Text += btn.Text;
        }

        private void screenBox_Change(object sender, EventArgs e)
        {
            resultBox.Text = "";
        }


        /* OPERATOR BUTTONs CLICKED */
        private void Operator_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            screenBox.Text += btn.Text;
        }


        /* DOT BUTTON CLICKED */
        private void dotBtn_Click(object sender, EventArgs e)
        {
            screenBox.Text += ".";
        }


        /* EQUAL BUTTON CLICKED */
        private void equalBtn_Click(object sender, EventArgs e)
        {
            try
            {
                double res = evaluate(screenBox.Text);
                resultBox.Text = res.ToString();
            }
            catch (DivideByZeroException ex)
            {
                resultBox.Text = ex.Message;
            }
            catch (Exception ex)
            {
                resultBox.Text = ex.Message;
            }
        }


        /* CLEAR | DELETE BUTTON CLICKED */
        private void clearBtn_Click(object sender, EventArgs e)
        {
            screenBox.Clear();
            resultBox.Clear();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (screenBox.Text.Length > 0)
            {
                screenBox.Text = screenBox.Text.Remove(screenBox.Text.Length - 1);
            }
            resultBox.Clear();
        }

        private void openParenthesisBtn_Click(object sender, EventArgs e)
        {
            screenBox.Text += "(";
        }

        private void closeParenthesisBtn_Click(object sender, EventArgs e)
        {
            screenBox.Text += ")";
        }


        private void degBtn_Click(object sender, EventArgs e)
        {
            degMode = !degMode;
            if (degMode)
            {
                degBtn.Text = "Deg";
            }
            else
            {
                degBtn.Text = "Rad";
            }
        }

        /* OTHER BUTTONs CLICKED */
        private void InvBtn_Click(object sender, EventArgs e)
        {
            InvMode = !InvMode;
            if (InvMode)
            {
                lnBtn.Text = "ln";
                logBtn.Text = "log";
                powBtn.Text = "^";
                sinBtn.Text = "sin";
                cosBtn.Text = "cos";
                tanBtn.Text = "tan";
                sqrtBtn.Text = "√";
            }
            else
            {
                lnBtn.Text = "e^";
                logBtn.Text = "10^";
                powBtn.Text = "y√x";
                sinBtn.Text = "sin⁻¹";
                cosBtn.Text = "cos⁻¹";
                tanBtn.Text = "tan⁻¹";
                sqrtBtn.Text = "x²";
            }
        }


        /* SUPPORTED CALCULATION FUNCTIONS */
        private bool isNumChar(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool isOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        private double add(double a, double b)
        {
            return a + b;
        }

        private double subtract(double a, double b)
        {
            return a - b;
        }

        private double multiply(double a, double b)
        {
            return a * b;
        }

        private double divide(double a, double b)
        {
            return a / b;
        }

        private double calculate(double a, double b, char opr)
        {
            double res = 0;

            switch (opr)
            {
                case '+':
                    return add(a, b);
                case '-':
                    return subtract(a, b);
                case '*':
                    return multiply(a, b);
                case '/':
                    if (b == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return divide(a, b);
            }

            return res;
        }

        private double convertToDouble(string st)
        {
            return double.Parse(st);
        }

        private double processFactor(string expression, ref int i)
        {
            if (i >= expression.Length)
            {
                throw new Exception("Invalid expression.");
            }

            if (expression[i] == '-')
            {
                i++;
                return -processFactor(expression, ref i);
            }

            if (expression[i] == '(')
            {
                i++;
                double res = processExpression(expression, ref i);

                if (i >= expression.Length || expression[i] != ')')
                {
                    throw new Exception("Invalid expression.");
                }

                i++;
                return res;
            }

            if (isNumChar(expression[i]))
            {
                int start = i;
                bool hasDot = false;

                while (i < expression.Length
                    && (isNumChar(expression[i]) || expression[i] == '.'))
                {
                    if (expression[i] == '.')
                    {
                        if (hasDot)
                        {
                            throw new Exception("Invalid expression.");
                        }
                        else
                        {
                            hasDot = true;
                        }
                    }
                    i++;
                }

                return convertToDouble(expression.Substring(start, i - start));
            }
            throw new Exception("Invalid expression.");
        }

        private double processTerm(string expression, ref int i)
        {
            double res = processFactor(expression, ref i);

            while (i < expression.Length && expression[i] != ')')
            {
                char opr = expression[i];

                if (opr != '*' && opr != '/')
                {
                    break;
                }

                i++;

                double next = processFactor(expression, ref i);

                res = calculate(res, next, opr);
            }

            return res;
        }

        private double processExpression(string expression, ref int i)
        {
            double res = processTerm(expression, ref i);

            while (i < expression.Length && expression[i] != ')')
            {
                char opr = expression[i];

                if (opr != '+' && opr != '-')
                {
                    throw new Exception("Invalid expression.");
                }

                i++;

                double next = processTerm(expression, ref i);

                res = calculate(res, next, opr);
            }

            return res;
        }

        private double evaluate(string expression)
        {

            if (expression.Length == 0)
            {
                throw new Exception("Invalid expression.");
            }

            int i = 0;
            double res = processExpression(expression, ref i);

            if (i != expression.Length)
            {
                throw new Exception("Invalid expression.");
            }


            return res;
        }

        
    } 
}
