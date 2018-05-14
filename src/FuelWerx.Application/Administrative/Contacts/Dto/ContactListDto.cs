using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Administrative;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Contacts.Dto
{
	[AutoMapFrom(new Type[] { typeof(Contact) })]
	public class ContactListDto : FullAuditedEntityDto
	{
		public virtual string Description
		{
			get;
			set;
		}

		public virtual string Email
		{
			get;
			set;
		}

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

		public virtual string Title
		{
			get;
			set;
		}

		public ContactListDto()
		{
		}
	}
}