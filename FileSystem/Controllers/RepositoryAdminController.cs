
namespace FileSystem.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The repository admin controller.
    /// </summary>
    public partial class RepositoryAdminController : Controller
    {
        #region Index

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return this.View();
        }

        #endregion
    }
}
