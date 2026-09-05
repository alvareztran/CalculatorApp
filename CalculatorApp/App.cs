using CalculatorApp.Calculator;
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
        bool angleMode;
        bool InvMode;

        private ExpressionParser parser;

        public App()
        {
            InitializeComponent();

            angleMode = true;
            InvMode = true;

            parser = new ExpressionParser(angleMode);

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
                double res = parser.Evaluate(screenBox.Text);
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

        
        


        /* OTHER BUTTONs CLICKED */
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
            angleMode = !angleMode;
            if (angleMode)
            {
                degBtn.Text = "Rad";
            }
            else
            {
                degBtn.Text = "Deg";
            }
            parser.angleMode = angleMode;
        }

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

        private void Function_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            screenBox.Text += btn.Text + "(";
        }

        private void factBtn_Click(object sender, EventArgs e)
        {
            screenBox.Text += "factorial(";
        }

        private void sqrtBtn_Click(object sender, EventArgs e)
        {
            screenBox.Text += "sqrt(";
        }
    } 
}
