using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Authorization.Users
{
	public interface IUserAppService : IApplicationService, ITransientDependency
	{
		Task CreateOrUpdateUser(CreateOrUpdateUserInput input);

		Task DeleteUser(IdInput<long> input);

		Task<GetUserForEditOutput> GetUserForEdit(NullableIdInput<long> input);

		Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(IdInput<long> input);

		Task<string> GetUserPostLoginViewType(long userId);

		Task<PagedResultOutput<UserListDto>> GetUsers(GetUsersInput input);

		Task<FileDto> GetUsersToExcel();

		Task ResetUserSpecificPermissions(IdInput<long> input);

		Task<bool> ShowScreencastAtLogin(long userId);

		Task UpdateUserPermissions(UpdateUserPermissionsInput input);
	}
}