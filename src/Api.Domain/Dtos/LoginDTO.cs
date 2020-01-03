using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email é um campo obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail com formato inválido.")]
        [MaxLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }
    }
}