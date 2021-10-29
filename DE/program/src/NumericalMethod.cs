// Decompiled with JetBrains decompiler
// Type: ComputationalPractice.src.NumericalMethod
// Assembly: ComputationalPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 82529BA9-080A-4E86-A8A8-FD182D48B489
// Assembly location: C:\Users\Lenovo\AppData\Local\Apps\2.0\K37TJG8Z.6GB\5HXM0QQ7.WLA\comp..tion_8f38878aa02c0079_0001.0000_e8ad7f6c69bbf218\ComputationalPractice.exe

using System.Collections.Generic;

namespace ComputationalPractice.src
{
    internal abstract class NumericalMethod
    {
        public string Name { get; set; }
        public abstract Dictionary<double, double> GetSample(double x0, double y0, double X, int N);

        public abstract Dictionary<double, double> GetError(double x0, double y0, double X, int N);

        public abstract List<KeyValuePair<double, double>> GetGTE(
          double x0,
          double y0,
          double X,
          int N);
        public double x0 { get; set; }

        public double y0 { get; set; }

        public double X { get; set; }

        public int N { get; set; }

        public KeyValuePair<double, double> MaxError { get; set; }
        public double GlobalError { get; set; }

        public Dictionary<double, double> KeyValuePairs { get; set; }

        public Dictionary<double, double> Error { get; set; }

        public List<KeyValuePair<double, double>> GTE { get; set; }
    }

}
