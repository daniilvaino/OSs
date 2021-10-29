// Decompiled with JetBrains decompiler
// Type: ComputationalPractice.src.EulerNM
// Assembly: ComputationalPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 82529BA9-080A-4E86-A8A8-FD182D48B489
// Assembly location: C:\Users\Lenovo\AppData\Local\Apps\2.0\K37TJG8Z.6GB\5HXM0QQ7.WLA\comp..tion_8f38878aa02c0079_0001.0000_e8ad7f6c69bbf218\ComputationalPractice.exe

using System;
using System.Collections.Generic;

namespace ComputationalPractice.src
{
    internal class EulerNM : NumericalMethod
    {
        public EulerNM(double x0, double y0, double X, int N)
        {
            this.Name = "Euler";
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
            double num = (X - x0) / (double)N;
            Dictionary<double, double> dictionary = new Dictionary<double, double>();
            dictionary.Add(x0, y0);
            for (int index = 1; index <= N; ++index)
                dictionary.Add(x0 + (double)index * num, dictionary[x0 + (double)(index - 1) * num] + num * Problem.f(x0 + (double)index * num, dictionary[x0 + (double)(index - 1) * num]));
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
                    this.MaxError = new KeyValuePair<double, double>(X + (double)index * num1, num2);
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
