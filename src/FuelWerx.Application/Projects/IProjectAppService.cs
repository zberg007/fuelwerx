using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Dto;
using FuelWerx.Projects.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Projects
{
	public interface IProjectAppService : IApplicationService, ITransientDependency
	{
		Task ConvertToInvoice(IdInput<long> input);

		Task<long> CreateOrUpdateProject(CreateOrUpdateProjectInput input);

		Task CreateOrUpdateProjectTeamMembers(CreateOrUpdateProjectTeamMemberInput input);

		Task DeleteProject(IdInput<long> input);

		Task DeleteProjectResource(IdInput<long> input);

		Task<Project> GetProject(long projectId);

		Task<GetProjectForEditOutput> GetProjectForEdit(NullableIdInput<long> input);

		Task<ProjectResourceEditDto> GetProjectResourceDetailsByBinaryObjectId(Guid resourceId);

		Task<GetProjectResourceForEditOutput> GetProjectResourcesForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<ProjectListDto>> GetProjects(GetProjectsInput input);

		Task<FileDto> GetProjectsToExcel();

		Task<GetProjectTeamMembersForEditOutput> GetProjectTeamMembersForEdit(NullableIdInput<long> input);

		Task<ListResultDto<UserListDto>> GetTeamMembersByTenantId(int tenantId, bool active);

		Task SaveProjectResourceAsync(UpdateProjectResourceInput updateProjectResourceInput);

		Task SaveProjectResourceDetails(long projectResourceId, string name, string description, bool isActive);
	}
}