using System;

namespace ComputationalPractice
{
    internal class Problem
    {
        public static double f(double x, double y) =>
            Math.Exp(2 * x) + Math.Exp(x) + Math.Pow(y, 2) - 2 * y * Math.Exp(x);
        public static double c(double x, double y) =>
            1 / (y + Math.Exp(x)) - x;
        public static double y(double x, double x0, double y0)
              {
            var С = c(x0, y0);
            var Y = Math.Exp(x) - 1 / (x + С);
            return Y;
        }
  }
}
