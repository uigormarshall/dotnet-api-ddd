using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repositories;
using Domain.Dtos;
using Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private readonly SigningConfiguration _signingConfiguration;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IConfiguration _configuration;

        public LoginService(IUserRepository repository, SigningConfiguration signingConfiguration, TokenConfiguration tokenConfiguration, IConfiguration configuration)
        {
            _repository = repository;
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDTO login)
        {
            var user = await _repository.FindByLogin(login.Email);
            if (user != null)
            {
                var identity = new ClaimsIdentity(new GenericIdentity(login.Email),new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, login.Email) 
                });
                var created =  DateTime.Now;
                var expiration = created + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
                
                var handler = new JwtSecurityTokenHandler();
                var token = CreateToken( identity, created, expiration, handler);
                return new {
                    authenricated = true,
                    message = "Autenticado com sucesso.",
                    token
                };
            }
                    

            return new {
                authenricated = false,
                message = "Falha ao tentar autenticar.",
                token = ""
            };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime created, DateTime expiration,
            JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = created,
                Expires = expiration
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }
    }
}