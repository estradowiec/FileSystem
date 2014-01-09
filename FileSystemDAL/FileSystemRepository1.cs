
namespace FileSystemDAL
{
    using System;

    using FileSystemDAL.Models;

    using NHibernate;
    using NHibernate.Cfg;

    public class FileSystemRepository1
    {
        /*
        public FileSystemRepository1()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    Users users = new Users
                    {
                        DateAttach = DateTime.Now,
                        Email = "asdads",
                        Password = "asdads",
                        PermissionId = new Guid(),
                        RepositoryId = new Guid(),
                        UserName = "Tom"
                    };
                    mySession.Save(users);
                    mySession.Transaction.Commit();
                }
            }

        }*/
    }
}
