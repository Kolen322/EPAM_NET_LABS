using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using module_20.DAL.EntityFramework;

namespace module_20.Web.Extensions
{
    /// <summary>
    /// Class that contains connection's extensions
    /// </summary>
    public static class SetUpConnectionExtension
    {
        /// <summary>
        /// Set Up connection to sql server
        /// </summary>
        /// <param name="services">Services of api</param>
        /// <param name="connectionString">Connection string</param>
        public static void SetUpSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>
                (options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("module_20.DAL")));
        }
    }
}
