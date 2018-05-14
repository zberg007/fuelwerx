using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Common.Dto;
using System.Threading.Tasks;

namespace FuelWerx.Common
{
	public interface ICommonLookupAppService : IApplicationService, ITransientDependency
	{
		Task<PagedResultOutput<NameValueDto>> FindUsers(FindUsersInput input);

		Task<ListResultOutput<ComboboxItemDto>> GetEditionsForCombobox();
	}
}