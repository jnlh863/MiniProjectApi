using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace GestiondeEventos.Models.Dtos
{
    public class UserDTO
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        public string email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatorio")]
        public string password { get; set; } = null!;
    }
}
