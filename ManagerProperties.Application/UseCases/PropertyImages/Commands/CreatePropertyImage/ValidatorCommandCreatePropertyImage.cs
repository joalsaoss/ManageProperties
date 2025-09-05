using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.CreatePropertyImage
{
    public class ValidatorCommandCreatePropertyImage:AbstractValidator<CommandCreatePropertyImage>
    {
        public ValidatorCommandCreatePropertyImage()
        {
            RuleFor(x => x.PropertyId).NotEmpty().WithMessage("El campo {PropertyName} es requerido.");
            RuleFor(x => x.Image).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(50).WithMessage("La longitud del campo {PropertyName} debe ser menor que {MaxLength}");
            RuleFor(x => x.Enable).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(1).WithMessage("La longitud del campo {PropertyName} debe ser menor que {MaxLength}");
        }
    }
}
