using System.Threading.Tasks;
using Api.Domain.Entities;
using Domain.Dtos;

namespace Api.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
         Task<object> FindByLogin(LoginDTO user);
    }
}