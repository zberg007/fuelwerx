using Abp;
using Abp.Extensions;
using FuelWerx.Auditing.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Auditing.Exporting
{
    public class AuditLogListExcelExporter : EpPlusExcelExporterBase, IAuditLogListExcelExporter
    {
        public AuditLogListExcelExporter()
        {
        }

        public FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos)
        {
            return base.CreateExcelPackage("AuditLogs.xlsx", (ExcelPackage excelPackage) =>
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("AuditLogs"));
                excelWorksheet.OutLineApplyStyle = true;
                base.AddHeader(excelWorksheet, new string[] { this.L("Time"), this.L("UserName"), this.L("Service"), this.L("Action"), this.L("Parameters"), this.L("Duration"), this.L("IpAddress"), this.L("Client"), this.L("Browser"), this.L("ErrorState") });

                AddObjects(excelWorksheet, 2, auditLogListDtos, new Func<AuditLogListDto, object>[] {
                        l => l.ExecutionTime,
                        l => l.UserName,
                        l => l.ServiceName,
                        l => l.MethodName,
                        l => l.Parameters,
                        l => l.ExecutionDuration,
                        l => l.ClientIpAddress,
                        l => l.ClientName,
                        l => l.BrowserInfo,
                        l => {
                            if(!l.Exception.IsNullOrEmpty())
                                return l.Exception;
                            else
                                return L("Success");
                        }
                    });
                excelWorksheet.Column(1).Style.Numberformat.Format = "mm-dd-yy hh:mm:ss";
                for (int i = 1; i <= 10; i++)
                {
                    if (!i.IsIn<int>(new int[] { 5, 10 }))
                    {
                        excelWorksheet.Column(i).AutoFit();
                    }
                }
            });
        }
    }
}