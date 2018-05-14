using Abp.Extensions;
using Abp.Runtime.Validation;
using Abp.Timing;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Auditing.Dto
{
	public class GetAuditLogsInput : PagedAndSortedInputDto, IShouldNormalize
	{
		public string BrowserInfo
		{
			get;
			set;
		}

		public DateTime EndDate
		{
			get;
			set;
		}

		public bool? HasException
		{
			get;
			set;
		}

		public int? MaxExecutionDuration
		{
			get;
			set;
		}

		public string MethodName
		{
			get;
			set;
		}

		public int? MinExecutionDuration
		{
			get;
			set;
		}

		public string ServiceName
		{
			get;
			set;
		}

		public DateTime StartDate
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public GetAuditLogsInput()
		{
		}

		public void Normalize()
		{
			if (base.Sorting.IsNullOrWhiteSpace())
			{
				base.Sorting = "ExecutionTime DESC";
			}
			if (base.Sorting.IndexOf("UserName", StringComparison.InvariantCultureIgnoreCase) < 0)
			{
				base.Sorting = string.Concat("AuditLog.", base.Sorting);
			}
			else
			{
				base.Sorting = string.Concat("User.", base.Sorting);
			}
			if (this.StartDate == DateTime.MinValue)
			{
				this.StartDate = Clock.Now;
			}
			this.StartDate = this.StartDate.Date;
			if (this.EndDate == DateTime.MinValue)
			{
				this.EndDate = Clock.Now;
			}
			this.EndDate = this.EndDate.AddDays(1).Date;
		}
	}
}