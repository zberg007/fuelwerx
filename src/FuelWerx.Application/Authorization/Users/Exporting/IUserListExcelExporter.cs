using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Authorization.Users.Exporting
{
	public interface IUserListExcelExporter
	{
		FileDto ExportToFile(List<UserListDto> userListDtos);
	}
}