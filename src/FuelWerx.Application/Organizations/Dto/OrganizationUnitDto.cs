using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Organizations.Dto
{
	[AutoMapFrom(new Type[] { typeof(OrganizationUnit) })]
	public class OrganizationUnitDto : AuditedEntityDto<long>
	{
		public string Code
		{
			get;
			set;
		}

		public string DisplayName
		{
			get;
			set;
		}

		public int MemberCount
		{
			get;
			set;
		}

		public long? ParentId
		{
			get;
			set;
		}

		public OrganizationUnitDto()
		{
		}
	}
}