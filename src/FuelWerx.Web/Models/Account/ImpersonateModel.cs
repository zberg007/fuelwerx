using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Account
{
	public class ImpersonateModel
	{
		public int? TenantId
		{
			get;
			set;
		}

		[Range(1, 9.22337203685478E+18)]
		public long UserId
		{
			get;
			set;
		}

		public ImpersonateModel()
		{
		}
	}
}