using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using FuelWerx;
using FuelWerx.Assets.FillLots.Dto;
using FuelWerx.Assets.FillLots.Exporting;
using FuelWerx.Dto;
using FuelWerx.FillLots;
using FuelWerx.FillLotTanks;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Assets.FillLots
{
	public class FillLotAppService : FuelWerxAppServiceBase, IFillLotAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<FillLot, long> _fillLotRepository;

		private readonly IRepository<Address, long> _addressRepository;

		private readonly IRepository<CountryRegion> _countryRegionRepository;

		private readonly IRepository<Country> _countryRepository;

		private readonly IRepository<FillLotTank, long> _fillLotTankRepository;

		private readonly IFillLotListExcelExporter _fillLotListExcelExporter;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public FillLotAppService(IRepository<FillLot, long> fillLotRepository, IRepository<Address, long> addressRepository, IRepository<FillLotTank, long> fillLotTankRepository, IFillLotListExcelExporter fillLotListExcelExporter, IBinaryObjectManager binaryObjectManager, IRepository<CountryRegion> countryRegionRepository, IRepository<Country> countryRepository)
		{
			this._fillLotRepository = fillLotRepository;
			this._addressRepository = addressRepository;
			this._fillLotTankRepository = fillLotTankRepository;
			this._fillLotListExcelExporter = fillLotListExcelExporter;
			this._binaryObjectManager = binaryObjectManager;
			this._countryRegionRepository = countryRegionRepository;
			this._countryRepository = countryRepository;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.FillLots.Create", "Pages.Tenant.FillLots.Edit" })]
        public async Task<long> CreateOrUpdateFillLot(CreateOrUpdateFillLotInput input)
        {
            if (!input.FillLot.AddressId.HasValue || (input.FillLot.AddressId.HasValue && input.FillLot.AddressId.Value == 0L))
            {
                input.FillLot.AddressId = null;
            }
            bool isNew;
            long fillLotId = 0;
            if (input.FillLot.Id.HasValue)
            {
                if (!PermissionChecker.IsGranted("Pages.Tenant.FillLots.Edit"))
                {
                    throw new UserFriendlyException(L("Permissions_UserNotAuthorizedMessage"));
                }
                isNew = false;
                FillLot fillLot = new FillLot();
                Mapper.Map<FillLotEditDto, FillLot>(input.FillLot, fillLot);
                fillLot.Tanks = null;
                await _fillLotRepository.UpdateAsync(fillLot);
                fillLotId = fillLot.Id;
            }
            else
            {
                if (!PermissionChecker.IsGranted("Pages.Tenant.FillLots.Create"))
                {
                    throw new UserFriendlyException(L("Permissions_UserNotAuthorizedMessage"));
                }
                isNew = true;
                FillLot newFillLot = new FillLot();
                Mapper.Map<FillLotEditDto, FillLot>(input.FillLot, newFillLot);
                fillLotId = await this._fillLotRepository.InsertAndGetIdAsync(newFillLot);
            }
            var fillLotTanks = await _fillLotTankRepository.GetAllListAsync(m => m.FillLotId == fillLotId);
            if (input.FillLot.Tanks.Any())
            {
                if (!isNew)
                {
                    if (fillLotTanks.Any())
                    {
                        var existingTankIdList = fillLotTanks.Select(t => t.Id).ToList();
                        foreach (var inputTank in input.FillLot.Tanks)
                        {
                            if(inputTank.Id > 0)
                            {
                                var existing = await _fillLotTankRepository.GetAsync(inputTank.Id);
                                existing.Name = inputTank.Name;
                                existing.Number = inputTank.Number;
                                existing.Capacity = inputTank.Capacity;
                                existing.RemainingCapacity = inputTank.RemainingCapacity;
                                existing.Description = inputTank.Description;
                                existing.LastInspectionComments = inputTank.LastInspectionComments;
                                existing.LastInspectionDate = inputTank.LastInspectionDate;
                                existing.IsActive = inputTank.IsActive;
                                await this._fillLotTankRepository.UpdateAsync(existing);
                            }
                            else
                            {
                                inputTank.FillLotId = fillLotId;
                                await _fillLotTankRepository.InsertAndGetIdAsync(inputTank);
                            }
                        }
                        if(existingTankIdList.Any())
                        {
                            await _fillLotTankRepository.DeleteAsync(m => m.FillLotId == fillLotId && !existingTankIdList.Contains(m.Id));
                        }
                    }
                    else
                    {
                        foreach (var inputTank in input.FillLot.Tanks)
                        {
                            inputTank.FillLotId = fillLotId;
                            await _fillLotTankRepository.InsertAsync(inputTank);
                        }
                    }
                }
            }
            else if (!input.FillLot.Tanks.Any() && fillLotTanks.Any())
            {
                var existingTankIdListToDelete = fillLotTanks.Select(x => x.Id);
                await _fillLotTankRepository.DeleteAsync(x => x.FillLotId == fillLotId && !existingTankIdListToDelete.Contains(x.Id));
            }
            return fillLotId;
        }

        [AbpAuthorize(new string[] { "Pages.Tenant.FillLots.Delete" })]
		public async Task DeleteFillLot(IdInput<long> input)
		{
			await this._fillLotRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.FillLots" })]
		public async Task<FillLot> GetFillLot(long fillLotId)
		{
			return await this._fillLotRepository.GetAsync(fillLotId);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.FillLots.Create", "Pages.Tenant.FillLots.Edit" })]
		public async Task<GetFillLotForEditOutput> GetFillLotForEdit(NullableIdInput<long> input)
		{
			long? id;
			FillLotEditDto fillLotEditDto;
			if (!input.Id.HasValue)
			{
				fillLotEditDto = new FillLotEditDto()
				{
					Tanks = new List<FillLotTank>()
				};
			}
			else
			{
				IRepository<FillLot, long> repository = this._fillLotRepository;
				id = input.Id;
				FillLot async = await repository.GetAsync(id.Value);
				fillLotEditDto = async.MapTo<FillLotEditDto>();
				FillLotEditDto fillLotEditDto1 = fillLotEditDto;
				IRepository<FillLotTank, long> repository1 = this._fillLotTankRepository;
				List<FillLotTank> allListAsync = await repository1.GetAllListAsync((FillLotTank x) => x.FillLotId == async.Id);
				fillLotEditDto1.Tanks = allListAsync;
				fillLotEditDto1 = null;
			}
			if (!fillLotEditDto.Id.HasValue || !fillLotEditDto.AddressId.HasValue)
			{
				fillLotEditDto.Address = new AddressDto();
			}
			else
			{
				IRepository<Address, long> repository2 = this._addressRepository;
				id = fillLotEditDto.AddressId;
				Address address = await repository2.GetAsync(id.Value);
				fillLotEditDto.Address = address.MapTo<AddressDto>();
			}
			return new GetFillLotForEditOutput()
			{
				FillLot = fillLotEditDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.FillLots" })]
		public async Task<PagedResultOutput<FillLotListDto>> GetFillLots(GetFillLotsInput input)
		{
			//FillLotAppService.<>c__DisplayClass8_1 variable = null;
			//FillLotAppService.<>c__DisplayClass8_0 variable1 = null;
			//FillLotAppService.<>c__DisplayClass8_2 variable2 = null;
			int fillLotId;
			IQueryable<FillLot> all = _fillLotRepository.GetAll();
            var fillLots = all.Where(p => p.TenantId == AbpSession.TenantId && p.Id == 0); // Really?
            var listAsync = await fillLots.Select(s => new
            {
                FillLotId = s.Id
            }).ToListAsync();
			var collection = listAsync;
			List<long> foundFillLotIdsFromInputFilter = (
				from s in collection
				select s.FillLotId).ToList();
			bool foundUsingIdFilter = false;
			if (input.Filter.ToLower().StartsWith("id:"))
			{
				try
				{
					string lower = input.Filter.ToLower();
					char[] chrArray = new char[] { ':' };
					int.TryParse(lower.Split(chrArray)[1].ToString(), out fillLotId);
					IQueryable<FillLot> all1 = _fillLotRepository.GetAll();
                    var fillLots1 = all1.Where(p => p.TenantId == AbpSession.TenantId && p.Id == fillLotId);

					var listAsync1 = await fillLots1.Select(s => new
                    {
                        FillLotId = s.Id
                    }).ToListAsync();
					foundUsingIdFilter = true;
					foundFillLotIdsFromInputFilter = foundFillLotIdsFromInputFilter.Union(
						from s in listAsync1
                        select s.FillLotId).ToList();
				}
				catch (Exception)
				{
				}
			}
			if (!foundUsingIdFilter)
			{
				IQueryable<FillLot> all2 = _fillLotRepository.GetAll();
                var fillLots2 = all2.WhereIf(!input.Filter.IsNullOrEmpty(), p =>
                    p.Label.Contains(input.Filter) ||
                    p.ShortLabel.Contains(input.Filter) ||
                    p.Description.Contains(input.Filter));
				var listAsync2 = await fillLots2.Select(s => new
                {
                    FillLotId = s.Id
                }).ToListAsync();
				foundFillLotIdsFromInputFilter = foundFillLotIdsFromInputFilter.Union(
					from s in listAsync2
                    select s.FillLotId).ToList();
			}
			IQueryable<FillLot> all3 = this._fillLotRepository.GetAll();
            var fillLots3 = all3.Where(m => foundFillLotIdsFromInputFilter.Contains(m.Id));
			int resultCount = await fillLots3.CountAsync();
			List<FillLot> orderedResult = await fillLots3.OrderBy(input.Sorting, new object[0]).PageBy(input).ToListAsync();
			List<FillLotListDto> fillLotListDtos = orderedResult.MapTo<List<FillLotListDto>>();
			foreach (FillLotListDto countryDto in fillLotListDtos)
			{
				FillLotListDto fillLotListDto = countryDto;
                int tankTotal = await _fillLotTankRepository.CountAsync(m => m.FillLotId == countryDto.Id);
				fillLotListDto.TankTotal = tankTotal;
				fillLotListDto = null;
				if (countryDto.AddressId > 0)
				{
					Address async = await _addressRepository.GetAsync(countryDto.AddressId);
					countryDto.Address = async.MapTo<AddressDto>();
					if (countryDto.Address.CountryId <= 0)
					{
						countryDto.Address.Country = new CountryDto();
					}
					else
					{
						Country country = await this._countryRepository.GetAsync(countryDto.Address.CountryId);
						countryDto.Address.Country = country.MapTo<CountryDto>();
					}
					if (!countryDto.Address.CountryRegionId.HasValue)
					{
						countryDto.Address.CountryRegion = new CountryRegionDto();
					}
					else
					{
						IRepository<CountryRegion> repository1 = this._countryRegionRepository;
						int? countryRegionId = countryDto.Address.CountryRegionId;
						CountryRegion countryRegion = await repository1.GetAsync(countryRegionId.Value);
						countryDto.Address.CountryRegion = countryRegion.MapTo<CountryRegionDto>();
					}
				}
			}
			return new PagedResultOutput<FillLotListDto>(resultCount, fillLotListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.FillLots.ExportData" })]
		public async Task<FileDto> GetFillLotsToExcel()
		{
			List<FillLot> allListAsync = await this._fillLotRepository.GetAllListAsync();
			List<FillLotListDto> fillLotListDtos = allListAsync.MapTo<List<FillLotListDto>>();
			return this._fillLotListExcelExporter.ExportToFile(fillLotListDtos);
		}
	}
}