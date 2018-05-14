using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Organizations.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelWerx.Organizations
{
	public interface IOrganizationUnitAppService : IApplicationService, ITransientDependency
	{
		Task AddUserToOrganizationUnit(UserToOrganizationUnitInput input);

		Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input);

		Task DeleteOrganizationUnit(IdInput<long> input);

		Task<List<string>> GetDiscountForUsersOrganizationUnits(long customerId);

		Task<OrganizationUnitPropertiesDto> GetOrganizationUnitProperties(long organizationUnitId);

		Task<ListResultOutput<OrganizationUnitDto>> GetOrganizationUnits();

		Task<List<OrganizationUnitDto>> GetOrganizationUnitsByProperty(string propertyName, string propertyValue);

		Task<List<OrganizationUnitDto>> GetOrganizationUnitsForUser(long userId);

		Task<PagedResultOutput<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input);

		Task<List<string>> GetUpchargeForUsersOrganizationUnits(long customerId);

		Task<bool> IsInOrganizationUnit(UserToOrganizationUnitInput input);

		bool IsTenantOrganizationUnit();

		Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input);

		Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input);

		Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input);

		Task<OrganizationUnitPropertiesDto> UpdateOrganizationUnitProperties(UpdateOrganizationUnitPropertiesInput input);
	}
}