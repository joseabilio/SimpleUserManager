using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required(ErrorMessage = "O Id não pode estar vazio")]
        [Range(1, long.MaxValue, ErrorMessage ="O Id não pode ser menor que 1")]
        public long Id { get; set; }
        [Required(ErrorMessage = "O Nome não pode estar vazio")]
        [MinLength(3, ErrorMessage = "O Nome deve ter no mínimo 3 caracteres")]
        [MaxLength(80, ErrorMessage = "O Nome deve ter no máximo 80 caractres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O Email não pode estar vazio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha não pode estar vazia")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres")]
        public string Password { get; set; }
    }
}