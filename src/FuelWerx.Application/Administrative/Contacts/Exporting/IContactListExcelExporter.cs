using FuelWerx.Administrative.Contacts.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Administrative.Contacts.Exporting
{
	public interface IContactListExcelExporter
	{
		FileDto ExportToFile(List<ContactListDto> contactListDtos);
	}
}