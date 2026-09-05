using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Calculator
{
    public static class MathOperations
    {
        /* ANGLE CONVERSION */
        private static double convertToRadian(double angle)
        {
            return angle * Math.PI / 180;
        }

        private static double convertToDeg(double angle)
        {
            return angle * 180 / Math.PI;
        }

        /* BASIC OPERATIONS */
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return a / b;
        }

        public static double Power(double a, double b)
        {
            return Math.Pow(a, b);
        }

        public static double Sqrt(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Cannot calculate square root of a negative number.");
            }
            return Math.Sqrt(value);
        }

        public static double Reciprocal(double value)
        {
            if (value == 0)
            {
                throw new DivideByZeroException("Cannot calculate deciprocal for zero.");
            }
            return 1 / value;
        }

        public static double Log(double value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Invalid value.");
            }
            return Math.Log10(value);
        }

        public static double Ln(double value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Invalid value.");
            }
            return Math.Log(value);                             
        }

        public static double Sin(double angle)
        {
            return Math.Sin(angle);
        }

        public static double Cos(double angle)
        {
            return Math.Cos(angle);
        }

        public static double Tan(double angle)
        {
            return Math.Tan(angle);
        }

        public static double Factorial(double value)
        {
            if (value < 0 || value != Math.Floor(value))
            {
                throw new ArgumentException("Invalid value.");
            }
            double res = 1;
            for (int i = 2; i <= value; i++)
            {
                res = res * i;
            }
            return res;
        }

        public static double Percent(double value)
        {
            return value / 100;
        }

        public static double Exp(double x)
        {
            return Math.Exp(x);
        }

        public static double Asin(double value)
        {
            if (value < -1 || value > 1)
            {
                throw new ArgumentException("Invalid value.");
            }
            return Math.Asin(value);
        }

        public static double Acos(double value)
        {
            if (value < -1 || value > 1)
            {
                throw new ArgumentException("Invalid value.");
            }
            return Math.Acos(value);
        }

        public static double Atan(double value)
        {
            return Math.Atan(value);
        }
    }
}
