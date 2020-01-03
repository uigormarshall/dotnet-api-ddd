using System.Threading.Tasks;
using Api.Data.Contexts;
using Api.Domain.Entities;
using Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataset;
        public UserRepository(MyContext context) : base(context)
        {
            _dataset = context.Set<UserEntity>();
        }

        public async Task<UserEntity> FindByLogin(string email)
        {
           return await _dataset.FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}