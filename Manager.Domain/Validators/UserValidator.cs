using Manager.Domain.Entities;
using FluentValidation;

namespace Manager.Domain.Validators
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode estar vazia")
                .NotNull()
                .WithMessage("A entidade não pode ser nula");

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O Nome não pode ser nulo")
                .NotEmpty()
                .WithMessage("O Nome não pode estar vazio")
                .MinimumLength(3)
                .WithMessage("O Nome deve ter no mínimo 3 caracteres")
                .MaximumLength(80)
                .WithMessage("O Nome deve ter no máximo 80 caractres");
            
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("O E-Mail não pode ser nulo")
                .NotEmpty()
                .WithMessage("O E-Mail não pode estar vazio")
                .EmailAddress()
                .WithMessage("E-Mail inválido");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("A Senha não pode ser nulo")
                .NotEmpty()
                .WithMessage("A Senha não pode estar vazia")
                .MinimumLength(8)
                .WithMessage("A sebga deve ter no mínimo 6 caracteres");
        }
    }
}