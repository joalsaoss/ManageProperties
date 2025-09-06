using FluentValidation;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.UpdatePropertyImage
{
    public class ValidatorCommandUpdatePropertyImage : AbstractValidator<CommandUpdatePropertyImage>
    {
        public ValidatorCommandUpdatePropertyImage()
        {
            RuleFor(x => x.Image).NotEmpty().WithMessage("El campo {PropertyName} es requerido.").
                MaximumLength(50).WithMessage("La longitud del campo {PropertyName} debe ser menor que {MaxLength}"); ;
            RuleFor(x => x.Enable).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(1).WithMessage("La longitud del campo {PropertyName} debe ser menor que {MaxLength}");
        }
    }
}
