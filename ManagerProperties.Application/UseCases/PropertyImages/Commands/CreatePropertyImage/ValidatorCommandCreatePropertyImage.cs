using FluentValidation;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.CreatePropertyImage
{
    public class ValidatorCommandCreatePropertyImage:AbstractValidator<CommandCreatePropertyImage>
    {
        public ValidatorCommandCreatePropertyImage()
        {
            RuleFor(x => x.PropertyId).NotEmpty().WithMessage("El campo {PropertyName} es requerido.");
            RuleFor(p => p.PhotoFileName).NotEmpty().WithMessage("El campo {PropertyName} es requerido.");
            RuleFor(p => p.ContentType).NotEmpty().WithMessage("El campo {PropertyName} es requerido.");
            RuleFor(x => x.Enable).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(1).WithMessage("La longitud del campo {PropertyName} debe ser menor que {MaxLength}");
        }
    }
}
