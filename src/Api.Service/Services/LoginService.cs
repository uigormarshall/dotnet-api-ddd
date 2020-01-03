using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repositories;
using Domain.Dtos;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        public LoginService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserEntity> FindByLogin(LoginDTO user)
        {
            return await _repository.FindByLogin(user.Email);
        }
    }
}