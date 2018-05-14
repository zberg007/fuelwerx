using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using FuelWerx;
using FuelWerx.Assets.Trucks.Dto;
using FuelWerx.Assets.Trucks.Exporting;
using FuelWerx.Dto;
using FuelWerx.Storage;
using FuelWerx.Trucks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Assets.Trucks
{
	[AbpAuthorize(new string[] { "Pages.Tenant.Trucks" })]
	public class TruckAppService : FuelWerxAppServiceBase, ITruckAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Truck, long> _truckRepository;

		private readonly IBinaryObjectManager _binaryObjectManager;

		private readonly ITruckListExcelExporter _truckListExcelExporter;

		public TruckAppService(IRepository<Truck, long> truckRepository, IBinaryObjectManager binaryObjectManager, ITruckListExcelExporter truckListExcelExporter)
		{
			this._truckRepository = truckRepository;
			this._binaryObjectManager = binaryObjectManager;
			this._truckListExcelExporter = truckListExcelExporter;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Trucks.Create", "Pages.Tenant.Trucks.Edit" })]
		public async Task CreateOrUpdateTruck(CreateOrUpdateTruckInput input)
		{
			if (!input.Truck.Id.HasValue)
			{
				await this._truckRepository.InsertAsync(input.Truck.MapTo<Truck>());
			}
			else
			{
				await this._truckRepository.UpdateAsync(input.Truck.MapTo<Truck>());
			}
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Trucks.Delete" })]
		public async Task DeleteTruck(IdInput<long> input)
		{
			await this.DeleteTruckImage(input);
			await this._truckRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Trucks.Delete" })]
		public async Task<bool> DeleteTruckImage(IdInput<long> input)
		{
			Guid guid;
			Truck async = await this._truckRepository.GetAsync(input.Id);
			Guid? imageId = async.ImageId;
			if (imageId.HasValue)
			{
				imageId = async.ImageId;
				if (Guid.TryParse(imageId.ToString(), out guid))
				{
					await this._binaryObjectManager.DeleteAsync(guid);
					imageId = null;
					async.ImageId = imageId;
				}
			}
			return true;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Trucks.Create", "Pages.Tenant.Trucks.Edit" })]
		public async Task<GetTruckForEditOutput> GetTruckForEdit(NullableIdInput<long> input)
		{
			TruckEditDto truckEditDto;
			if (!input.Id.HasValue)
			{
				truckEditDto = new TruckEditDto();
			}
			else
			{
				IRepository<Truck, long> repository = this._truckRepository;
				Truck async = await repository.GetAsync(input.Id.Value);
				truckEditDto = async.MapTo<TruckEditDto>();
			}
			return new GetTruckForEditOutput()
			{
				Truck = truckEditDto
			};
		}

		public async Task<PagedResultOutput<TruckListDto>> GetTrucks(GetTrucksInput input)
		{
			IQueryable<Truck> all = this._truckRepository.GetAll();
			IQueryable<Truck> trucks = all.WhereIf<Truck>(!input.Filter.IsNullOrEmpty(), (Truck p) => p.Name.Contains(input.Filter) || p.Description.Contains(input.Filter) || p.Number.Contains(input.Filter));
			int num = await trucks.CountAsync<Truck>();
			List<Truck> listAsync = await trucks.OrderBy<Truck>(input.Sorting, new object[0]).PageBy<Truck>(input).ToListAsync<Truck>();
			return new PagedResultOutput<TruckListDto>(num, listAsync.MapTo<List<TruckListDto>>());
		}

		public async Task<ListResultDto<TruckListDto>> GetTrucksByTenantId(int tenantId, bool active)
		{
			IQueryable<Truck> all = this._truckRepository.GetAll();
			IQueryable<Truck> trucks = 
				from p in all
				where p.TenantId == tenantId
				select p;
			bool flag = active;
			List<Truck> listAsync = await trucks.WhereIf<Truck>(flag, (Truck p) => p.IsActive == active).OrderBy<Truck>("Number", new object[0]).ToListAsync<Truck>();
			return new ListResultDto<TruckListDto>(listAsync.MapTo<List<TruckListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Trucks.ExportData" })]
		public async Task<FileDto> GetTrucksToExcel()
		{
			List<Truck> allListAsync = await this._truckRepository.GetAllListAsync();
			List<TruckListDto> truckListDtos = allListAsync.MapTo<List<TruckListDto>>();
			return this._truckListExcelExporter.ExportToFile(truckListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Trucks.Create", "Pages.Tenant.Trucks.Edit" })]
		public async Task SaveImageAsync(UpdateTruckImageInput input)
		{
			Truck async = await this._truckRepository.GetAsync(input.TruckId);
			Guid? imageId = input.ImageId;
			if (!imageId.HasValue)
			{
				imageId = null;
				async.ImageId = imageId;
			}
			else
			{
				imageId = input.ImageId;
				async.ImageId = new Guid?(imageId.Value);
			}
			await this._truckRepository.UpdateAsync(async);
		}
	}
}