using Api.Data.Mappings;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Contexts {
    public class MyContext : DbContext {
        public DbSet<UserEntity> Users { get; set; }
        public MyContext (DbContextOptions<MyContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {

            base.OnModelCreating (modelBuilder);
            modelBuilder.Entity<UserEntity> (new UserMap().Configure);
        }
    }

}