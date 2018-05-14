using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	public class LookupFill
	{
		public virtual ICollection<Lookup> LookupItems
		{
			get;
			set;
		}

		public LookupFill(string stringDataPopulateKey, int currentTenantId)
		{
			string[] strArrays;
			int i;
			List<Lookup> lookups = new List<Lookup>();
			switch (stringDataPopulateKey)
			{
				case "PaymentTerms":
				{
					strArrays = new string[] { "< Add New >", "Due on Receipt", "1% 10, Net 30", "2% 10, Net 30", "Net 10", "Net 15", "Net 30" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str,
							Value = str,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "DiscountTypes":
				{
					strArrays = new string[] { "Dollar", "Percentage" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str1 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str1,
							Value = str1,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "TaxRuleBehaviors":
				{
					strArrays = new string[] { "This tax only", "Combine", "One after another" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str2 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str2,
							Value = str2,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "PostLoginViewTypes":
				{
					strArrays = new string[] { "Dashboard", "Delivery Schedule", "Invoices", "Suppliers" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str3 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str3,
							Value = str3,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "BillingTypes":
				{
					strArrays = new string[] { "Hourly Staff Rate", "Hourly Task Rate", "Hourly Project Rate", "Flat Project Amount" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str4 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str4,
							Value = str4,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "QuantityTypes":
				{
					strArrays = new string[] { "gallon", "each", "piece", "foot", "yard", "case" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str5 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str5,
							Value = str5,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "TitleTypes":
				{
					strArrays = new string[] { "Male", "Female", "Neutral" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str6 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str6,
							Value = str6,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "BankTypes":
				{
					strArrays = new string[] { "Checking", "Savings", "Other" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str7 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str7,
							Value = str7,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "FillLotAddressTypes":
				{
					strArrays = new string[] { "Company", "Supplier", "Rural" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str8 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str8,
							Value = str8,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "AddressTypes":
				{
					strArrays = new string[] { "Commercial", "Mailing", "Primary Residence", "Secondary Residence", "Vacation" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str9 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str9,
							Value = str9,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "PhoneTypes":
				{
					strArrays = new string[] { "Home", "Mobile", "Office", "Relative", "Fax" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str10 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str10,
							Value = str10,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "ServiceTypes":
				{
					strArrays = new string[] { "Builder", "Commercial", "Residential - Home Owner", "Residential - Renter", "Other" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str11 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str11,
							Value = str11,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "RequestedServices":
				{
					strArrays = new string[] { "Propane Service", "Complimentary Service Inspection", "Propane System Installation", "Other" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str12 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str12,
							Value = str12,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
				case "HeardOfUs":
				{
					strArrays = new string[] { "Event", "Internet Search", "Mail", "News Print", "Phone Book", "Referral - Customer", "Referral - Employee", "Referral - External", "Saw Your Truck!", "Other" };
					for (i = 0; i < (int)strArrays.Length; i++)
					{
						string str13 = strArrays[i];
						lookups.Add(new Lookup()
						{
							Text = str13,
							Value = str13,
							TenantId = currentTenantId,
							Selected = false,
							Disabled = false,
							Group = null,
							AssociatedClass = string.Empty
						});
					}
					break;
				}
			}
			this.LookupItems = lookups;
		}

		public static Lookup CreateLookupFromString(string str, int tenantId)
		{
			return new Lookup()
			{
				Text = str,
				Value = str,
				TenantId = tenantId,
				Selected = false,
				Disabled = false,
				Group = null,
				AssociatedClass = string.Empty
			};
		}
	}
}