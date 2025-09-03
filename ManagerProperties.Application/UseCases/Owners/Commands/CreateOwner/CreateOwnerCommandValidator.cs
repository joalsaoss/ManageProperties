using FluentValidation;

namespace ManagerProperties.Application.UseCases.Owners.Commands.CreateOwner
{
    public class CreateOwnerCommandValidator:AbstractValidator<CommandCreateOwner>
    {
        public CreateOwnerCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("El campo nombre es requerido.");
            RuleFor(p => p.Address).NotEmpty().WithMessage("El campo dirección es requerido.");
            RuleFor(p => p.Photo).NotEmpty().WithMessage("El campo foto es requerido.");
            RuleFor(p => p.Birthday).NotEmpty().WithMessage("El campo fecha de nacimiento es requerido.");
        }
    }
}
