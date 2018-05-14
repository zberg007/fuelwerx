using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Auditing.Dto
{
	[AutoMapFrom(new Type[] { typeof(AuditLog) })]
	public class AuditLogListDto : EntityDto<long>
	{
		public string BrowserInfo
		{
			get;
			set;
		}

		public string ClientIpAddress
		{
			get;
			set;
		}

		public string ClientName
		{
			get;
			set;
		}

		public string CustomData
		{
			get;
			set;
		}

		public string Exception
		{
			get;
			set;
		}

		public int ExecutionDuration
		{
			get;
			set;
		}

		public DateTime ExecutionTime
		{
			get;
			set;
		}

		public int? ImpersonatorTenantId
		{
			get;
			set;
		}

		public long? ImpersonatorUserId
		{
			get;
			set;
		}

		public string MethodName
		{
			get;
			set;
		}

		public string Parameters
		{
			get;
			set;
		}

		public string ServiceName
		{
			get;
			set;
		}

		public long? UserId
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public AuditLogListDto()
		{
		}
	}
}