using Abp.Collections.Extensions;
using Abp.Dependency;
using FuelWerx;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace FuelWerx.DataExporting.Excel.EpPlus
{
	public abstract class EpPlusExcelExporterBase : FuelWerxServiceBase, ITransientDependency
	{
		public IAppFolders AppFolders
		{
			get;
			set;
		}

		protected EpPlusExcelExporterBase()
		{
		}

		protected void AddHeader(ExcelWorksheet sheet, params string[] headerTexts)
		{
			if (headerTexts.IsNullOrEmpty<string>())
			{
				return;
			}
			for (int i = 0; i < (int)headerTexts.Length; i++)
			{
				this.AddHeader(sheet, i + 1, headerTexts[i]);
			}
		}

		protected void AddHeader(ExcelWorksheet sheet, int columnIndex, string headerText)
		{
			sheet.Cells[1, columnIndex].Value = headerText;
			sheet.Cells[1, columnIndex].Style.Font.Bold = true;
		}

		protected void AddObjects<T>(ExcelWorksheet sheet, int startRowIndex, IList<T> items, params Func<T, object>[] propertySelectors)
		{
			if (items.IsNullOrEmpty<T>() || propertySelectors.IsNullOrEmpty<Func<T, object>>())
			{
				return;
			}
			for (int i = 0; i < items.Count; i++)
			{
				for (int j = 0; j < (int)propertySelectors.Length; j++)
				{
					sheet.Cells[i + startRowIndex, j + 1].Value = propertySelectors[j](items[i]);
				}
			}
		}

		protected FileDto CreateExcelPackage(string fileName, Action<ExcelPackage> creator)
		{
			FileDto fileDto = new FileDto(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
			using (ExcelPackage excelPackage = new ExcelPackage())
			{
				creator(excelPackage);
				this.Save(excelPackage, fileDto);
			}
			return fileDto;
		}

		protected void Save(ExcelPackage excelPackage, FileDto file)
		{
			string str = Path.Combine(this.AppFolders.TempFileDownloadFolder, file.FileToken);
			excelPackage.SaveAs(new FileInfo(str));
		}
	}
}