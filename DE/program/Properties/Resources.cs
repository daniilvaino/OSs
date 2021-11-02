
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ComputationalPractice.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (ComputationalPractice.Properties.Resources.resourceMan == null)
          ComputationalPractice.Properties.Resources.resourceMan = new ResourceManager("ComputationalPractice.Properties.Resources", typeof (ComputationalPractice.Properties.Resources).Assembly);
        return ComputationalPractice.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => ComputationalPractice.Properties.Resources.resourceCulture;
      set => ComputationalPractice.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap iconfinder_help_216643 => (Bitmap) ComputationalPractice.Properties.Resources.ResourceManager.GetObject(nameof (iconfinder_help_216643), ComputationalPractice.Properties.Resources.resourceCulture);

    internal static Bitmap iconfinder_help_216643_1_ => (Bitmap) ComputationalPractice.Properties.Resources.ResourceManager.GetObject("iconfinder_help_216643(1)", ComputationalPractice.Properties.Resources.resourceCulture);

    internal static Bitmap Screenshot_2020_11_06_102417 => (Bitmap) ComputationalPractice.Properties.Resources.ResourceManager.GetObject("Screenshot 2020-11-06 102417", ComputationalPractice.Properties.Resources.resourceCulture);
  }
}
