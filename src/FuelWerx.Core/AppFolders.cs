using Abp.Dependency;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx
{
	public class AppFolders : IAppFolders, ISingletonDependency
	{
		public string SampleProfileImagesFolder
		{
			get
			{
				return JustDecompileGenerated_get_SampleProfileImagesFolder();
			}
			set
			{
				JustDecompileGenerated_set_SampleProfileImagesFolder(value);
			}
		}

		private string JustDecompileGenerated_SampleProfileImagesFolder_k__BackingField;

		public string JustDecompileGenerated_get_SampleProfileImagesFolder()
		{
			return this.JustDecompileGenerated_SampleProfileImagesFolder_k__BackingField;
		}

		public void JustDecompileGenerated_set_SampleProfileImagesFolder(string value)
		{
			this.JustDecompileGenerated_SampleProfileImagesFolder_k__BackingField = value;
		}

		public string TempFileDownloadFolder
		{
			get
			{
				return JustDecompileGenerated_get_TempFileDownloadFolder();
			}
			set
			{
				JustDecompileGenerated_set_TempFileDownloadFolder(value);
			}
		}

		private string JustDecompileGenerated_TempFileDownloadFolder_k__BackingField;

		public string JustDecompileGenerated_get_TempFileDownloadFolder()
		{
			return this.JustDecompileGenerated_TempFileDownloadFolder_k__BackingField;
		}

		public void JustDecompileGenerated_set_TempFileDownloadFolder(string value)
		{
			this.JustDecompileGenerated_TempFileDownloadFolder_k__BackingField = value;
		}

		public AppFolders()
		{
		}
	}
}