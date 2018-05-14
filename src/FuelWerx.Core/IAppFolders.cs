using System;

namespace FuelWerx
{
	public interface IAppFolders
	{
		string SampleProfileImagesFolder
		{
			get;
		}

		string TempFileDownloadFolder
		{
			get;
		}
	}
}