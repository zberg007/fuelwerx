using Abp.Auditing;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx;
using FuelWerx.Dto;
using System;
using System.IO;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public class FileController : FuelWerxControllerBase
	{
		private readonly IAppFolders _appFolders;

		public FileController(IAppFolders appFolders)
		{
			this._appFolders = appFolders;
		}

		[AbpMvcAuthorize(new string[] {  })]
		[DisableAuditing]
		public ActionResult DownloadTempFile(FileDto file)
		{
			this.CheckModelState();
			string str = Path.Combine(this._appFolders.TempFileDownloadFolder, file.FileToken);
			if (!System.IO.File.Exists(str))
			{
				throw new UserFriendlyException(this.L("RequestedFileDoesNotExists"));
			}
			byte[] numArray = System.IO.File.ReadAllBytes(str);
            System.IO.File.Delete(str);
			return this.File(numArray, file.FileType, file.FileName);
		}
	}
}