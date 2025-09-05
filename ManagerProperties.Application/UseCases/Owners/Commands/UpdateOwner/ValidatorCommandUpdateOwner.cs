using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.Owners.Commands.UpdateOwner
{
    public class ValidatorCommandUpdateOwner: AbstractValidator<CommandUpdateOwner>
    {
        public ValidatorCommandUpdateOwner()
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
