namespace FileSystem.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The file system controller.
    /// </summary>
    public class FileSystemController : Controller
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

        #region BrowseFiles

        /// <summary>
        /// The browse files.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult BrowseFiles()
        {
            return this.View();
        }

        #endregion

        #region UploadFile

        /// <summary>
        /// The upload file.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult UploadFile()
        {
            return this.View();
        }

        #endregion

        #region DownloadFile

        /// <summary>
        /// The download file.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult DownloadFile()
        {
            return this.View();
        }

        #endregion
    }
}
