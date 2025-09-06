using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Commands.CreatePropertyTrace
{
    public class ValidatorCommandCreatePropertyTrace : AbstractValidator<CommandCreatePropertyTrace>
    {
        public ValidatorCommandCreatePropertyTrace()
        {
            RuleFor(x => x.PropertyId).NotEmpty().WithMessage("El campo {PropertyName} es requerido.");
            RuleFor(p => p.DateSale).NotEmpty().WithMessage("El campo {PropertyName} es requerido.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .MaximumLength(60).WithMessage("La longitud del campo {PropertyName} debe ser menor que {MaxLength}");
            RuleFor(x => x.Value).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .GreaterThan(0).WithMessage("El valor del campo {PropertyName} debe ser mayor que cero.");
            RuleFor(x => x.Tax).NotEmpty().WithMessage("El campo {PropertyName} es requerido.")
                .GreaterThan(0).WithMessage("El valor del campo {PropertyName} debe ser mayor que cero.");
        }
    }
}
