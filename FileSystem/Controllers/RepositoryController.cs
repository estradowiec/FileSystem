
namespace FileSystem.Controllers
{
    using System.Configuration;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Routing;

    using FileSystem.Models;

    using FileSystemDAL.Enum;
    using FileSystemDAL.Manage;

    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The repository controller.
    /// </summary>
    public class RepositoryController : Controller
    {
        /// <summary>
        /// The authorization.
        /// </summary>
        private PersonRepository personRepository;

        /// <summary>
        /// The authorization.
        /// </summary>
        private Authorization authorization;

        /// <summary>
        /// GET: /Repository/
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// The my repository.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult MyRepository(int? folderId = null)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if ((int)user.Permission == 0)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var repositoryData = new RepositoryViewModel();
            repositoryData.RepositoryName = this.personRepository.MyRepository.Repository.RepositoryName;
            repositoryData.Folders = this.personRepository.MyRepository.GetFolders(folderId);
            repositoryData.Fileses = this.personRepository.MyRepository.GetFiles(folderId);
            repositoryData.PathFolderList = this.personRepository.MyRepository.GetPathFoldersDictionary(folderId);

            return this.View(repositoryData);
        }

        /// <summary>
        /// The _ create folder partial.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult _CreateFolderPartial(CreateFolderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = this.personRepository.MyRepository.ValidateFolderNameAsync(model.FolderId, model.FolderName);
                if (result.Succeeded)
                {
                    this.personRepository.CreateFolder(model.FolderId, model.FolderName, (EPermission)(int)model.UserPermission);

                    return this.Json(new { Success = true });
                }

                this.AddErrors(result);
            }

            return this.PartialView(model);
        }

        /// <summary>
        /// The upload file.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult UploadFile()
        {
            var file = Request.Files[0];
            var asd = Request.Form.Get("example");
            int fileId = 0;
            if (Request.Headers.Get("Content-Range") != null)
            {
                string pattern = @"bytes (\d+)-(\d+)/(\d+)";
                var ranges = Request.Headers.Get("Content-Range");
                string rangeStart = Regex.Replace(ranges, pattern, "$1");
                if (int.Parse(rangeStart).Equals(0))
                {
                    //fileId = this.personRepository.InitFile(file.FileName, file.InputStream.Length, null, EPermission.UserLow);
                }
            }

            //this.personRepository.UploadFile(ConfigurationManager.AppSettings["UploadPath"], fileId, file.InputStream);

            return this.Json(new { Success = true });
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        /// <param name="requestContext">
        /// The request context.
        /// </param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (!this.User.Identity.IsAuthenticated)
            {
                return;
            }

            this.authorization = new Authorization();
            var user = this.authorization.GetPerson(int.Parse(this.User.Identity.GetUserId()));
            if ((int)user.Permission > 0)
            {
                this.personRepository = new PersonRepository(this.authorization.GetRepository(user.PersonId), user.Permission);
            }
        }

        /// <summary>
        /// The add errors.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}