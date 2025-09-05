using FluentValidation;

namespace ManagerProperties.Application.UseCases.Properties.Commands.CreateProperty
{
    public class ValidatorCommandCreateProperty:AbstractValidator<CommandCreateProperty>
    {
        public ValidatorCommandCreateProperty()
        {
            RuleFor(x => x.OwnerId).NotEmpty().WithMessage("El campo {PropertyName} es requerido.");
            RuleFor(x => x.CodeInternal).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(20).WithMessage("La longitud del campo {PropertyName} debe ser menor que {MaxLength}");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(60).WithMessage("La longitud del campo {PropertyName} debe ser menor que {MaxLength}");
            RuleFor(x => x.Address).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(100).WithMessage("La longitud del campo {PropertyName} debe ser menor que {MaxLength}");
            RuleFor(x => x.Price).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .GreaterThan(0).WithMessage("El valor del campo {PropertyName} debe ser mayor que cero.");
            RuleFor(x => x.Year).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .InclusiveBetween(1900, DateTime.Now.Year)
                .WithMessage("El valor del campo {PropertyName} debe ser mayor a 1900 y menor que el año actual.");

        }
    }
}
