using FluentValidation;
using Intercorp.Clientes.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intercorp.Clientes.Api.Validators
{
    public class NewClientValidator : AbstractValidator<ClientDto>
    {
        public NewClientValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty();
            RuleFor(x => x.Apellido).NotEmpty();
        }
    }
}
