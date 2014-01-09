
namespace FileSystem.Controllers
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using System.Web.Security;

    using FileSystemDAL;

    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// The file system repository.
        /// </summary>
        //private FileSystemRepository fileSystemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
            //this.fileSystemRepository = new FileSystemRepository(ConfigurationManager.ConnectionStrings["fileSystemDatabase"].ConnectionString);    
        }
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

        #region Login

        /// <summary>
        /// The login.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Login()
        {
            FileSystemRepository1 f = new FileSystemRepository1();

            return this.View();
        }

        #endregion
        /*
        [ChildActionOnly]
        public ActionResult Authorize()
        {
            
            var user = this.fileSystemRepository.GetUser("tomasz.nocon@hotmail.com");
            var serializeModel = new UserPrincipalSerializeModel();

            serializeModel.UserId = user.UserId;
            serializeModel.UserName = user.UserName;
            serializeModel.Email = user.Email;
            serializeModel.DateAttach = user.DateAttach;
            serializeModel.PermissionId = user.PermissionId;
            serializeModel.RepositoryId = user.RepositoryId;
            serializeModel.Password = user.Password;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     "tomasz.nocon@hotmail.com",
                     DateTime.Now,
                     DateTime.Now.AddMinutes(15),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
            
            //return this.Redirect("Index");
        }
        */
        #region RememberPassword

        /// <summary>
        /// The remember password.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult RememberPassword()
        {
            return this.View();
        }

        #endregion
    }
}
