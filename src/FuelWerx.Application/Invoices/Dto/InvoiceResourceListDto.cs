using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Invoices;
using FuelWerx.Storage;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapFrom(new Type[] { typeof(InvoiceResource) })]
	public class InvoiceResourceListDto : FullAuditedEntityDto
	{
		[ForeignKey("BinaryObjectId")]
		public virtual FuelWerx.Storage.BinaryObject BinaryObject
		{
			get;
			set;
		}

		public virtual Guid BinaryObjectId
		{
			get;
			set;
		}

		public virtual string Category
		{
			get;
			set;
		}

		public virtual string Description
		{
			get;
			set;
		}

		public virtual string FileExtension
		{
			get;
			set;
		}

		public virtual string FileName
		{
			get;
			set;
		}

		public virtual string FileSize
		{
			get;
			set;
		}

		[ForeignKey("InvoiceId")]
		public virtual FuelWerx.Invoices.Invoice Invoice
		{
			get;
			set;
		}

		public virtual long InvoiceId
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

		public InvoiceResourceListDto()
		{
		}
	}
}