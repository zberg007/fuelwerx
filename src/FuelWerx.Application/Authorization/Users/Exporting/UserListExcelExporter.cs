using Abp;
using Abp.Collections.Extensions;
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Exporting
{
	public class UserListExcelExporter : EpPlusExcelExporterBase, IUserListExcelExporter
	{
		public UserListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<UserListDto> userListDtos)
		{
			return base.CreateExcelPackage("UserList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Users"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("Name"), this.L("Surname"), this.L("UserName"), this.L("EmailAddress"), this.L("EmailConfirm"), this.L("Roles"), this.L("LastLoginTime"), this.L("Active"), this.L("CreationTime") });

				AddObjects(excelWorksheet, 2, userListDtos, new Func<UserListDto, object>[] {
						l => l.Name,
						l => l.Surname,
						l => l.UserName,
						l => l.EmailAddress,
						l => l.IsEmailConfirmed,
						l => (
							from r in l.Roles
							select r.RoleName).JoinAsString(", "),
						l => l.LastLoginTime,
						l => l.IsActive,
						l => l.CreationTime
                    });
				excelWorksheet.Column(7).Style.Numberformat.Format = "mm-dd-yy";
				excelWorksheet.Column(9).Style.Numberformat.Format = "mm-dd-yy";
				for (int i = 1; i <= 7; i++)
				{
					excelWorksheet.Column(i).AutoFit();
				}
			});
		}
	}
}