
namespace FileSystem.Controllers
{
    using System.Configuration;
    using System.Globalization;
    using System.IO;
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
        #region Fields

        /// <summary>
        /// The authorization.
        /// </summary>
        private PersonRepository personRepository;

        /// <summary>
        /// The authorization.
        /// </summary>
        private Authorization authorization;

        /// <summary>
        /// The partnership manager.
        /// </summary>
        private PartnershipManager partnershipManager;

        #endregion

        #region MyRepository

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
            repositoryData.PathFolderList = this.personRepository.GetPathFoldersDictionary(folderId);

            return this.View(repositoryData);
        }

        #endregion

        #region FriendsRepository

        public ActionResult FriendsRepository()
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

            var friendsList = this.partnershipManager.GetFriends();

            return this.View(friendsList);
        }

        #endregion

        /// <summary>
        /// The friends repository data.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <param name="repositoryName">
        /// The repository name.
        /// </param>
        /// <returns>
        /// The <see cref="ViewResult"/>.
        /// </returns>
        public ViewResult FriendsRepositoryData(int? folderId, int repositoryId, string repositoryName)
        {
            var repositoryData = new RepositoryViewModel();
            repositoryData.RepositoryName = repositoryName;
            repositoryData.Fileses = this.personRepository.FriendRepository.GetFriendsFiles(folderId, repositoryId);
            repositoryData.Folders = this.personRepository.FriendRepository.GetFriendsFolders(folderId, repositoryId);
            repositoryData.PathFolderList = this.personRepository.GetPathFoldersDictionary(folderId);
            repositoryData.RepositoryId = repositoryId;

            return this.View(repositoryData);
        }

        #region _CreateFolderPartial

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

        #endregion

        #region UploadFile

        /// <summary>
        /// The upload file.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult UploadFile()
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

            var file = Request.Files[0];
            var fileUploadId = int.Parse(Request.Form.Get("fileUploadId"));

            if (file == null)
            {
                return this.Json(new { Success = false });
            }

            this.personRepository.UploadFile(ConfigurationManager.AppSettings["UploadPath"], fileUploadId, file.InputStream);
            return this.Json(new { Success = true });
        }

        #endregion

        #region InitUpload

        /// <summary>
        /// The init upload.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="fileSize">
        /// The file size.
        /// </param>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <param name="userPermission">
        /// The user permission.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult InitUpload(string fileName, decimal fileSize, int? folderId, EUserPermission userPermission)
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

            var fileUploadId = this.personRepository.InitFile(fileName, fileSize, folderId, (EPermission)(int)userPermission);

            return this.Json(new { FileUploadId = fileUploadId });
        }

        #endregion

        #region FinishUploadFile

        /// <summary>
        /// The finish upload file.
        /// </summary>
        /// <param name="fileUploadId">
        /// The file upload id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult FinishUploadFile(int fileUploadId)
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

            this.personRepository.FinishUploadFile(fileUploadId, ConfigurationManager.AppSettings["UploadPath"]);

            return this.Json(new { Success = true });
        }

        #endregion

        #region DeleteFile

        /// <summary>
        /// The delete file.
        /// </summary>
        /// <param name="fileId">
        /// The file id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult DeleteFile(int fileId)
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

            this.personRepository.DeleteFile(ConfigurationManager.AppSettings["UploadPath"], fileId);

            return this.Json(new { Success = true });
        }

        #endregion

        #region DownloadFile

        /// <summary>
        /// The download file.
        /// </summary>
        /// <param name="fileId">
        /// The file id.
        /// </param>
        /// <returns>
        /// The <see cref="FileResult"/>.
        /// </returns>
        public ActionResult DownloadFile(int fileId)
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

            const int BlockSize = 1024 * 512;

            var fileStream = this.personRepository.DownloadFile(ConfigurationManager.AppSettings["UploadPath"], fileId);

            var buffer = new byte[BlockSize];
            int bytesRead;
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(fileStream.Name) + "\"");
            Response.AddHeader("Content-Length", fileStream.Length.ToString(CultureInfo.InvariantCulture));
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                Response.OutputStream.Write(buffer, 0, bytesRead);
                Response.Flush();
            }

            fileStream.Close();
            return this.Json(new { Success = true });
        }

        #endregion

        #region DeleteFolder

        /// <summary>
        /// The delete folder.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult DeleteFolder(int folderId)
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

            this.personRepository.DeleteFolder(folderId, ConfigurationManager.AppSettings["UploadPath"]);

            return this.Json(new { Success = true });
        }

        #endregion

        #region ShareFolder

        /// <summary>
        /// The share folder.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult ShareFolder(int folderId, int repositoryId)
        {
            this.personRepository.ShareFolder(folderId, repositoryId);

            var shareFolderModel = new ShareFolderViewModel();
            shareFolderModel.FriendList = this.partnershipManager.GetFriends();
            shareFolderModel.SharedFolders = this.personRepository.MyRepository.GetSharedFolders();
            shareFolderModel.ShareFolderId = folderId;

            return this.PartialView("_ShareFolderPartial", shareFolderModel);
        }

        #endregion

        #region UnshareFolder

        /// <summary>
        /// The share folder.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult UnshareFolder(int folderId, int repositoryId)
        {
            this.personRepository.UnshareFolder(folderId, repositoryId);

            var shareFolderModel = new ShareFolderViewModel();
            shareFolderModel.FriendList = this.partnershipManager.GetFriends();
            shareFolderModel.SharedFolders = this.personRepository.MyRepository.GetSharedFolders();
            shareFolderModel.ShareFolderId = folderId;

            return this.PartialView("_ShareFolderPartial", shareFolderModel);
        }

        #endregion

        #region ShareFile

        /// <summary>
        /// The share file.
        /// </summary>
        /// <param name="fileId">
        /// The file id.
        /// </param>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult ShareFile(int fileId, int repositoryId)
        {
            this.personRepository.ShareFile(fileId, repositoryId);

            var shareFileModel = new ShareFileViewModel();
            shareFileModel.FriendList = this.partnershipManager.GetFriends();
            shareFileModel.SharedFiles = this.personRepository.MyRepository.GetSharedFiles();
            shareFileModel.ShareFileId = fileId;

            return this.PartialView("_ShareFilePartial", shareFileModel);
        }

        #endregion

        #region UnshareFile

        /// <summary>
        /// The share file.
        /// </summary>
        /// <param name="fileId">
        /// The file id.
        /// </param>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult UnshareFile(int fileId, int repositoryId)
        {
            this.personRepository.UnshareFile(fileId, repositoryId);

            var shareFileModel = new ShareFileViewModel();
            shareFileModel.FriendList = this.partnershipManager.GetFriends();
            shareFileModel.SharedFiles = this.personRepository.MyRepository.GetSharedFiles();
            shareFileModel.ShareFileId = fileId;

            return this.PartialView("_ShareFilePartial", shareFileModel);
        }

        #endregion

        #region _ShareFilePartial

        /// <summary>
        /// The _ share file partial.
        /// </summary>
        /// <param name="fileId">
        /// The file id.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult _ShareFilePartial(int? fileId)
        {
            var shareFileModel = new ShareFileViewModel();
            shareFileModel.FriendList = this.partnershipManager.GetFriends();
            shareFileModel.SharedFiles = this.personRepository.MyRepository.GetSharedFiles();
            if (fileId.HasValue)
            {
                shareFileModel.ShareFileId = fileId.Value;
            }

            return this.PartialView(shareFileModel);
        }

        #endregion

        #region _ShareFolderPartial

        /// <summary>
        /// The _ share folder partial.
        /// </summary>
        /// <param name="folderId">
        /// The folder id.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult _ShareFolderPartial(int? folderId)
        {
            var shareFolderModel = new ShareFolderViewModel();
            shareFolderModel.FriendList = this.partnershipManager.GetFriends();
            shareFolderModel.SharedFolders = this.personRepository.MyRepository.GetSharedFolders();

            if (folderId.HasValue)
            {
                shareFolderModel.ShareFolderId = folderId.Value;
            }

            return this.PartialView(shareFolderModel);
        }

        #endregion

        #region Initialize

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
                this.partnershipManager = new PartnershipManager(this.personRepository.MyRepository.Repository);
            }
        }

        #endregion

        #region AddErrors

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

        #endregion
    }
}