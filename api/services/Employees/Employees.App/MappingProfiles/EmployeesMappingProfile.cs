using AutoMapper;
using Employees.App.Dtos;
using Employees.Domain.ValueObjects;
using SeedWork.Domainn.ValueObjects;

namespace Employees.App.MappingProfiles;

sealed class EmployeesMappingProfile : Profile
{

    public EmployeesMappingProfile()
    {
        CreateMap<FullName, FullNameDto>();
        CreateMap<IEnumerable<Contact>, ContactInfoDto>()
            .ConstructUsing(contacts => new ContactInfoDto
            {
                Email = GetContact(contacts, ContactType.Email).Value,
                MobilePhoneNumber = GetContact(contacts, ContactType.Email).Value,
            });
    }

    private static Contact GetContact(IEnumerable<Contact>? contacts, ContactType contactType)
    {
        return contacts?.FirstOrDefault(x => x.Type == contactType) ?? Contact.Empty;
    }
}
