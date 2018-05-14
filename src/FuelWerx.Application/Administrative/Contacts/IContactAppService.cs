using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Administrative.Contacts.Dto;
using FuelWerx.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.Contacts
{
	public interface IContactAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateContact(CreateOrUpdateContactInput input);

		Task DeleteContact(IdInput<long> input);

		Task<GetContactForEditOutput> GetContactForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<ContactListDto>> GetContacts(GetContactsInput input);

		Task<FileDto> GetContactsToExcel();

		Task SaveContactImageAsync(UpdateContactImageInput updateContactImageInput);
	}
}