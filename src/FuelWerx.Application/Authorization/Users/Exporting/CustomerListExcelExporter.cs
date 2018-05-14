using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Customers.Dto;
using FuelWerx.Customers.Exporting;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Exporting
{
	public class CustomerListExcelExporter : EpPlusExcelExporterBase, ICustomerListExcelExporter
	{
		public CustomerListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<CustomerListDto> customerListDtos)
		{
			return base.CreateExcelPackage("CustomerList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Customers"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("CustomerIdentifier"), this.L("FirstName"), this.L("LastName"), this.L("FullName"), this.L("BusinessName"), this.L("Email"), this.L("AllowBillPay"), this.L("Active"), this.L("CreationTime") });
				AddObjects<CustomerListDto>(excelWorksheet, 2, customerListDtos, new Func<CustomerListDto, object>[] {
						l => l.Id,
						l => l.FirstName,
						l => l.LastName,
						l => l.FullName,
						l => l.BusinessName,
						l => l.Email,
						l => l.AllowBillPay,
						l => l.IsActive,
						l => l.CreationTime
                    });
				excelWorksheet.Column(9).Style.Numberformat.Format = "mm-dd-yy";
				for (int i = 1; i <= 7; i++)
				{
					excelWorksheet.Column(i).AutoFit();
				}
			});
		}

		public FileDto ExportToFile(List<CustomerWithAddressListDto> customerWithAddressListDtos)
		{
			return base.CreateExcelPackage("CustomerWithAddressesList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("CustomersWithAddresses"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("CustomerIdentifier"), this.L("FirstName"), this.L("LastName"), this.L("FullName"), this.L("BusinessName"), this.L("Email"), this.L("AllowBillPay"), this.L("Active"), this.L("CreationTime"), this.L("AddressIdentifier"), this.L("AddressType"), this.L("PrimaryAddress"), this.L("SecondaryAddress"), this.L("City"), this.L("CountryRegionCode"), this.L("PostalCode"), this.L("CountryCode"), this.L("Longitude"), this.L("Latitude"), this.L("ContactName"), this.L("Active") });
				AddObjects(excelWorksheet, 2, customerWithAddressListDtos, new Func<CustomerWithAddressListDto, object>[] {
						l => l.CustomerId,
						l => l.FirstName,
						l => l.LastName,
						l => l.FullName,
						l => l.BusinessName,
						l => l.Email,
						l => l.AllowBillPay,
						l => l.IsActive,
						l => l.CreationTime,
						l => l.AddressId,
						l => l.Type,
						l => l.PrimaryAddress,
						l => l.SecondaryAddress,
						l => l.City,
						l => l.CountryRegionCode,
						l => l.PostalCode,
						l => l.CountryCode,
						l => l.Longitude,
						l => l.Latitude,
						l => l.ContactName,
						l => l.AddressIsActive
                    });
				excelWorksheet.Column(9).Style.Numberformat.Format = "mm-dd-yy";
				for (int i = 1; i <= 21; i++)
				{
					excelWorksheet.Column(i).AutoFit();
				}
			});
		}
	}
}