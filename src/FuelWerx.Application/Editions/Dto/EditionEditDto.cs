using Abp.Application.Editions;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Editions.Dto
{
	[AutoMap(new Type[] { typeof(Edition) })]
	public class EditionEditDto
	{
		[Required]
		public string DisplayName
		{
			get;
			set;
		}

		public int? Id
		{
			get;
			set;
		}

		public EditionEditDto()
		{
		}
	}
}