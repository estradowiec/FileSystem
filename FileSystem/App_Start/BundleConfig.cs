


namespace FileSystem
{
    using System.Web.Optimization;

    /// <summary>
    /// The bundle config.
    /// </summary>
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            ////*******SCRIPTS***********
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.dataTables").Include(
                       "~/Scripts/jquery.dataTables.js",
                       "~/Scripts/dataTables.editor.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-switch").Include(
            "~/Scripts/bootstrap-switch.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
                        "~/Scripts/bootstrap-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.fileupload").Include(
                        "~/Scripts/jquery-ui.js",
                       "~/Scripts/jquery.fileupload.js",
                       "~/Scripts/jquery.iframe-transport.js"));


            ////******STYLES************

            bundles.Add(new StyleBundle("~/Content/bootstrap-switch").Include(
            "~/Content/bootstrap-switch.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-select").Include(
            "~/Content/bootstrap-select.css"));
          
            bundles.Add(new StyleBundle("~/Content/jquery.dataTables").Include(
            "~/Content/jquery.dataTables.css",
            "~/Content/jquery.dataTables.bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jquery.fileupload").Include(
                "~/Content/jquery.fileupload.css"));
        }
    }
}
