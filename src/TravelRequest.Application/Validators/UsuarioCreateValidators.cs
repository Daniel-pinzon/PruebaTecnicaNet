using FluentValidation;
using TravelRequest.Application.DTOs;

namespace TravelRequest.Application.Validators
{
    public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioCreateDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre no debe exceder los 50 caracteres.");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(50).WithMessage("El apellido no debe exceder los 50 caracteres.");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("Debe ser un correo válido.");

            RuleFor(x => x.Rol)
                .NotEmpty().WithMessage("El rol es obligatorio.")
                .Must(r => r == "Solicitante" || r == "Aprobador")
                .WithMessage("Rol no válido. Debe ser 'Admin' o 'User'.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
        }
    }
}
