using Abp.Application.Services;
using Abp.Dependency;
using FuelWerx.Authorization.Users.Profile.Dto;
using System.Threading.Tasks;

namespace FuelWerx.Authorization.Users.Profile
{
	public interface IProfileAppService : IApplicationService, ITransientDependency
	{
		Task ChangePassword(ChangePasswordInput input);

		Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit();

		Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input);
	}
}