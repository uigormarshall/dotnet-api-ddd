using Api.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    public class ConfigurationContext
    {
        public static void ConfigureDependeciesContext(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql("Server=localhost;Port=3306;Database=dbAPI;Uid=marshall;Pwd=passwordTest")
            );
        }
    }
}