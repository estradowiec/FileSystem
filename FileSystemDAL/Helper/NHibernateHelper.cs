
namespace FileSystemDAL.Helper
{
    using FileSystemDAL.Models;

    using NHibernate;
    using NHibernate.Cfg;

    /// <summary>
    /// The n hibernate helper.
    /// </summary>
    public class NHibernateHelper
    {
        /// <summary>
        /// The _session factory.
        /// </summary>
        private static ISessionFactory sessionFactory;

        /// <summary>
        /// Gets the session factory.
        /// </summary>
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory != null)
                {
                    return sessionFactory;
                }

                var configuration = new Configuration();
                configuration.Configure();
                sessionFactory = configuration.BuildSessionFactory();
                return sessionFactory;
            }
        }

        /// <summary>
        /// The open session.
        /// </summary>
        /// <returns>
        /// The <see cref="ISession"/>.
        /// </returns>
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
