namespace FileSystem.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The repository user controller.
    /// </summary>
    public partial class RepositoryUserController : Controller
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

        #region ManageAccount

        /// <summary>
        /// The manage user account.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult ManageUser()
        {
            return this.View();
        }

        #endregion

        #region CreateUserAccount

        /// <summary>
        /// The create user account.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult CreateUser()
        {
            return this.View();
        }

        #endregion
    }
}
