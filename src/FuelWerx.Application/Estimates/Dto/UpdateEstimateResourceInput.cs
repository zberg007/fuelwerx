using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	public class UpdateEstimateResourceInput
	{
		public long EstimateId
		{
			get;
			set;
		}

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

		public Guid? ResourceId
		{
			get;
			set;
		}

		public UpdateEstimateResourceInput()
		{
		}
	}
}