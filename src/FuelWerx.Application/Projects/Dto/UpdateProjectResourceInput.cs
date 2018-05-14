using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	public class UpdateProjectResourceInput
	{
		public string FileExtension
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}

		public string FileSize
		{
			get;
			set;
		}

		public long Id
		{
			get;
			set;
		}

		public bool IsActive
		{
			get;
			set;
		}

		public long ProjectId
		{
			get;
			set;
		}

		public Guid? ResourceId
		{
			get;
			set;
		}

		public UpdateProjectResourceInput()
		{
		}
	}
}