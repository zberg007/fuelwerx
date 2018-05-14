using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Authorization.Roles.Dto;
using System.Threading.Tasks;

namespace FuelWerx.Authorization.Roles
{
	public interface IRoleAppService : IApplicationService, ITransientDependency
	{
		Task CreateOrUpdateRole(CreateOrUpdateRoleInput input);

		Task DeleteRole(EntityRequestInput input);

		Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdInput input);

		Task<ListResultOutput<RoleListDto>> GetRoles();
	}
}