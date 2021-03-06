using System;
using System.Collections.Generic;

namespace ComputationalPractice.src
{
    internal class RungeKuttaNM : NumericalMethod
    {


        public RungeKuttaNM(double x0, double y0, double X, int N)
        {
            this.Name = "Runge Kutta";
            if (x0 > X)
                throw new Exception("The Domain is not valid! \n X > x0");
            if (N <= 0)
                throw new Exception("Invalid Number of Samples! \n N < 0 or N = 0");
            this.x0 = x0;
            this.y0 = y0;
            this.X = X;
            this.N = N;
            this.KeyValuePairs = this.GetSample(x0, y0, X, N);
            this.Error = this.GetError(x0, y0, X, N);
            this.GTE = this.GetGTE(x0, y0, X, N);
        }

        public override Dictionary<double, double> GetSample(double x0, double y0, double X, int N)
        {
            double num1 = (X - x0) / (double)N;
            Dictionary<double, double> dictionary = new Dictionary<double, double>();
            dictionary.Add(x0, y0);
            for (int index = 1; index <= N; ++index)
            {
                double num2 = Problem.f(x0 + (double)(index - 1) * num1, dictionary[x0 + (double)(index - 1) * num1]);
                double num3 = Problem.f(x0 + ((double)index - 0.5) * num1, dictionary[x0 + (double)(index - 1) * num1] + num1 * (num2 / 2.0));
                double num4 = Problem.f(x0 + ((double)index - 0.5) * num1, dictionary[x0 + (double)(index - 1) * num1] + num1 * (num3 / 2.0));
                double num5 = Problem.f(x0 + (double)index * num1, dictionary[x0 + (double)(index - 1) * num1] + num1 * num4);
                dictionary.Add(x0 + (double)index * num1, dictionary[x0 + (double)(index - 1) * num1] + num1 * (num2 + 2.0 * num3 + 2.0 * num4 + num5) / 6.0);
            }
            return dictionary;
        }

        public override Dictionary<double, double> GetError(double x0, double y0, double X, int N)
        {
            double num1 = (X - x0) / (double)N;
            Dictionary<double, double> dictionary = new Dictionary<double, double>();
            Dictionary<double, double> sample = this.GetSample(x0, y0, X, N);
            dictionary.Add(x0, 0.0);
            this.MaxError = new KeyValuePair<double, double>(x0, 0.0);
            this.GlobalError = 0;
            for (int index = 1; index <= N; ++index)
            {
                double num2 = Math.Abs(Problem.y(x0 + (double)index * num1, x0, y0) - sample[x0 + (double)index * num1]);
                dictionary.Add(x0 + (double)index * num1, num2);
                if (num2 > this.MaxError.Value)
                    this.MaxError = new KeyValuePair<double, double>(x0 + (double)index * num1, num2);
                this.GlobalError = num2;
            }
            return dictionary;
        }

        public override List<KeyValuePair<double, double>> GetGTE(
         double x0,
         double y0,
         double X,
         int N)
        {
            double num = (X - x0) / (double)N;
            List<KeyValuePair<double, double>> keyValuePairList = new List<KeyValuePair<double, double>>();
            //keyValuePairList.Add(new KeyValuePair<double, double>(x0, 0.0));
            KeyValuePair<double, double> maxError = this.MaxError;
            for (int N1 = Math.Max(1, N - 100); N1 <= N; ++N1)
            {
                this.GetError(x0, y0, X, N1);
                keyValuePairList.Add(new KeyValuePair<double, double>((double)N1, this.GlobalError));
            }
            this.MaxError = maxError;
            return keyValuePairList;
        }
    }
}
