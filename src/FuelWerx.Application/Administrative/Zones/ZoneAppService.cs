using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using FuelWerx;
using FuelWerx.Administrative;
using FuelWerx.Administrative.Taxes.Dto;
using FuelWerx.Administrative.Zones.Dto;
using FuelWerx.Administrative.Zones.Exporting;
using FuelWerx.Dto;
using FuelWerx.EntityFramework;
using FuelWerx.Generic;
using FuelWerx.Storage;
using FuelWerx.Tenants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.Zones
{
	[AbpAuthorize(new string[] { "Pages.Administration.Zones" })]
	public class ZoneAppService : FuelWerxAppServiceBase, IZoneAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Zone, long> _zoneRepository;

		private readonly IZoneListExcelExporter _zoneListExcelExporter;

		private readonly IRepository<ZoneTax, long> _zoneTaxRepository;

		private readonly IUnitOfWorkManager _unitOfWorkManager;

		private readonly IRepository<TenantDetail, long> _tenantDetailRepository;

		private readonly IRepository<Address, long> _addressRepository;

		private readonly IRepository<EmergencyDeliveryFee, long> _emergencyDeliveryFeeRepository;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public ZoneAppService(IRepository<Zone, long> zoneRepository, IRepository<Address, long> addressRepository, IRepository<ZoneTax, long> zoneTaxRepository, IRepository<TenantDetail, long> tenantDetailRepository, IZoneListExcelExporter zoneListExcelExporter, IRepository<EmergencyDeliveryFee, long> emergencyDeliveryFeeRepository, IBinaryObjectManager binaryObjectManager, IUnitOfWorkManager unitOfWorkManager)
		{
			this._addressRepository = addressRepository;
			this._zoneRepository = zoneRepository;
			this._tenantDetailRepository = tenantDetailRepository;
			this._zoneTaxRepository = zoneTaxRepository;
			this._zoneListExcelExporter = zoneListExcelExporter;
			this._emergencyDeliveryFeeRepository = emergencyDeliveryFeeRepository;
			this._binaryObjectManager = binaryObjectManager;
			this._unitOfWorkManager = unitOfWorkManager;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Zones.Create", "Pages.Administration.Zones.Edit" })]
		[UnitOfWork]
		public async Task<long> CreateOrUpdateZone(CreateOrUpdateZoneInput input)
		{
			long value;
			long id;
			IRepository<Zone, long> repository = this._zoneRepository;
			List<Zone> allListAsync = await repository.GetAllListAsync((Zone m) => m.Name == input.Zone.Name && m.TenantId == (this.AbpSession.ImpersonatorTenantId.HasValue ? this.AbpSession.ImpersonatorTenantId.Value : this.AbpSession.TenantId.Value));
			List<Zone> zones = allListAsync;
			if (zones.Any<Zone>() && (!input.Zone.Id.HasValue || input.Zone.Id.HasValue && input.Zone.Id.Value != zones.First<Zone>().Id))
			{
				throw new UserFriendlyException(this.L("ZonePolygonNameAlreadyInUse"));
			}
			IRepository<Zone, long> repository1 = this._zoneRepository;
			List<Zone> allListAsync1 = await repository1.GetAllListAsync((Zone m) => m.HexColor == input.Zone.HexColor && m.TenantId == (this.AbpSession.ImpersonatorTenantId.HasValue ? this.AbpSession.ImpersonatorTenantId.Value : this.AbpSession.TenantId.Value));
			List<Zone> zones1 = allListAsync1;
			if (zones1.Any<Zone>() && (!input.Zone.Id.HasValue || input.Zone.Id.HasValue && input.Zone.Id.Value != zones1.First<Zone>().Id))
			{
				throw new UserFriendlyException(this.L("ZonePolygonColorAlreadyInUse"));
			}
			if (!input.Zone.Id.HasValue || input.Zone.Id.Value <= (long)0)
			{
				long num = await this._zoneRepository.InsertAndGetIdAsync(input.Zone.MapTo<Zone>());
				id = num;
			}
			else
			{
				IRepository<Zone, long> repository2 = this._zoneRepository;
				long? impersonatorUserId = input.Zone.Id;
				Zone async = await repository2.GetAsync(impersonatorUserId.Value);
				async.Caption = input.Zone.Caption;
				async.HexColor = input.Zone.HexColor;
				async.IsActive = input.Zone.IsActive;
				async.Name = input.Zone.Name;
				async.PolygonCoordinates = input.Zone.PolygonCoordinates;
				async.PolygonCoordinatesReversed = input.Zone.PolygonCoordinatesReversed;
				List<long> nums = new List<long>();
				bool flag = !input.Zone.Taxes.Any<ZoneTax>();
				if (async.Taxes.Any<ZoneTax>())
				{
					foreach (ZoneTax taxis in async.Taxes)
					{
						if (flag || !(
							from x in input.Zone.Taxes
							where x.TaxId == taxis.TaxId
							select x).Any<ZoneTax>())
						{
							taxis.IsDeleted = true;
							ZoneTax nullable = taxis;
							if (this.AbpSession.ImpersonatorUserId.HasValue)
							{
								impersonatorUserId = this.AbpSession.ImpersonatorUserId;
								value = impersonatorUserId.Value;
							}
							else
							{
								impersonatorUserId = this.AbpSession.UserId;
								value = impersonatorUserId.Value;
							}
							nullable.DeleterUserId = new long?(value);
							taxis.DeletionTime = new DateTime?(DateTime.Now);
						}
						nums.Add(taxis.TaxId);
					}
				}
				IEnumerable<ZoneTax> taxes = 
					from x in input.Zone.Taxes
					where !nums.Contains(x.TaxId)
					select x;
				if (taxes.Any<ZoneTax>())
				{
					foreach (ZoneTax zoneTax in taxes)
					{
						async.Taxes.Add(zoneTax);
					}
				}
				id = async.Id;
				await this._zoneRepository.UpdateAsync(async);
			}
			return id;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Zones.Delete" })]
		public async Task DeleteZone(IdInput<long> input)
		{
			await this._zoneRepository.DeleteAsync(input.Id);
		}

		public async Task<ZoneListDto> GetZone(long input, long tenantId)
		{
			ZoneListDto zoneListDto;
			Zone async = await this._zoneRepository.GetAsync(input);
			zoneListDto = (!(async != null) || (long)async.TenantId != tenantId ? new ZoneListDto() : async.MapTo<ZoneListDto>());
			return zoneListDto;
		}

		public async Task<decimal> GetZoneEmergencyDeliveryFeesByAddressId(int tenantId, long addressId, bool active)
		{
			decimal num;
			if (addressId != (long)-1)
			{
				Address async = await this._addressRepository.GetAsync(addressId);
				IQueryable<Zone> all = this._zoneRepository.GetAll();
				IQueryable<Zone> zones = 
					from p in all
					where p.TenantId == tenantId
					select p;
				bool flag = active;
				List<Zone> listAsync = await zones.WhereIf<Zone>(flag, (Zone p) => p.IsActive == active).OrderBy<Zone>("Name", new object[0]).ToListAsync<Zone>();
				List<ZoneDto> zoneDtos = new List<ZoneDto>();
				CustomDbContext customDbContext = new CustomDbContext();
				foreach (Zone zone in listAsync)
				{
					ZoneDto zoneDto = zone.MapTo<ZoneDto>();
					object[] sqlParameter = new object[] { new SqlParameter("@ZoneId", (object)zoneDto.Id), new SqlParameter("@AddressId", (object)async.Id), null };
					SqlParameter sqlParameter1 = new SqlParameter()
					{
						ParameterName = "@retVal",
						SqlDbType = SqlDbType.Int,
						Direction = ParameterDirection.Output,
						Value = 0
					};
					sqlParameter[2] = sqlParameter1;
					if (customDbContext.Database.SqlQuery<int>("exec @retVal = Geo_AddressIdInZone @ZoneId, @AddressId", sqlParameter).Single<int>() != 1)
					{
						continue;
					}
					zoneDtos.Add(zoneDto);
				}
				customDbContext.Dispose();
				List<ZoneDto> zoneDtos1 = new List<ZoneDto>(zoneDtos.MapTo<List<ZoneDto>>());
				List<decimal> nums = new List<decimal>();
				foreach (ZoneDto zoneDto1 in zoneDtos1)
				{
					IRepository<EmergencyDeliveryFee, long> repository = this._emergencyDeliveryFeeRepository;
					List<EmergencyDeliveryFee> allListAsync = await repository.GetAllListAsync((EmergencyDeliveryFee m) => m.ZoneId == (long?)zoneDto1.Id);
					foreach (EmergencyDeliveryFee emergencyDeliveryFee in allListAsync)
					{
						if (emergencyDeliveryFee.Fee <= decimal.Zero)
						{
							continue;
						}
						nums.Add(emergencyDeliveryFee.Fee);
					}
				}
				if (!nums.Any<decimal>())
				{
					num = new decimal(0);
				}
				else
				{
					List<decimal> nums1 = nums;
					num = nums1.Sum<decimal>((decimal d) => d);
				}
			}
			else
			{
				IRepository<TenantDetail, long> repository1 = this._tenantDetailRepository;
				TenantDetail tenantDetail = await repository1.FirstOrDefaultAsync((TenantDetail x) => x.TenantId == tenantId);
				TenantDetail tenantDetail1 = tenantDetail;
				IQueryable<Zone> all1 = this._zoneRepository.GetAll();
				IQueryable<Zone> zones1 = 
					from p in all1
					where p.TenantId == tenantId
					select p;
				bool flag1 = active;
				List<Zone> listAsync1 = await zones1.WhereIf<Zone>(flag1, (Zone p) => p.IsActive == active).OrderBy<Zone>("Name", new object[0]).ToListAsync<Zone>();
				List<ZoneDto> zoneDtos2 = new List<ZoneDto>();
				CustomDbContext customDbContext1 = new CustomDbContext();
				foreach (Zone zone1 in listAsync1)
				{
					ZoneDto zoneDto2 = zone1.MapTo<ZoneDto>();
					object[] objArray = new object[] { new SqlParameter("@ZoneId", (object)zoneDto2.Id), new SqlParameter("@CompanyAddressId", (object)tenantDetail1.Id), null };
					SqlParameter sqlParameter2 = new SqlParameter()
					{
						ParameterName = "@retVal",
						SqlDbType = SqlDbType.Int,
						Direction = ParameterDirection.Output,
						Value = 0
					};
					objArray[2] = sqlParameter2;
					if (customDbContext1.Database.SqlQuery<int>("exec @retVal = Geo_CompanyAddressIdInZone @ZoneId, @CompanyAddressId", objArray).Single<int>() != 1)
					{
						continue;
					}
					zoneDtos2.Add(zoneDto2);
				}
				customDbContext1.Dispose();
				List<ZoneDto> zoneDtos3 = new List<ZoneDto>(zoneDtos2.MapTo<List<ZoneDto>>());
				List<decimal> nums2 = new List<decimal>();
				foreach (ZoneDto zoneDto3 in zoneDtos3)
				{
					IRepository<EmergencyDeliveryFee, long> repository2 = this._emergencyDeliveryFeeRepository;
					List<EmergencyDeliveryFee> emergencyDeliveryFees = await repository2.GetAllListAsync((EmergencyDeliveryFee m) => m.ZoneId == (long?)zoneDto3.Id);
					foreach (EmergencyDeliveryFee emergencyDeliveryFee1 in emergencyDeliveryFees)
					{
						if (emergencyDeliveryFee1.Fee <= decimal.Zero)
						{
							continue;
						}
						nums2.Add(emergencyDeliveryFee1.Fee);
					}
				}
				if (!nums2.Any<decimal>())
				{
					num = new decimal(0);
				}
				else
				{
					List<decimal> nums3 = nums2;
					num = nums3.Sum<decimal>((decimal d) => d);
				}
			}
			return num;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Zones.Edit" })]
		public async Task<GetZoneForEditOutput> GetZoneForEdit(NullableIdInput<long> input)
		{
			ZoneEditDto zoneEditDto;
			if (!input.Id.HasValue)
			{
				zoneEditDto = new ZoneEditDto()
				{
					Taxes = new List<ZoneTax>()
				};
			}
			else
			{
				IRepository<Zone, long> repository = this._zoneRepository;
				Zone async = await repository.GetAsync(input.Id.Value);
				zoneEditDto = async.MapTo<ZoneEditDto>();
				IRepository<ZoneTax, long> repository1 = this._zoneTaxRepository;
				List<ZoneTax> allListAsync = await repository1.GetAllListAsync((ZoneTax m) => m.ZoneId == async.Id);
				List<ZoneTax> zoneTaxes = allListAsync;
				if (!zoneTaxes.Any<ZoneTax>())
				{
					zoneEditDto.Taxes = new List<ZoneTax>();
				}
				else
				{
					zoneEditDto.Taxes = zoneTaxes;
				}
			}
			return new GetZoneForEditOutput()
			{
				Zone = zoneEditDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Zones" })]
		public async Task<PagedResultOutput<ZoneListDto>> GetZones(GetZonesInput input)
		{
			IQueryable<Zone> all = this._zoneRepository.GetAll();
			IQueryable<Zone> zones = all.WhereIf<Zone>(!input.Filter.IsNullOrEmpty(), (Zone p) => p.Name.Contains(input.Filter) || p.Caption.Contains(input.Filter));
			int num = await zones.CountAsync<Zone>();
			List<Zone> listAsync = await zones.OrderBy<Zone>(input.Sorting, new object[0]).PageBy<Zone>(input).ToListAsync<Zone>();
			return new PagedResultOutput<ZoneListDto>(num, listAsync.MapTo<List<ZoneListDto>>());
		}

		public async Task<List<ZoneListDto>> GetZonesByAddressId(int tenantId, long addressId, bool active)
		{
			List<ZoneListDto> zoneListDtos;
			if (addressId != (long)-1)
			{
				Address async = await this._addressRepository.GetAsync(addressId);
				IQueryable<Zone> all = this._zoneRepository.GetAll();
				IQueryable<Zone> zones = 
					from p in all
					where p.TenantId == tenantId
					select p;
				bool flag = active;
				IQueryable<Zone> zones1 = zones.WhereIf<Zone>(flag, (Zone p) => p.IsActive == active);
				List<Zone> listAsync = await System.Data.Entity.QueryableExtensions.Include<Zone, ICollection<ZoneTax>>(zones1, (Zone n) => n.Taxes).OrderBy<Zone>("Name", new object[0]).ToListAsync<Zone>();
				List<ZoneDto> zoneDtos = new List<ZoneDto>();
				CustomDbContext customDbContext = new CustomDbContext();
				foreach (Zone zone in listAsync)
				{
					ZoneDto zoneDto = zone.MapTo<ZoneDto>();
					object[] sqlParameter = new object[] { new SqlParameter("@ZoneId", (object)zoneDto.Id), new SqlParameter("@AddressId", (object)async.Id), null };
					SqlParameter sqlParameter1 = new SqlParameter()
					{
						ParameterName = "@retVal",
						SqlDbType = SqlDbType.Int,
						Direction = ParameterDirection.Output,
						Value = 0
					};
					sqlParameter[2] = sqlParameter1;
					if (customDbContext.Database.SqlQuery<int>("exec @retVal = Geo_AddressIdInZone @ZoneId, @AddressId", sqlParameter).Single<int>() != 1)
					{
						continue;
					}
					zoneDtos.Add(zoneDto);
				}
				customDbContext.Dispose();
				zoneListDtos = new List<ZoneListDto>(zoneDtos.MapTo<List<ZoneListDto>>());
			}
			else
			{
				IRepository<TenantDetail, long> repository = this._tenantDetailRepository;
				TenantDetail tenantDetail = await repository.FirstOrDefaultAsync((TenantDetail x) => x.TenantId == tenantId);
				TenantDetail tenantDetail1 = tenantDetail;
				IQueryable<Zone> all1 = this._zoneRepository.GetAll();
				IQueryable<Zone> zones2 = 
					from p in all1
					where p.TenantId == tenantId
					select p;
				bool flag1 = active;
				IQueryable<Zone> zones3 = zones2.WhereIf<Zone>(flag1, (Zone p) => p.IsActive == active);
				List<Zone> listAsync1 = await System.Data.Entity.QueryableExtensions.Include<Zone, ICollection<ZoneTax>>(zones3, (Zone n) => n.Taxes).OrderBy<Zone>("Name", new object[0]).ToListAsync<Zone>();
				List<ZoneDto> zoneDtos1 = new List<ZoneDto>();
				CustomDbContext customDbContext1 = new CustomDbContext();
				foreach (Zone zone1 in listAsync1)
				{
					ZoneDto zoneDto1 = zone1.MapTo<ZoneDto>();
					object[] objArray = new object[] { new SqlParameter("@ZoneId", (object)zoneDto1.Id), new SqlParameter("@CompanyAddressId", (object)tenantDetail1.Id), null };
					SqlParameter sqlParameter2 = new SqlParameter()
					{
						ParameterName = "@retVal",
						SqlDbType = SqlDbType.Int,
						Direction = ParameterDirection.Output,
						Value = 0
					};
					objArray[2] = sqlParameter2;
					if (customDbContext1.Database.SqlQuery<int>("exec @retVal = Geo_CompanyAddressIdInZone @ZoneId, @CompanyAddressId", objArray).Single<int>() != 1)
					{
						continue;
					}
					zoneDtos1.Add(zoneDto1);
				}
				customDbContext1.Dispose();
				zoneListDtos = new List<ZoneListDto>(zoneDtos1.MapTo<List<ZoneListDto>>());
			}
			return zoneListDtos;
		}

		public async Task<List<ZoneListDto>> GetZonesByTenantId(int tenantId, bool active)
		{
			IQueryable<Zone> all = this._zoneRepository.GetAll();
			IQueryable<Zone> zones = 
				from p in all
				where p.TenantId == tenantId
				select p;
			bool flag = active;
			List<Zone> listAsync = await zones.WhereIf<Zone>(flag, (Zone p) => p.IsActive == active).OrderBy<Zone>("Name", new object[0]).ToListAsync<Zone>();
			return new List<ZoneListDto>(listAsync.MapTo<List<ZoneListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Zones.ExportData" })]
		public async Task<FileDto> GetZonesToExcel()
		{
			List<Zone> allListAsync = await this._zoneRepository.GetAllListAsync();
			List<ZoneListDto> zoneListDtos = allListAsync.MapTo<List<ZoneListDto>>();
			return this._zoneListExcelExporter.ExportToFile(zoneListDtos);
		}

		public async Task<List<TaxDto>> GetZoneTaxesByAddressId(int tenantId, long addressId, bool active)
		{
			List<TaxDto> taxDtos;
			if (addressId != (long)-1)
			{
				Address async = await this._addressRepository.GetAsync(addressId);
				IQueryable<Zone> all = this._zoneRepository.GetAll();
				IQueryable<Zone> zones = 
					from p in all
					where p.TenantId == tenantId
					select p;
				bool flag = active;
				IQueryable<Zone> zones1 = zones.WhereIf<Zone>(flag, (Zone p) => p.IsActive == active);
				List<Zone> listAsync = await System.Data.Entity.QueryableExtensions.Include<Zone, ICollection<ZoneTax>>(zones1, (Zone n) => n.Taxes).OrderBy<Zone>("Name", new object[0]).ToListAsync<Zone>();
				List<ZoneDto> zoneDtos = new List<ZoneDto>();
				CustomDbContext customDbContext = new CustomDbContext();
				foreach (Zone zone in listAsync)
				{
					ZoneDto zoneDto = zone.MapTo<ZoneDto>();
					object[] sqlParameter = new object[] { new SqlParameter("@ZoneId", (object)zoneDto.Id), new SqlParameter("@AddressId", (object)async.Id), null };
					SqlParameter sqlParameter1 = new SqlParameter()
					{
						ParameterName = "@retVal",
						SqlDbType = SqlDbType.Int,
						Direction = ParameterDirection.Output,
						Value = 0
					};
					sqlParameter[2] = sqlParameter1;
					if (customDbContext.Database.SqlQuery<int>("exec @retVal = Geo_AddressIdInZone @ZoneId, @AddressId", sqlParameter).Single<int>() != 1)
					{
						continue;
					}
					zoneDtos.Add(zoneDto);
				}
				customDbContext.Dispose();
				List<ZoneDto> zoneDtos1 = new List<ZoneDto>(zoneDtos.MapTo<List<ZoneDto>>());
				List<TaxDto> taxDtos1 = new List<TaxDto>();
				List<long> nums = new List<long>();
				foreach (ZoneDto zoneDto1 in zoneDtos1)
				{
					foreach (ZoneTaxDto taxis in zoneDto1.Taxes)
					{
						TaxDto taxDto = taxis.Tax.MapTo<TaxDto>();
						if (nums.Contains(taxDto.Id))
						{
							continue;
						}
						nums.Add(taxDto.Id);
						taxDtos1.Add(taxDto);
					}
				}
				taxDtos = new List<TaxDto>(taxDtos1.Distinct<TaxDto>().MapTo<List<TaxDto>>());
			}
			else
			{
				IRepository<TenantDetail, long> repository = this._tenantDetailRepository;
				TenantDetail tenantDetail = await repository.FirstOrDefaultAsync((TenantDetail x) => x.TenantId == tenantId);
				TenantDetail tenantDetail1 = tenantDetail;
				IQueryable<Zone> all1 = this._zoneRepository.GetAll();
				IQueryable<Zone> zones2 = 
					from p in all1
					where p.TenantId == tenantId
					select p;
				bool flag1 = active;
				IQueryable<Zone> zones3 = zones2.WhereIf<Zone>(flag1, (Zone p) => p.IsActive == active);
				List<Zone> listAsync1 = await System.Data.Entity.QueryableExtensions.Include<Zone, ICollection<ZoneTax>>(zones3, (Zone n) => n.Taxes).OrderBy<Zone>("Name", new object[0]).ToListAsync<Zone>();
				List<ZoneDto> zoneDtos2 = new List<ZoneDto>();
				CustomDbContext customDbContext1 = new CustomDbContext();
				foreach (Zone zone1 in listAsync1)
				{
					ZoneDto zoneDto2 = zone1.MapTo<ZoneDto>();
					object[] objArray = new object[] { new SqlParameter("@ZoneId", (object)zoneDto2.Id), new SqlParameter("@CompanyAddressId", (object)tenantDetail1.Id), null };
					SqlParameter sqlParameter2 = new SqlParameter()
					{
						ParameterName = "@retVal",
						SqlDbType = SqlDbType.Int,
						Direction = ParameterDirection.Output,
						Value = 0
					};
					objArray[2] = sqlParameter2;
					if (customDbContext1.Database.SqlQuery<int>("exec @retVal = Geo_CompanyAddressIdInZone @ZoneId, @CompanyAddressId", objArray).Single<int>() != 1)
					{
						continue;
					}
					zoneDtos2.Add(zoneDto2);
				}
				customDbContext1.Dispose();
				List<ZoneDto> zoneDtos3 = new List<ZoneDto>(zoneDtos2.MapTo<List<ZoneDto>>());
				List<TaxDto> taxDtos2 = new List<TaxDto>();
				List<long> nums1 = new List<long>();
				foreach (ZoneDto zoneDto3 in zoneDtos3)
				{
					foreach (ZoneTaxDto zoneTaxDto in zoneDto3.Taxes)
					{
						TaxDto taxDto1 = zoneTaxDto.Tax.MapTo<TaxDto>();
						if (nums1.Contains(taxDto1.Id))
						{
							continue;
						}
						nums1.Add(taxDto1.Id);
						taxDtos2.Add(taxDto1);
					}
				}
				taxDtos = new List<TaxDto>(taxDtos2.Distinct<TaxDto>().MapTo<List<TaxDto>>());
			}
			return taxDtos;
		}

		public async Task<List<string>> ValidateUserCanDelete(long zoneId)
		{
			List<string> strs = new List<string>();
			IRepository<EmergencyDeliveryFee, long> repository = this._emergencyDeliveryFeeRepository;
			List<EmergencyDeliveryFee> allListAsync = await repository.GetAllListAsync((EmergencyDeliveryFee m) => m.ZoneId == (long?)zoneId);
			List<EmergencyDeliveryFee> emergencyDeliveryFees = allListAsync;
			if (emergencyDeliveryFees.Any<EmergencyDeliveryFee>())
			{
				List<EmergencyDeliveryFee> emergencyDeliveryFees1 = emergencyDeliveryFees;
				strs = (
					from s in emergencyDeliveryFees1
					select s.Name).ToList<string>();
			}
			return strs;
		}
	}
}