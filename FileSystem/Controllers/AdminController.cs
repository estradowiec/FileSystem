
namespace FileSystem.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The admin controller.
    /// </summary>
    public class AdminController : Controller
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

        #region CreateCompanyAccount
        /// <summary>
        /// The create company account.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult CreateCompanyAccount()
        {
            return this.View();
        }
        #endregion

        #region ConfirmRegisterAccount
        /// <summary>
        /// The confirm register account.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult ConfirmRegisterAccount()
        {
            return this.View();
        }
        #endregion

        #region ManageAccounts
        /// <summary>
        /// The manage accounts.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult ManageAccounts()
        {
            return this.View();
        }
        #endregion
    }
}
