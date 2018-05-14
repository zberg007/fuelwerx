using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using FuelWerx.Generic;
using FuelWerx.Suppliers.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Suppliers.Exporting
{
    public class SupplierListExcelExporter : EpPlusExcelExporterBase, ISupplierListExcelExporter
    {
        public SupplierListExcelExporter()
        {
        }

        public FileDto ExportToFile(List<SupplierListDto> supplierListDtos)
        {
            return base.CreateExcelPackage("SupplierList.xlsx", (ExcelPackage excelPackage) =>
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Suppliers"));
                excelWorksheet.OutLineApplyStyle = true;
                base.AddHeader(excelWorksheet, new string[] { this.L("SupplierIdentifier"), this.L("SupplierName"), this.L("PhoneNumber"), this.L("MobileNumber"), this.L("Address"), this.L("SecondaryAddress"), this.L("City"), this.L("PostalCode"), this.L("CountryRegion"), this.L("Country"), this.L("ContactName"), this.L("ContactEmail"), this.L("Description"), this.L("Active"), this.L("CreationTime") });

                AddObjects(excelWorksheet, 2, supplierListDtos, new Func<SupplierListDto, object>[] {
                        l => l.Id,
                        l => l.Name,
                        l => l.PhoneNumber,
                        l => l.MobilePhoneNumber,
                        l => l.Address,
                        l => l.SecondaryAddress,
                        l => l.City,
                        l => l.PostalCode,
                        l => {
                            if (l.CountryRegion == null)
                            {
                                return string.Empty;
                            }
                            return l.CountryRegion.Code;
                        },
                        l => {
                            if (l.Country == null)
                            {
                                return string.Empty;
                            }
                            return l.Country.Name;
                        },
                        l => l.ContactName,
                        l => l.ContactEmail,
                        l => l.Description,
                        l => l.IsActive,
                        l => l.CreationTime
                    });
                excelWorksheet.Column(14).Style.Numberformat.Format = "mm-dd-yy";
                for (int i = 1; i <= 12; i++)
                {
                    excelWorksheet.Column(i).AutoFit();
                }
            });
        }
    }
}