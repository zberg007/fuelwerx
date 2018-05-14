using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Administrative;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Titles.Dto
{
	[AutoMapFrom(new System.Type[] { typeof(Title) })]
	public class TitleListDto : FullAuditedEntityDto
	{
		public virtual Guid? ImageId
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual string Type
		{
			get;
			set;
		}

		public TitleListDto()
		{
		}
	}
}