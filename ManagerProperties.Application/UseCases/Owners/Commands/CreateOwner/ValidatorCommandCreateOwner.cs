using FluentValidation;

namespace ManagerProperties.Application.UseCases.Owners.Commands.CreateOwner
{
    public class ValidatorCommandCreateOwner:AbstractValidator<CommandCreateOwner>
    {
        public ValidatorCommandCreateOwner()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(50).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLenght}");
            RuleFor(p => p.Address).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(100).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLenght}");
            RuleFor(p => p.Photo).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(100).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLenght}");
            RuleFor(p => p.Birthday).NotEmpty().WithMessage("El campo {PropertyName} es requerido.");
        }
    }
}
