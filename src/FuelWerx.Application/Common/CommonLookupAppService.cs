using Abp;
using Abp.Application.Editions;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using FuelWerx;
using FuelWerx.Authorization.Users;
using FuelWerx.Common.Dto;
using FuelWerx.Dto;
using FuelWerx.Editions;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Common
{
	[AbpAuthorize(new string[] {  })]
	public class CommonLookupAppService : FuelWerxAppServiceBase, ICommonLookupAppService, IApplicationService, ITransientDependency
	{
		private readonly EditionManager _editionManager;

		public CommonLookupAppService(EditionManager editionManager)
		{
			this._editionManager = editionManager;
		}

		public async Task<PagedResultOutput<NameValueDto>> FindUsers(FindUsersInput input)
		{
			if (this.AbpSession.MultiTenancySide == MultiTenancySides.Host && input.TenantId.HasValue)
			{
				IActiveUnitOfWork currentUnitOfWork = this.CurrentUnitOfWork;
				int? tenantId = input.TenantId;
				currentUnitOfWork.SetFilterParameter("MayHaveTenant", "tenantId", tenantId.Value);
			}
			IQueryable<User> users = this.UserManager.Users;
			IQueryable<User> users1 = users.WhereIf<User>(!input.Filter.IsNullOrWhiteSpace(), (User u) => u.Name.Contains(input.Filter) || u.Surname.Contains(input.Filter) || u.UserName.Contains(input.Filter) || u.EmailAddress.Contains(input.Filter));
			int num = await users1.CountAsync<User>();
			IQueryable<User> users2 = users1;
			IOrderedQueryable<User> name = 
				from u in users2
				orderby u.Name
				select u;
			List<User> listAsync = await name.ThenBy<User, string>((User u) => u.Surname).PageBy<User>(input).ToListAsync<User>();
			List<User> users3 = listAsync;
			int num1 = num;
			List<User> users4 = users3;
			PagedResultOutput<NameValueDto> pagedResultOutput = new PagedResultOutput<NameValueDto>(num1, (
				from u in users4
				select new NameValueDto(string.Concat(new string[] { u.Name, " ", u.Surname, " (", u.EmailAddress, ")" }), u.Id.ToString())).ToList<NameValueDto>());
			return pagedResultOutput;
		}

		public async Task<ListResultOutput<ComboboxItemDto>> GetEditionsForCombobox()
		{
			List<Edition> listAsync = await this._editionManager.Editions.ToListAsync<Edition>();
			ListResultOutput<ComboboxItemDto> listResultOutput = new ListResultOutput<ComboboxItemDto>((
				from e in listAsync
				select new ComboboxItemDto(e.Id.ToString(), e.DisplayName)).ToList<ComboboxItemDto>());
			return listResultOutput;
		}
	}
}