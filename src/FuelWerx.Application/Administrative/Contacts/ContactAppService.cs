using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using FuelWerx;
using FuelWerx.Administrative;
using FuelWerx.Administrative.Contacts.Dto;
using FuelWerx.Administrative.Contacts.Exporting;
using FuelWerx.Dto;
using FuelWerx.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.Contacts
{
	[AbpAuthorize(new string[] { "Pages.Administration.Contacts" })]
	public class ContactAppService : FuelWerxAppServiceBase, IContactAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Contact, long> _contactRepository;

		private readonly IContactListExcelExporter _contactListExcelExporter;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public ContactAppService(IRepository<Contact, long> contactRepository, IContactListExcelExporter contactListExcelExporter, IBinaryObjectManager binaryObjectManager)
		{
			this._contactRepository = contactRepository;
			this._contactListExcelExporter = contactListExcelExporter;
			this._binaryObjectManager = binaryObjectManager;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Contacts.Create", "Pages.Administration.Contacts.Edit" })]
		public async Task<long> CreateOrUpdateContact(CreateOrUpdateContactInput input)
		{
			long value;
			if (!input.Contact.Id.HasValue)
			{
				long num = await this._contactRepository.InsertAndGetIdAsync(input.Contact.MapTo<Contact>());
				value = num;
			}
			else
			{
				value = input.Contact.Id.Value;
				await this._contactRepository.UpdateAsync(input.Contact.MapTo<Contact>());
			}
			return value;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Contacts.Delete" })]
		public async Task DeleteContact(IdInput<long> input)
		{
			await this.DeleteContactImage(input);
			await this._contactRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Contacts.Delete" })]
		private async Task<bool> DeleteContactImage(IdInput<long> input)
		{
			Guid guid;
			Contact async = await this._contactRepository.GetAsync(input.Id);
			Guid? imageId = async.ImageId;
			if (imageId.HasValue)
			{
				imageId = async.ImageId;
				if (Guid.TryParse(imageId.ToString(), out guid))
				{
					await this._binaryObjectManager.DeleteAsync(guid);
					imageId = null;
					async.ImageId = imageId;
				}
			}
			return true;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Contacts.Edit" })]
		public async Task<GetContactForEditOutput> GetContactForEdit(NullableIdInput<long> input)
		{
			ContactEditDto contactEditDto;
			if (!input.Id.HasValue)
			{
				contactEditDto = new ContactEditDto();
			}
			else
			{
				IRepository<Contact, long> repository = this._contactRepository;
				Contact async = await repository.GetAsync(input.Id.Value);
				contactEditDto = async.MapTo<ContactEditDto>();
			}
			return new GetContactForEditOutput()
			{
				Contact = contactEditDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Contacts" })]
		public async Task<PagedResultOutput<ContactListDto>> GetContacts(GetContactsInput input)
		{
			IQueryable<Contact> all = this._contactRepository.GetAll();
			IQueryable<Contact> contacts = all.WhereIf<Contact>(!input.Filter.IsNullOrEmpty(), (Contact p) => p.Title.Contains(input.Filter) || p.Description.Contains(input.Filter) || p.Email.Contains(input.Filter));
			int num = await contacts.CountAsync<Contact>();
			List<Contact> listAsync = await contacts.OrderBy<Contact>(input.Sorting, new object[0]).PageBy<Contact>(input).ToListAsync<Contact>();
			return new PagedResultOutput<ContactListDto>(num, listAsync.MapTo<List<ContactListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Contacts.ExportData" })]
		public async Task<FileDto> GetContactsToExcel()
		{
			List<Contact> allListAsync = await this._contactRepository.GetAllListAsync();
			List<ContactListDto> contactListDtos = allListAsync.MapTo<List<ContactListDto>>();
			return this._contactListExcelExporter.ExportToFile(contactListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Contacts.Create", "Pages.Administration.Contacts.Edit" })]
		public async Task SaveContactImageAsync(UpdateContactImageInput input)
		{
			Contact async = await this._contactRepository.GetAsync(input.ContactId);
			Guid? imageId = input.ImageId;
			if (!imageId.HasValue)
			{
				imageId = null;
				async.ImageId = imageId;
			}
			else
			{
				imageId = input.ImageId;
				async.ImageId = new Guid?(imageId.Value);
			}
			await this._contactRepository.UpdateAsync(async);
		}
	}
}