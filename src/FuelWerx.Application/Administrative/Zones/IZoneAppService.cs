using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Administrative.Taxes.Dto;
using FuelWerx.Administrative.Zones.Dto;
using FuelWerx.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.Zones
{
	public interface IZoneAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateZone(CreateOrUpdateZoneInput input);

		Task DeleteZone(IdInput<long> input);

		Task<ZoneListDto> GetZone(long input, long tenantId);

		Task<decimal> GetZoneEmergencyDeliveryFeesByAddressId(int tenantId, long addressId, bool active);

		Task<GetZoneForEditOutput> GetZoneForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<ZoneListDto>> GetZones(GetZonesInput input);

		Task<List<ZoneListDto>> GetZonesByAddressId(int tenantId, long addressId, bool active);

		Task<List<ZoneListDto>> GetZonesByTenantId(int tenantId, bool active);

		Task<FileDto> GetZonesToExcel();

		Task<List<TaxDto>> GetZoneTaxesByAddressId(int tenantId, long addressId, bool active);

		Task<List<string>> ValidateUserCanDelete(long zoneId);
	}
}