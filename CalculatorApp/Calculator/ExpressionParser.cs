using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Calculator
{
    public class ExpressionParser
    {

        public bool angleMode { get; set; }

        public ExpressionParser(bool _angleMode)
        {
            angleMode = _angleMode;
        }

        private bool IsNumChar(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        private double ConvertToDouble(string st)
        {
            return double.Parse(st);
        }

        private double Calculate(double a, double b, char opr)
        {
            switch (opr)
            {
                case '+':
                    return MathOperations.Add(a, b);
                case '-':
                    return MathOperations.Subtract(a, b);
                case '*':
                    return MathOperations.Multiply(a, b);
                case '/':
                    return MathOperations.Divide(a, b);
                case '^':
                    return MathOperations.Power(a, b);
                default:
                    throw new Exception("Unknown operator.");
            }
        }

        private double ProcessFactor(string expression, ref int i)
        {
            if (i >= expression.Length)
            {
                throw new Exception("Invalid expression.");
            }

            if (expression[i] == '-')
            {
                i++;
                return -ProcessFactor(expression, ref i);
            }

            if (expression[i] == '(')
            {
                i++;
                double res = ProcessExpression(expression, ref i);

                if (i >= expression.Length || expression[i] != ')')
                {
                    throw new Exception("Invalid expression.");
                }

                i++;

                return res;
            }

            if ((expression[i] >= 'a' && expression[i] <= 'z') 
                || (expression[i] >= 'A' && expression[i] <= 'Z'))
            {
                return ProcessFunction(expression, ref i);
            }

            if (IsNumChar(expression[i]))
            {
                int start = i;
                bool hasDot = false;

                while (i < expression.Length && (IsNumChar(expression[i]) || expression[i] == '.'))
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

                return ConvertToDouble(expression.Substring(start, i - start));
            }
            throw new Exception("Invalid expression.");
        }

        private double ProcessTerm(string expression, ref int i)
        {
            double res = ProcessFactor(expression, ref i);

            while (i < expression.Length && expression[i] != ')')
            {
                char opr = expression[i];

                if (opr != '*' && opr != '/')
                {
                    break;
                }

                i++;
                double next = ProcessFactor(expression, ref i);

                res = Calculate(res, next, opr);
            }

            return res;
        }

        private double ProcessExpression(string expression, ref int i)
        {
            double res = ProcessTerm(expression, ref i);

            while (i < expression.Length && expression[i] != ')')
            {
                char opr = expression[i];
                
                if (opr != '+' && opr != '-')
                {
                    throw new Exception("Invalid expression.");
                }

                i++;
                double next = ProcessTerm(expression, ref i);
                
                res = Calculate(res, next, opr);
            }

            return res;
        }

        private double ProcessFunction(string expression, ref int i)
        {
            int start = i;
            while (i < expression.Length
                && ((expression[i] >= 'a' && expression[i] <= 'z')
                || (expression[i] >= 'A' && expression[i] <= 'Z'))) 
            {
                i++;
            }

            string function = expression.Substring(start, i - start);

            if (i >= expression.Length || expression[i] != '(')
            {
                throw new Exception("Invalid expression.");
            }

            i++;

            double value = ProcessExpression(expression, ref i);

            if (i >= expression.Length || expression[i] != ')')
            {
                throw new Exception("Invalid expression.");
            }

            i++;

            switch (function)
            {
                case "sqrt":
                    return MathOperations.Sqrt(value);

                case "log":
                    return MathOperations.Log(value);

                case "ln":
                    return MathOperations.Ln(value);

                case "sin":
                    if (angleMode == false)
                    {
                        value = value * Math.PI / 180;
                    }
                    return MathOperations.Sin(value);

                case "cos":
                    if (angleMode == false)
                    {
                        value = value * Math.PI / 180;
                    }
                    return MathOperations.Cos(value);

                case "tan":
                    if (angleMode == false)
                    {
                        value = value * Math.PI / 180;
                    }
                    return MathOperations.Tan(value);

                case "factorial":
                    return MathOperations.Factorial(value);

                case "reciprocal":
                    return MathOperations.Reciprocal(value);

                default:
                    throw new Exception("Unknown function.");
            }
        }

        public double Evaluate(string expression)
        {
            if (expression.Length == 0)
            {
                throw new Exception("Invalid expression.");
            }

            int i = 0;
            double res = ProcessExpression(expression, ref i);

            if (i != expression.Length)
            {
                throw new Exception("Invalid expression.");
            }

            return res;
        }

    }
}
