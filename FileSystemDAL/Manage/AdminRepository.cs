

namespace FileSystemDAL.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using FileSystemDAL.Enum;
    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;

    using Microsoft.AspNet.Identity;

    using NHibernate;
    using NHibernate.Criterion;

    /// <summary>
    /// The admin repository.
    /// </summary>
    public class AdminRepository : PersonRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminRepository"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public AdminRepository(Repository repository) : base(repository, EPermission.RepositoryAdmin)
        {
            this.PartnershipManager = new PartnershipManager(repository);
        }

        /// <summary>
        /// Gets the partnership manager.
        /// </summary>
        public PartnershipManager PartnershipManager { get; private set; }

        /// <summary>
        /// The validate user account async.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IdentityResult> ValidateUserAccountAsync(string email, string userName)
        {
            var errors = new List<string>();

            if (this.VerifyEmail(email))
            {
                errors.Add("E-mail exist in system. Enter different e-mail.");
            }

            if (this.VerifyUserName(userName))
            {
                errors.Add("User name exist in system. Enter different name.");
            }

            return errors.Any() ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        /// <summary>
        /// The get repository users.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Person> GetRepositoryUsers()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(Person))
                    .Add(Restrictions.Eq("RepositoryId", this.MyRepository.Repository.RepositoryId))
                    .Add(Restrictions.Gt("Permission", EPermission.RepositoryAdmin))
                    .List<Person>();
            }
        }

        /// <summary>
        /// The search repository.
        /// </summary>
        /// <param name="repositoryName">
        /// The repository name.
        /// </param>
        /// <returns>
        /// The <see cref="Repository"/>.
        /// </returns>
        public Repository SearchRepository(string repositoryName)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.CreateCriteria(typeof(Repository))
                    .Add(Restrictions.Eq("RepositoryName", repositoryName))
                    .UniqueResult<Repository>();
            }
        }

        /// <summary>
        /// The create person account.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="personName">
        /// The person name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="permission">
        /// The permission.
        /// </param>
        /// <param name="repositoryId">
        /// The repository id.
        /// </param>
        /// <returns>
        /// The <see cref="Person"/>.
        /// </returns>
        public Person CreatePersonAccount(string email, string personName, string password, EPermission permission, int repositoryId)
        {
            using (var md5 = MD5.Create())
            {
                string hashPassword = this.GetMd5Hash(md5, password);
                var person = new Person
                {
                    DateAttach = DateTime.Now,
                    Email = email,
                    Password = hashPassword,
                    Permission = permission,
                    PersonName = personName,
                    RepositoryId = repositoryId
                };

                person.PersonId = this.AddPerson(person);
                return person;
            }
        }

        /// <summary>
        /// The update person permission.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="permission">
        /// The permission.
        /// </param>
        public void UpdatePersonPermission(int userId, EPermission permission)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var person =
                        session.CreateCriteria(typeof(Person))
                            .Add(Restrictions.Eq("PersonId", userId))
                            .UniqueResult<Person>();

                    person.Permission = permission;
                    session.SaveOrUpdate(person);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The delete person.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        public void DeletePerson(int userId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(string.Format("from {0} where PersonID = {1}", typeof(Person), userId));
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// The add person.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int AddPerson(Person person)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var identifier = (int)session.Save(person);
                    transaction.Commit();
                    return identifier;
                }
            }
        }

        /// <summary>
        /// The get md 5 hash.
        /// </summary>
        /// <param name="md5Hash">
        /// The md 5 hash.
        /// </param>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetMd5Hash(HashAlgorithm md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// The verify email.
        /// </summary>
        /// <param name="emial">
        /// The emial.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool VerifyEmail(string emial)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var exist = session.QueryOver<Person>().Where(x => x.Email == emial).List().Any();
                return exist;
            }
        }

        /// <summary>
        /// The verify user name.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool VerifyUserName(string userName)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var exist = session.QueryOver<Person>().Where(x => x.PersonName == userName).List().Any();
                return exist;
            }
        }
    }
}
