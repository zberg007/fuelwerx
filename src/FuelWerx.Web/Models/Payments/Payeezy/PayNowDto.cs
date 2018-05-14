using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Payments.Payeezy
{
	public class PayNowDto
	{
		public decimal? discount_amount
		{
			get;
			set;
		}

		[Required]
		public string PostToUrl
		{
			get;
			set;
		}

		public string x_address
		{
			get;
			set;
		}

		[Required]
		public decimal x_amount
		{
			get;
			set;
		}

		public string x_city
		{
			get;
			set;
		}

		public string x_company
		{
			get;
			set;
		}

		public string x_country
		{
			get;
			set;
		}

		[Required]
		public string x_cust_id
		{
			get;
			set;
		}

		[Required]
		public string x_customer_ip
		{
			get;
			set;
		}

		public string x_customer_tax_id
		{
			get;
			set;
		}

		public string x_description
		{
			get;
			set;
		}

		[Required]
		public string x_email
		{
			get;
			set;
		}

		public bool x_email_customer
		{
			get;
			set;
		}

		public decimal? x_fee_amount
		{
			get;
			set;
		}

		public string x_first_name
		{
			get;
			set;
		}

		[Required]
		public string x_fp_hash
		{
			get;
			set;
		}

		[Required]
		public string x_fp_sequence
		{
			get;
			set;
		}

		[Required]
		public string x_fp_timestamp
		{
			get;
			set;
		}

		public string x_gateway_id
		{
			get;
			set;
		}

		[Required]
		public string x_invoice_num
		{
			get;
			set;
		}

		public string x_last_name
		{
			get;
			set;
		}

		[Required]
		public string x_login
		{
			get;
			set;
		}

		public string x_logo_url
		{
			get;
			set;
		}

		public string x_phone
		{
			get;
			set;
		}

		[Required]
		public string x_po_num
		{
			get;
			set;
		}

		[Required]
		public string x_receipt_link_method
		{
			get;
			set;
		}

		[Required]
		public string x_receipt_link_text
		{
			get;
			set;
		}

		[Required]
		public string x_receipt_link_url
		{
			get;
			set;
		}

		[Required]
		public string x_reference_3
		{
			get;
			set;
		}

		[Required]
		public string x_show_form
		{
			get
			{
				return "PAYMENT_FORM";
			}
		}

		public string x_state
		{
			get;
			set;
		}

		public decimal? x_tax
		{
			get;
			set;
		}

		[Required]
		public bool x_test_request
		{
			get;
			set;
		}

		[Required]
		public string x_transaction_key
		{
			get;
			set;
		}

		[Required]
		public string x_type
		{
			get
			{
				return "AUTH_CAPTURE";
			}
		}

		public string x_zip
		{
			get;
			set;
		}

		public PayNowDto()
		{
		}
	}
}