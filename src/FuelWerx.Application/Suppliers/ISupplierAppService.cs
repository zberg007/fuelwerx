using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Dto;
using FuelWerx.Suppliers.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Suppliers
{
	public interface ISupplierAppService : IApplicationService, ITransientDependency
	{
		Task CreateOrUpdateSupplier(CreateOrUpdateSupplierInput input);

		Task DeleteSupplier(IdInput<long> input);

		Task<bool> DeleteSupplierLogo(IdInput<long> input);

		Task<GetSupplierForEditOutput> GetSupplierForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<SupplierListDto>> GetSuppliers(GetSuppliersInput input);

		Task<ListResultDto<SupplierListDto>> GetSuppliersByTenantId(int tenantId, bool active);

		Task<FileDto> GetSuppliersToExcel();

		Task SaveLogoAsync(UpdateSupplierLogoInput input);
	}
}