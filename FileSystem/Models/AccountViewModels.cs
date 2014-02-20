
namespace FileSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FileSystemDAL.Models;

    /// <summary>
    /// The e user permission.
    /// </summary>
    public enum EUserPermission
    {
        /// <summary>
        /// The user high.
        /// </summary>
        UserHigh = 2,

        /// <summary>
        /// The user low.
        /// </summary>
        UserMedium = 3,

        /// <summary>
        /// The user low.
        /// </summary>
        UserLow = 4
    }

    /// <summary>
    /// The external login confirmation view model.
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    /// <summary>
    /// The share file view model.
    /// </summary>
    public class ShareFileViewModel
    {
        /// <summary>
        /// Gets or sets the share file id.
        /// </summary>
        public int ShareFileId { get; set; }

        /// <summary>
        /// Gets or sets the friend list.
        /// </summary>
        public IList<Repository> FriendList { get; set; }

        /// <summary>
        /// Gets or sets the shared files.
        /// </summary>
        public IList<SharedFile> SharedFiles { get; set; } 
    }

    /// <summary>
    /// The share file view model.
    /// </summary>
    public class ShareFolderViewModel
    {
        /// <summary>
        /// Gets or sets the share file id.
        /// </summary>
        public int ShareFolderId { get; set; }

        /// <summary>
        /// Gets or sets the friend list.
        /// </summary>
        public IList<Repository> FriendList { get; set; }

        /// <summary>
        /// Gets or sets the shared files.
        /// </summary>
        public IList<SharedFolder> SharedFolders{ get; set; }
    }

    /// <summary>
    /// The share file view model.
    /// </summary>
    public class SharedFoldersViewModel
    {
        /// <summary>
        /// Gets or sets the repository name.
        /// </summary>
        public string RepositoryName { get; set; }

        /// <summary>
        /// Gets or sets the repository id.
        /// </summary>
        public int RepositoryId { get; set; }

        /// <summary>
        /// Gets or sets the folders.
        /// </summary>
        public IList<Folder> Folders { get; set; }
    }

    /// <summary>
    /// The create folder view model.
    /// </summary>
    public class CreateFolderViewModel
    {
        /// <summary>
        /// Gets or sets the parrent folder id.
        /// </summary>
        public int? FolderId { get; set; }

        /// <summary>
        /// Gets or sets the folder name.
        /// </summary>
        [Required]
        [Display(Name = "Folder name")]
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [Display(Name = "Permission")]
        public EUserPermission UserPermission { get; set; }
    }

    /// <summary>
    /// The create folder view model.
    /// </summary>
    public class UploadFileViewModel
    {
        /// <summary>
        /// Gets or sets the folder id.
        /// </summary>
        public int? FolderId { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        [Required]
        [Display(Name = "File")]
        public string File { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [Display(Name = "Permission")]
        public EUserPermission UserPermission { get; set; }
    }

    /// <summary>
    /// The repository view model.
    /// </summary>
    public class RepositoryViewModel
    {
        /// <summary>
        /// Gets or sets the repository id.
        /// </summary>
        public int RepositoryId { get; set; }

        /// <summary>
        /// Gets or sets the repository name.
        /// </summary>
        public string RepositoryName { get; set; }

        /// <summary>
        /// Gets or sets the path folder dictionary.
        /// </summary>
        public List<Folder> PathFolderList { get; set; }

        /// <summary>
        /// Gets or sets the fileses.
        /// </summary>
        public IList<Files> Fileses { get; set; }

        /// <summary>
        /// Gets or sets the folders.
        /// </summary>
        public IList<Folder> Folders { get; set; } 
    }

    /// <summary>
    /// The repository search.
    /// </summary>
    public class RepositorySearch
    {
        /// <summary>
        /// Gets or sets the repository name.
        /// </summary>
        [Required]
        [Display(Name = "Repository Name")]
        public string RepositoryName { get; set; }

        /// <summary>
        /// Gets or sets the repository list.
        /// </summary>
        public IList<Repository> SearchRepositoryList { get; set; }

        /// <summary>
        /// Gets or sets the repository list.
        /// </summary>
        public IList<Repository> SentInvitationList { get; set; }

        /// <summary>
        /// Gets or sets the accepted invitation list.
        /// </summary>
        public IList<Repository> AcceptedInvitationList { get; set; }

        /// <summary>
        /// Gets or sets the incoming invitation list.
        /// </summary>
        public IList<Repository> IncomingInvitationList { get; set; } 
    }

    /// <summary>
    /// The manage user view model.
    /// </summary>
    public class ManageUserViewModel
    {
        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// The login view model.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether remember me.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// The register view model.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the repository name.
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Repository name")]
        public string RepositoryName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// The register view model.
    /// </summary>
    public class CreateUserViewModel
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [Display(Name = "User permission")]
        public EUserPermission UserPermission { get; set; }
    }
}
