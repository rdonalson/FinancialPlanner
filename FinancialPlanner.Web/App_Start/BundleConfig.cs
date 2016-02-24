using System.Web.Optimization;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;

namespace FinancialPlanner.Web
{

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            var cssTransformer = new StyleTransformer();
            var jsTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            var cssBundle = new StyleBundle("~/bundles/css");
            cssBundle.Include(
                "~/Content/fp-custom.less",
                "~/Content/fp-standard.less");   //fp-spacelab
            cssBundle.Transforms.Add(cssTransformer);
            cssBundle.Orderer = nullOrderer;
            bundles.Add(cssBundle);

            var jqueryBundle = new ScriptBundle("~/bundles/jquery");
            jqueryBundle.Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.custom.js",
                "~/Scripts/bootstrap-datepicker.js");
            jqueryBundle.Transforms.Add(jsTransformer);
            jqueryBundle.Orderer = nullOrderer;
            bundles.Add(jqueryBundle);

            var jqueryvalBundle = new ScriptBundle("~/bundles/jqueryval");
            jqueryvalBundle.Include("~/Scripts/jquery.validate*");
            jqueryvalBundle.Transforms.Add(jsTransformer);
            jqueryvalBundle.Orderer = nullOrderer;
            bundles.Add(jqueryvalBundle);

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            var modernizrBundle = new ScriptBundle("~/bundles/modernizr");
            modernizrBundle.Include("~/Scripts/modernizr-*");
            modernizrBundle.Transforms.Add(jsTransformer);
            modernizrBundle.Orderer = nullOrderer;
            bundles.Add(modernizrBundle);

            var bootstrapBundle = new ScriptBundle("~/bundles/bootstrap");
            bootstrapBundle.Include(
                "~/Scripts/bootstrap.js", 
                "~/Scripts/respond.js");
            bootstrapBundle.Transforms.Add(jsTransformer);
            bootstrapBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapBundle);

            var autoNumericBundle = new ScriptBundle("~/bundles/autoNumeric");
            autoNumericBundle.Include("~/Scripts/autoNumeric-{version}.js");
            autoNumericBundle.Transforms.Add(jsTransformer);
            autoNumericBundle.Orderer = nullOrderer;
            bundles.Add(autoNumericBundle);

            var flotBundle = new ScriptBundle("~/bundles/flot");
            flotBundle.Include(
                "~/Scripts/flot/jquery.flot.js",
                "~/Scripts/flot/jquery.flot.stack.js",
                "~/Scripts/flot/jquery.flot.symbol.js",
                "~/Scripts/flot/jquery.flot.axislabels.js",
                "~/Scripts/flot/jquery.flot.fp-time.js",
                "~/Scripts/flot/jquery.flot.fp-tickrotor.js",
                "~/Scripts/jshashtable-{version}.js",
                "~/Scripts/jquery.numberformatter-{version}.js");
            flotBundle.Transforms.Add(jsTransformer);
            flotBundle.Orderer = nullOrderer;
            bundles.Add(flotBundle);
        }
    }
}
