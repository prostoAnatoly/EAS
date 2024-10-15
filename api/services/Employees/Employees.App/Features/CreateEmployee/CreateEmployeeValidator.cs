using FluentValidation;

namespace Employees.App.Features;

sealed class CreateEmployeeValidatorValidator : AbstractValidator<CreateEmployeeCommand>
{

    public CreateEmployeeValidatorValidator()
    {

    }
}