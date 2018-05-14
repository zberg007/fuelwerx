using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using FuelWerx;
using FuelWerx.Dto;
using FuelWerx.Generic;
using FuelWerx.Storage;
using FuelWerx.Suppliers.Dto;
using FuelWerx.Suppliers.Exporting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Suppliers
{
	[AbpAuthorize(new string[] { "Pages.Tenant.Suppliers" })]
	public class SupplierAppService : FuelWerxAppServiceBase, ISupplierAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Supplier, long> _supplierRepository;

		private readonly IBinaryObjectManager _binaryObjectManager;

		private readonly ISupplierListExcelExporter _supplierListExcelExporter;

		public SupplierAppService(IRepository<Supplier, long> supplierRepository, IBinaryObjectManager binaryObjectManager, ISupplierListExcelExporter supplierListExcelExporter)
		{
			this._supplierRepository = supplierRepository;
			this._binaryObjectManager = binaryObjectManager;
			this._supplierListExcelExporter = supplierListExcelExporter;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Suppliers.Create", "Pages.Tenant.Suppliers.Edit" })]
		public async Task CreateOrUpdateSupplier(CreateOrUpdateSupplierInput input)
		{
			if (!input.Supplier.Id.HasValue)
			{
				await this._supplierRepository.InsertAsync(input.Supplier.MapTo<Supplier>());
			}
			else
			{
				await this._supplierRepository.UpdateAsync(input.Supplier.MapTo<Supplier>());
			}
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Suppliers.Delete" })]
		public async Task DeleteSupplier(IdInput<long> input)
		{
			await this.DeleteSupplierLogo(input);
			await this._supplierRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Suppliers.Delete" })]
		public async Task<bool> DeleteSupplierLogo(IdInput<long> input)
		{
			Guid guid;
			Supplier async = await this._supplierRepository.GetAsync(input.Id);
			Guid? logoId = async.LogoId;
			if (logoId.HasValue)
			{
				logoId = async.LogoId;
				if (Guid.TryParse(logoId.ToString(), out guid))
				{
					await this._binaryObjectManager.DeleteAsync(guid);
					logoId = null;
					async.LogoId = logoId;
				}
			}
			return true;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Suppliers.Create", "Pages.Tenant.Suppliers.Edit" })]
		public async Task<GetSupplierForEditOutput> GetSupplierForEdit(NullableIdInput<long> input)
		{
			SupplierEditDto supplierEditDto;
			if (!input.Id.HasValue)
			{
				supplierEditDto = new SupplierEditDto();
			}
			else
			{
				IRepository<Supplier, long> repository = this._supplierRepository;
				Supplier async = await repository.GetAsync(input.Id.Value);
				supplierEditDto = async.MapTo<SupplierEditDto>();
			}
			return new GetSupplierForEditOutput()
			{
				Supplier = supplierEditDto
			};
		}

		public async Task<PagedResultOutput<SupplierListDto>> GetSuppliers(GetSuppliersInput input)
		{
			IQueryable<Supplier> all = this._supplierRepository.GetAll();
			IQueryable<Supplier> suppliers = all.WhereIf<Supplier>(!input.Filter.IsNullOrEmpty(), (Supplier p) => p.Name.Contains(input.Filter) || p.Description.Contains(input.Filter) || p.PhoneNumber.Contains(input.Filter) || p.MobilePhoneNumber.Contains(input.Filter) || p.Address.Contains(input.Filter) || p.SecondaryAddress.Contains(input.Filter) || p.City.Contains(input.Filter) || p.Country.Code.Contains(input.Filter) || p.Country.Name.Contains(input.Filter) || p.CountryRegion.Code.Contains(input.Filter) || p.CountryRegion.Name.Contains(input.Filter) || p.PostalCode.Contains(input.Filter) || p.ContactName.Contains(input.Filter) || p.ContactEmail.Contains(input.Filter));
			int num = await suppliers.CountAsync<Supplier>();
			List<Supplier> listAsync = await suppliers.OrderBy<Supplier>(input.Sorting, new object[0]).PageBy<Supplier>(input).ToListAsync<Supplier>();
			return new PagedResultOutput<SupplierListDto>(num, listAsync.MapTo<List<SupplierListDto>>());
		}

		public async Task<ListResultDto<SupplierListDto>> GetSuppliersByTenantId(int tenantId, bool active)
		{
			IQueryable<Supplier> all = this._supplierRepository.GetAll();
			IQueryable<Supplier> suppliers = 
				from p in all
				where p.TenantId == tenantId
				select p;
			bool flag = active;
			List<Supplier> listAsync = await suppliers.WhereIf<Supplier>(flag, (Supplier p) => p.IsActive == active).OrderBy<Supplier>("Name", new object[0]).ToListAsync<Supplier>();
			return new ListResultDto<SupplierListDto>(listAsync.MapTo<List<SupplierListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Suppliers.ExportData" })]
		public async Task<FileDto> GetSuppliersToExcel()
		{
			IQueryable<Supplier> all = this._supplierRepository.GetAll();
			IQueryable<Supplier> suppliers = System.Data.Entity.QueryableExtensions.Include<Supplier, Country>(all, (Supplier c) => c.Country);
			List<Supplier> listAsync = await System.Data.Entity.QueryableExtensions.Include<Supplier, CountryRegion>(suppliers, (Supplier cr) => cr.CountryRegion).ToListAsync<Supplier>();
			List<SupplierListDto> supplierListDtos = listAsync.MapTo<List<SupplierListDto>>();
			return this._supplierListExcelExporter.ExportToFile(supplierListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Suppliers.Create", "Pages.Tenant.Suppliers.Edit" })]
		public async Task SaveLogoAsync(UpdateSupplierLogoInput input)
		{
			Supplier async = await this._supplierRepository.GetAsync(input.SupplierId);
			Guid? logoId = input.LogoId;
			if (!logoId.HasValue)
			{
				logoId = null;
				async.LogoId = logoId;
			}
			else
			{
				logoId = input.LogoId;
				async.LogoId = new Guid?(logoId.Value);
			}
			await this._supplierRepository.UpdateAsync(async);
		}
	}
}