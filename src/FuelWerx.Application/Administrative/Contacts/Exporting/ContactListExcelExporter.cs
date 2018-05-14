using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Administrative.Contacts.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Contacts.Exporting
{
    public class ContactListExcelExporter : EpPlusExcelExporterBase, IContactListExcelExporter
    {
        public ContactListExcelExporter()
        {
        }

        public FileDto ExportToFile(List<ContactListDto> contactListDtos)
        {
            return base.CreateExcelPackage("ContactList.xlsx", (ExcelPackage excelPackage) =>
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Contacts"));
                excelWorksheet.OutLineApplyStyle = true;
                base.AddHeader(excelWorksheet, new string[] { this.L("ContactIdentifier"), this.L("ContactTitle"), this.L("Email"), this.L("Description"), this.L("Active"), this.L("CreationTime") });
                AddObjects(excelWorksheet, 2, contactListDtos, new Func<ContactListDto, object>[] {
                        l => l.Id,
                        l => l.Title,
                        l => l.Email,
                        l => l.Description,
                        l => l.IsActive,
                        l => l.CreationTime
                    });
                excelWorksheet.Column(6).Style.Numberformat.Format = "mm-dd-yy";
                for (int i = 1; i <= 4; i++)
                {
                    excelWorksheet.Column(i).AutoFit();
                }
            });
        }
    }
}