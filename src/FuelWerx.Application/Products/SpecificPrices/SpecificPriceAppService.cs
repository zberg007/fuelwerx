using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Organizations;
using Abp.Runtime.Session;
using FuelWerx;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Customers;
using FuelWerx.Dto;
using FuelWerx.Generic;
using FuelWerx.MultiTenancy;
using FuelWerx.Organizations;
using FuelWerx.Organizations.Dto;
using FuelWerx.Products;
using FuelWerx.Products.SpecificPrices.Dto;
using FuelWerx.Products.SpecificPrices.Exporting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Products.SpecificPrices
{
	[AbpAuthorize(new string[] { "Pages.Tenant.Products" })]
	public class SpecificPriceAppService : FuelWerxAppServiceBase, ISpecificPriceAppService, IApplicationService, ITransientDependency
	{
		private readonly FuelWerx.Authorization.Users.UserManager _userManager;

		private readonly IRepository<Product, long> _productRepository;

		private readonly IRepository<ProductSpecificPrice, long> _specificPriceRepository;

		private readonly IRepository<Address, long> _addressRepository;

		private readonly ISpecificPriceListExcelExporter _specificPriceListExcelExporter;

		private readonly IRepository<Customer, long> _customerRepository;

		private readonly IOrganizationUnitAppService _organizationUnitAppService;

		public SpecificPriceAppService(FuelWerx.Authorization.Users.UserManager userManager, IRepository<ProductSpecificPrice, long> specificPriceRepository, ISpecificPriceListExcelExporter specificPriceListExcelExporter, IRepository<Product, long> productRepository, IRepository<Address, long> addressRepository, IRepository<Customer, long> customerRepository, IOrganizationUnitAppService organizationUnitAppService)
		{
			this._userManager = userManager;
			this._specificPriceRepository = specificPriceRepository;
			this._addressRepository = addressRepository;
			this._specificPriceListExcelExporter = specificPriceListExcelExporter;
			this._customerRepository = customerRepository;
			this._productRepository = productRepository;
			this._organizationUnitAppService = organizationUnitAppService;
		}

		public async Task<string> CheckForSpecificPrice(CheckSpecificPriceInputDto input)
		{
			string str;
			Customer customer;
			Address address;
			decimal num1;
			bool flag;
			bool flag1;
			bool flag2;
			if (input.ProductId <= 0 || input.ForCustomerId <= 0)
			{
				str = "";
			}
			else
			{
				Product async = await this._productRepository.GetAsync((long)input.ProductId);
				Product product = async;
				IRepository<ProductSpecificPrice, long> repository = this._specificPriceRepository;
				List<ProductSpecificPrice> allListAsync = await repository.GetAllListAsync((ProductSpecificPrice x) => x.ProductId == product.Id);
				List<ProductSpecificPrice> productSpecificPrices = allListAsync;
				bool flag3 = false;
				if (productSpecificPrices.Any<ProductSpecificPrice>())
				{
					List<string> strs = new List<string>();
					string empty = string.Empty;
					List<string> upchargeForUsersOrganizationUnits = new List<string>();
					string empty1 = string.Empty;
					if (input.ForCustomerId <= 0)
					{
						customer = null;
					}
					else
					{
						Customer async1 = await this._customerRepository.GetAsync((long)input.ForCustomerId);
						customer = async1;
					}
					Customer customer1 = customer;
					if (input.ForCustomerAddressId <= 0)
					{
						address = null;
					}
					else
					{
						Address address1 = await this._addressRepository.GetAsync((long)input.ForCustomerAddressId);
						address = address1;
					}
					Address address2 = address;
					int countryId = 0;
					List<long> nums = new List<long>();
					if (address2 != null)
					{
						countryId = address2.CountryId;
					}
					if (customer1 != null && customer1.UserId.HasValue)
					{
						FuelWerx.Authorization.Users.UserManager userManager = this._userManager;
						User userByIdAsync = await userManager.GetUserByIdAsync(customer1.UserId.Value);
						List<Abp.Organizations.OrganizationUnit> organizationUnitsAsync = await this._userManager.GetOrganizationUnitsAsync(userByIdAsync);
						nums = (
							from ou in organizationUnitsAsync
							select ou.Id).ToList<long>();
						strs = await this._organizationUnitAppService.GetDiscountForUsersOrganizationUnits(customer1.Id);
						if (strs.Any<string>())
						{
							empty = strs[0];
						}
						upchargeForUsersOrganizationUnits = await this._organizationUnitAppService.GetUpchargeForUsersOrganizationUnits(customer1.Id);
						if (upchargeForUsersOrganizationUnits.Any<string>())
						{
							empty1 = upchargeForUsersOrganizationUnits[0];
						}
						if (nums.Any<long>())
						{
							customer1 = null;
							flag3 = true;
						}
					}
					string[] strArrays = new string[0];
					if (!string.IsNullOrEmpty(input.ProductOptionIds))
					{
						string productOptionIds = input.ProductOptionIds;
						productOptionIds.Split(new char[] { ',' });
					}
					IEnumerable<ProductSpecificPrice> productSpecificPrices1 = productSpecificPrices.WhereIf<ProductSpecificPrice>(input.Quantity > 0, (ProductSpecificPrice p) => {
						int? startingAtQuantity = p.StartingAtQuantity;
						int quantity = input.Quantity;
						if (startingAtQuantity.GetValueOrDefault() > quantity)
						{
							return false;
						}
						return startingAtQuantity.HasValue;
					}).Where<ProductSpecificPrice>((ProductSpecificPrice p) => {
						if (p.ForCurrency == input.ForCurrency)
						{
							return true;
						}
						return p.ForCurrency == null;
					}).WhereIf<ProductSpecificPrice>(customer1 != null, (ProductSpecificPrice p) => {
						long? forCustomerId = p.ForCustomerId;
						long id = customer1.Id;
						if (forCustomerId.GetValueOrDefault() != id)
						{
							return false;
						}
						return forCustomerId.HasValue;
					}).Where<ProductSpecificPrice>((ProductSpecificPrice p) => {
						int? forCountryId = p.ForCountryId;
						int num = countryId;
						if ((forCountryId.GetValueOrDefault() == num ? forCountryId.HasValue : false))
						{
							return true;
						}
						forCountryId = p.ForCountryId;
						return !forCountryId.HasValue;
					});
					bool flag4 = !nums.Any<long>();
					IEnumerable<ProductSpecificPrice> productSpecificPrices2 = productSpecificPrices1.WhereIf<ProductSpecificPrice>(flag4, (ProductSpecificPrice p) => !p.ForOrganizationalUnitId.HasValue);
					flag = (!nums.Any<long>() ? false : nums.Count == 1);
					IEnumerable<ProductSpecificPrice> productSpecificPrices3 = productSpecificPrices2.WhereIf<ProductSpecificPrice>(flag, (ProductSpecificPrice p) => {
						if (p.ForOrganizationalUnitId.HasValue && p.ForOrganizationalUnitId.Value == nums[0])
						{
							return true;
						}
						return !p.ForOrganizationalUnitId.HasValue;
					});
					flag1 = (!nums.Any<long>() ? false : nums.Count > 1);
					List<ProductSpecificPrice> list = productSpecificPrices3.WhereIf<ProductSpecificPrice>(flag1, (ProductSpecificPrice p) => {
						if (p.ForOrganizationalUnitId.HasValue && nums.Contains(p.ForOrganizationalUnitId.Value))
						{
							return true;
						}
						return !p.ForOrganizationalUnitId.HasValue;
					}).ToList<ProductSpecificPrice>();
					if (!list.Any<ProductSpecificPrice>())
					{
						str = "";
					}
					else
					{
						if (list.Count > 1 & flag3 && nums.Any<long>() && nums.Count == 1)
						{
							List<ProductSpecificPrice> productSpecificPrices4 = list;
							list = list.Where<ProductSpecificPrice>((ProductSpecificPrice x) => {
								long? forOrganizationalUnitId = x.ForOrganizationalUnitId;
								long item = nums[0];
								if (forOrganizationalUnitId.GetValueOrDefault() != item)
								{
									return false;
								}
								return forOrganizationalUnitId.HasValue;
							}).ToList<ProductSpecificPrice>();
							if (!list.Any<ProductSpecificPrice>() && input.ForCustomerId > 0)
							{
								list = productSpecificPrices4.Where<ProductSpecificPrice>((ProductSpecificPrice x) => {
									long? forCustomerId = x.ForCustomerId;
									long num = (long)input.ForCustomerId;
									if (forCustomerId.GetValueOrDefault() != num)
									{
										return false;
									}
									return forCustomerId.HasValue;
								}).ToList<ProductSpecificPrice>();
							}
						}
						if (!list.Any<ProductSpecificPrice>())
						{
							str = "";
						}
						else
						{
							DateTime? availableFrom = list.First<ProductSpecificPrice>().AvailableFrom;
							DateTime? availableTo = list.First<ProductSpecificPrice>().AvailableTo;
							bool flag5 = false;
							if (!availableFrom.HasValue && !availableTo.HasValue)
							{
								flag5 = true;
							}
							else if (availableFrom.HasValue && availableTo.HasValue && availableFrom.Value <= DateTime.Now && availableTo.Value >= DateTime.Now)
							{
								flag5 = true;
							}
							if (!flag5)
							{
								str = "";
							}
							else
							{
								decimal cost = list.First<ProductSpecificPrice>().Cost;
								if (empty.Length > 0)
								{
									if (empty.Contains("$"))
									{
										num1 = decimal.Parse(cost.ToString()) - decimal.Parse(empty.Replace("$", ""));
										cost = decimal.Parse(num1.ToString());
									}
									else if (empty.Contains("%"))
									{
										num1 = decimal.Parse(cost.ToString()) - (decimal.Parse(cost.ToString()) * decimal.Parse(empty.Replace("%", "").ToString()));
										cost = decimal.Parse(num1.ToString("#.##"));
									}
								}
								if (empty1.Length > 0)
								{
									if (empty1.Contains("$"))
									{
										num1 = decimal.Parse(cost.ToString()) + decimal.Parse(empty1.Replace("$", ""));
										cost = decimal.Parse(num1.ToString());
									}
									else if (empty1.Contains("%"))
									{
										num1 = decimal.Parse(cost.ToString()) + (decimal.Parse(cost.ToString()) * decimal.Parse(empty1.Replace("%", "").ToString()));
										cost = decimal.Parse(num1.ToString("#.##"));
									}
								}
								decimal? discount = list.First<ProductSpecificPrice>().Discount;
								if (discount.HasValue)
								{
									decimal? nullable = discount;
									num1 = new decimal();
									flag2 = (nullable.GetValueOrDefault() > num1 ? nullable.HasValue : false);
									if (flag2)
									{
										if (decimal.Parse(discount.ToString()) >= new decimal(0.999))
										{
											cost = decimal.Parse(cost.ToString()) - decimal.Parse(discount.ToString());
										}
										else
										{
											num1 = decimal.Parse(cost.ToString()) - (decimal.Parse(cost.ToString()) * decimal.Parse(discount.ToString()));
											cost = decimal.Parse(num1.ToString("#.##"));
										}
									}
								}
								str = cost.ToString();
							}
						}
					}
				}
				else
				{
					str = "";
				}
			}
			return str;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task<long> CreateOrUpdateProductSpecificPrice(CreateOrUpdateProductSpecificPriceInput input)
		{
			long num;
			long num1;
			DateTime? nullable;
			DateTime? nullable1;
			decimal? nullable2;
			bool? nullable3;
			int? nullable4;
			long? nullable5;
			long? nullable6;
			long? nullable7;
			int? nullable8;
			long value = (long)0;
			long? id = input.SpecificPrice.Id;
			if (id.HasValue)
			{
				id = input.SpecificPrice.Id;
				if (id.Value <= (long)0)
				{
					num1 = await this._specificPriceRepository.InsertAndGetIdAsync(input.SpecificPrice.MapTo<ProductSpecificPrice>());
					value = num1;
					num = value;
					return num;
				}
				IRepository<ProductSpecificPrice, long> repository = this._specificPriceRepository;
				id = input.SpecificPrice.Id;
				ProductSpecificPrice async = await repository.GetAsync(id.Value);
				async.Cost = input.SpecificPrice.Cost;
				ProductSpecificPrice productSpecificPrice = async;
				DateTime? availableFrom = input.SpecificPrice.AvailableFrom;
				if (availableFrom.HasValue)
				{
					availableFrom = input.SpecificPrice.AvailableFrom;
					nullable = new DateTime?(availableFrom.Value);
				}
				else
				{
					availableFrom = null;
					nullable = availableFrom;
				}
				productSpecificPrice.AvailableFrom = nullable;
				ProductSpecificPrice productSpecificPrice1 = async;
				availableFrom = input.SpecificPrice.AvailableTo;
				if (availableFrom.HasValue)
				{
					availableFrom = input.SpecificPrice.AvailableTo;
					nullable1 = new DateTime?(availableFrom.Value);
				}
				else
				{
					availableFrom = null;
					nullable1 = availableFrom;
				}
				productSpecificPrice1.AvailableTo = nullable1;
				async.BaseCost = input.SpecificPrice.BaseCost;
				async.Cost = input.SpecificPrice.Cost;
				async.BaseCostOverride = input.SpecificPrice.BaseCostOverride;
				ProductSpecificPrice productSpecificPrice2 = async;
				decimal? discount = input.SpecificPrice.Discount;
				if (discount.HasValue)
				{
					discount = input.SpecificPrice.Discount;
					nullable2 = new decimal?(discount.Value);
				}
				else
				{
					discount = null;
					nullable2 = discount;
				}
				productSpecificPrice2.Discount = nullable2;
				ProductSpecificPrice productSpecificPrice3 = async;
				bool? discountIncludeTax = input.SpecificPrice.DiscountIncludeTax;
				if (discountIncludeTax.HasValue)
				{
					discountIncludeTax = input.SpecificPrice.DiscountIncludeTax;
					nullable3 = new bool?(discountIncludeTax.Value);
				}
				else
				{
					discountIncludeTax = null;
					nullable3 = discountIncludeTax;
				}
				productSpecificPrice3.DiscountIncludeTax = nullable3;
				ProductSpecificPrice productSpecificPrice4 = async;
				int? forCountryId = input.SpecificPrice.ForCountryId;
				if (forCountryId.HasValue)
				{
					forCountryId = input.SpecificPrice.ForCountryId;
					nullable4 = new int?(forCountryId.Value);
				}
				else
				{
					forCountryId = null;
					nullable4 = forCountryId;
				}
				productSpecificPrice4.ForCountryId = nullable4;
				async.ForCurrency = "Dollar";
				ProductSpecificPrice productSpecificPrice5 = async;
				id = input.SpecificPrice.ForCustomerId;
				if (id.HasValue)
				{
					id = input.SpecificPrice.ForCustomerId;
					nullable5 = new long?(id.Value);
				}
				else
				{
					id = null;
					nullable5 = id;
				}
				productSpecificPrice5.ForCustomerId = nullable5;
				ProductSpecificPrice productSpecificPrice6 = async;
				id = input.SpecificPrice.ForOrganizationalUnitId;
				if (id.HasValue)
				{
					id = input.SpecificPrice.ForOrganizationalUnitId;
					nullable6 = new long?(id.Value);
				}
				else
				{
					id = null;
					nullable6 = id;
				}
				productSpecificPrice6.ForOrganizationalUnitId = nullable6;
				ProductSpecificPrice productSpecificPrice7 = async;
				id = input.SpecificPrice.ProductOptionId;
				if (id.HasValue)
				{
					id = input.SpecificPrice.ProductOptionId;
					nullable7 = new long?(id.Value);
				}
				else
				{
					id = null;
					nullable7 = id;
				}
				productSpecificPrice7.ProductOptionId = nullable7;
				ProductSpecificPrice productSpecificPrice8 = async;
				forCountryId = input.SpecificPrice.StartingAtQuantity;
				if (forCountryId.HasValue)
				{
					forCountryId = input.SpecificPrice.StartingAtQuantity;
					nullable8 = new int?(forCountryId.Value);
				}
				else
				{
					forCountryId = null;
					nullable8 = forCountryId;
				}
				productSpecificPrice8.StartingAtQuantity = nullable8;
				async.IsActive = input.SpecificPrice.IsActive;
				id = input.SpecificPrice.Id;
				value = id.Value;
				await this._specificPriceRepository.UpdateAsync(async);
				num = value;
				return num;
			}
			num1 = await this._specificPriceRepository.InsertAndGetIdAsync(input.SpecificPrice.MapTo<ProductSpecificPrice>());
			value = num1;
			num = value;
			return num;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Delete" })]
		public async Task DeleteProductSpecificPrice(IdInput<long> input)
		{
			await this._specificPriceRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Create" })]
		public async Task<GetProductSpecificPriceForEditOutput> GetProductSpecificPriceForEdit(NullableIdInput<long> input)
		{
			ProductSpecificPriceEditDto productSpecificPriceEditDto;
			if (!input.Id.HasValue)
			{
				productSpecificPriceEditDto = new ProductSpecificPriceEditDto();
			}
			else
			{
				IRepository<ProductSpecificPrice, long> repository = this._specificPriceRepository;
				ProductSpecificPrice async = await repository.GetAsync(input.Id.Value);
				productSpecificPriceEditDto = async.MapTo<ProductSpecificPriceEditDto>();
			}
			return new GetProductSpecificPriceForEditOutput()
			{
				SpecificPrice = productSpecificPriceEditDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.ExportData" })]
		public async Task<FileDto> GetProductSpecificPricesToExcel()
		{
			List<ProductSpecificPrice> allListAsync = await this._specificPriceRepository.GetAllListAsync();
			List<ProductSpecificPriceListDto> productSpecificPriceListDtos = allListAsync.MapTo<List<ProductSpecificPriceListDto>>();
			return this._specificPriceListExcelExporter.ExportToFile(productSpecificPriceListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products" })]
		public async Task<PagedResultOutput<ProductSpecificPriceListDto>> GetSpecificPrices(GetProductSpecificPricesInput input)
		{
			IQueryable<ProductSpecificPrice> all = this._specificPriceRepository.GetAll();
			IQueryable<ProductSpecificPrice> tenantId = 
				from x in all
				where x.TenantId == (this.AbpSession.ImpersonatorTenantId.HasValue ? this.AbpSession.ImpersonatorTenantId.Value : this.AbpSession.TenantId.Value)
				select x;
			IQueryable<ProductSpecificPrice> productId = tenantId.WhereIf<ProductSpecificPrice>(!input.Filter.IsNullOrEmpty(), (ProductSpecificPrice p) => p.Cost == decimal.Parse(input.Filter));
			if (input.ProductId > (long)0)
			{
				IQueryable<ProductSpecificPrice> productSpecificPrices = this._specificPriceRepository.GetAll();
				productId = 
					from p in productSpecificPrices
					where p.ProductId == input.ProductId
					select p;
			}
			int num = await productId.CountAsync<ProductSpecificPrice>();
			List<ProductSpecificPrice> listAsync = await productId.OrderBy<ProductSpecificPrice>(input.Sorting, new object[0]).PageBy<ProductSpecificPrice>(input).ToListAsync<ProductSpecificPrice>();
			PagedResultOutput<ProductSpecificPriceListDto> pagedResultOutput = new PagedResultOutput<ProductSpecificPriceListDto>(num, listAsync.MapTo<List<ProductSpecificPriceListDto>>());
			if (pagedResultOutput.Items.Any<ProductSpecificPriceListDto>())
			{
				foreach (ProductSpecificPriceListDto item in pagedResultOutput.Items)
				{
					if (item.ForOrganizationalUnitId.HasValue)
					{
						IOrganizationUnitAppService organizationUnitAppService = this._organizationUnitAppService;
						GetOrganizationUnitUsersInput getOrganizationUnitUsersInput = new GetOrganizationUnitUsersInput()
						{
							Id = item.ForOrganizationalUnitId.Value,
							MaxResultCount = 1000
						};
						PagedResultOutput<OrganizationUnitUserListDto> organizationUnitUsers = await organizationUnitAppService.GetOrganizationUnitUsers(getOrganizationUnitUsersInput);
						if (organizationUnitUsers.Items.Any<OrganizationUnitUserListDto>())
						{
							item.ForOrganizationalUnit.MemberCount = organizationUnitUsers.TotalCount;
						}
					}
				}
			}
			return pagedResultOutput;
		}
	}
}