using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Administrative;
using FuelWerx.Administrative.Taxes.Dto;
using FuelWerx.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.Taxes
{
	public interface ITaxAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateTax(CreateOrUpdateTaxInput input);

		Task DeleteTax(IdInput<long> input);

		Task<PagedResultOutput<TaxListDto>> GetTaxes(GetTaxesInput input);

		Task<List<Tax>> GetTaxesForTaxRules();

		Task<FileDto> GetTaxesToExcel();

		Task<GetTaxForEditOutput> GetTaxForEdit(NullableIdInput<long> input);
	}
}