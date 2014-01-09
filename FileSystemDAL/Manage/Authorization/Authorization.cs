
namespace FileSystemDAL.Manage
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using FileSystemDAL.Enum;
    using FileSystemDAL.Helper;
    using FileSystemDAL.Models;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Linq;

    /// <summary>
    /// The authorization.
    /// </summary>
    public class Authorization
    {
        /// <summary>
        /// The verify email.
        /// </summary>
        /// <param name="emial">
        /// The emial.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool VerifyEmail(string emial)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var exist = session.QueryOver<Person>().Where(x => x.Email == emial).List().Any();
                return exist;
            }
        }

        /// <summary>
        /// The verify person.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool VerifyPerson(string email, string password)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    using (var md5 = MD5.Create())
                    {
                        string hashPassword = this.GetMd5Hash(md5, password);
                        var exist =
                            session.QueryOver<Person>().Where(x => x.Email == email).Where(x => x.Password == hashPassword).List().Any();
                        return exist;
                    }
                }
            }
        }

        /// <summary>
        /// The verify repository.
        /// </summary>
        /// <param name="repositoryName">
        /// The repository name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool VerifyRepository(string repositoryName)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var exist = session.QueryOver<Repository>().Where(x => x.RepositoryName == repositoryName).List().Any();
                    return exist;
                }
            }
        }

        /// <summary>
        /// The verify person.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public Person GetPerson(string email, string password)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    using (var md5 = MD5.Create())
                    {
                        string hashPassword = this.GetMd5Hash(md5, password);
                        var person =
                            session.CreateCriteria(typeof(Person))
                                .Add(Restrictions.Eq("Email", email))
                                .Add(Restrictions.Eq("Password", hashPassword))
                                .UniqueResult<Person>();
                        return person;
                    }
                }
            }
        }

        /// <summary>
        /// The register person.
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
        /// <param name="repositoryName">
        /// The repository name.
        /// </param>
        public void RegisterPerson(string email, string personName, string password, string repositoryName)
        {
            var repository = new Repository
            {
                DateAttach = DateTime.Now,
                RepositoryName = repositoryName,
                IsActive = false
            };
            var repositoryIdentifier = this.AddRepository(repository);

            using (var md5 = MD5.Create())
            {
                string hashPassword = this.GetMd5Hash(md5, password);
                var person = new Person
                {
                    DateAttach = DateTime.Now,
                    Email = email,
                    Password = hashPassword,
                    Permission = EPermission.RepositoryAdmin,
                    PersonName = personName,
                    RepositoryId = repositoryIdentifier
                };

                this.AddPerson(person);
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
        /// The add repository.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int AddRepository(Repository repository)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var repositoryIdentity = (int)session.Save(repository);
                    transaction.Commit();
                    return repositoryIdentity;
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
    }
}
