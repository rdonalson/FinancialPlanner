using System;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FinancialPlanner.Web.Helpers
{
    public static class AssemblyHelper
    {
        public static HtmlString ApplicationVersion()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Version version = asm.GetName().Version;
            var copyRight =
                asm.GetCustomAttributes(typeof (AssemblyCopyrightAttribute), true).FirstOrDefault() as
                    AssemblyCopyrightAttribute;
            var title =
                asm.GetCustomAttributes(typeof (AssemblyTitleAttribute), true).FirstOrDefault() as
                    AssemblyTitleAttribute;

            if (version != null && copyRight != null && title != null)
            {
                return
                    new HtmlString(string.Format("{0} | {1} v{2}.{3}.{4} ({5})", copyRight.Copyright,
                        title.Title, version.Major, version.Minor, version.Build, version.Revision));
            }
            return new HtmlString("");
        }

        public static HtmlString ApplicationTitle()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var title =
                asm.GetCustomAttributes(typeof (AssemblyTitleAttribute), true).FirstOrDefault() as
                    AssemblyTitleAttribute;

            if (title != null)
            {
                return
                    new HtmlString(string.Format("{0}", title.Title));
            }
            return new HtmlString("");
        }
    }
}