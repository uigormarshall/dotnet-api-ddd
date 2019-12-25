using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Contexts {
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext> {
        public MyContext CreateDbContext (string[] args) {
            var connectionString = "Server=localhost;Port-3306;Database=dbAPI;Uid=marshall;Pwd=passwordTest";
            var optinsBuilder = new DbContextOptionsBuilder<MyContext> ();
            optinsBuilder.UseMySql (connectionString);
            return new MyContext (optinsBuilder.Options);
        }
    }
}