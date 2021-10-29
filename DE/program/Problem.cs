// Decompiled with JetBrains decompiler
// Type: ComputationalPractice.Problem
// Assembly: ComputationalPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 82529BA9-080A-4E86-A8A8-FD182D48B489
// Assembly location: C:\Users\Lenovo\AppData\Local\Apps\2.0\K37TJG8Z.6GB\5HXM0QQ7.WLA\comp..tion_8f38878aa02c0079_0001.0000_e8ad7f6c69bbf218\ComputationalPractice.exe

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
