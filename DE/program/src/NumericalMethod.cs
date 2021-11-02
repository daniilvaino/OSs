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
