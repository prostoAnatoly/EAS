using FluentValidation;

namespace Organizations.App.Features;

sealed class GetOrganizationsValidator : AbstractValidator<GetOrganizationsQuery>
{

    public GetOrganizationsValidator()
    {

    }
}