// Decompiled with JetBrains decompiler
// Type: ComputationalPractice.Program
// Assembly: ComputationalPractice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 82529BA9-080A-4E86-A8A8-FD182D48B489
// Assembly location: C:\Users\Lenovo\AppData\Local\Apps\2.0\K37TJG8Z.6GB\5HXM0QQ7.WLA\comp..tion_8f38878aa02c0079_0001.0000_e8ad7f6c69bbf218\ComputationalPractice.exe

using System;
using System.Windows.Forms;

namespace ComputationalPractice
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((Form)new CP());
        }
    }
}
