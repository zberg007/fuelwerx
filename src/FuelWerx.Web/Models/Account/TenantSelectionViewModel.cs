using Abp.AutoMapper;
using FuelWerx.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Account
{
	public class TenantSelectionViewModel
	{
		public string Action
		{
			get;
			set;
		}

		public List<TenantSelectionViewModel.TenantInfo> Tenants
		{
			get;
			set;
		}

		public TenantSelectionViewModel()
		{
		}

		[AutoMapFrom(new Type[] { typeof(Tenant) })]
		public class TenantInfo
		{
			public int Id
			{
				get;
				set;
			}

			public string Name
			{
				get;
				set;
			}

			public string TenancyName
			{
				get;
				set;
			}

			public TenantInfo()
			{
			}
		}
	}
}