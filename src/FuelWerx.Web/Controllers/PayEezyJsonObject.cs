using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Controllers
{
	internal class PayEezyJsonObject
	{
		public int Id
		{
			get;
			set;
		}

		public string PayEezy_x_description
		{
			get;
			set;
		}

		public bool? PayEezy_x_email_customer
		{
			get;
			set;
		}

		public string PayEezy_x_gateway_id
		{
			get;
			set;
		}

		public string PayEezy_x_login
		{
			get;
			set;
		}

		public bool? PayEezy_x_test_request
		{
			get;
			set;
		}

		public string PayEezy_x_transaction_key
		{
			get;
			set;
		}

		public int? TenantId
		{
			get;
			set;
		}

		public PayEezyJsonObject()
		{
		}
	}
}