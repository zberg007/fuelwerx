using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using FuelWerx;
using FuelWerx.Administrative;
using FuelWerx.Administrative.Titles.Dto;
using FuelWerx.Administrative.Titles.Exporting;
using FuelWerx.Dto;
using FuelWerx.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.Titles
{
	[AbpAuthorize(new string[] { "Pages.Administration.Titles" })]
	public class TitleAppService : FuelWerxAppServiceBase, ITitleAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Title, long> _titleRepository;

		private readonly ITitleListExcelExporter _titleListExcelExporter;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public TitleAppService(IRepository<Title, long> titleRepository, ITitleListExcelExporter titleListExcelExporter, IBinaryObjectManager binaryObjectManager)
		{
			this._titleRepository = titleRepository;
			this._titleListExcelExporter = titleListExcelExporter;
			this._binaryObjectManager = binaryObjectManager;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Titles.Create", "Pages.Administration.Titles.Edit" })]
		public async Task<long> CreateOrUpdateTitle(CreateOrUpdateTitleInput input)
		{
			long value;
			if (!input.Title.Id.HasValue)
			{
				long num = await this._titleRepository.InsertAndGetIdAsync(input.Title.MapTo<Title>());
				value = num;
			}
			else
			{
				value = input.Title.Id.Value;
				await this._titleRepository.UpdateAsync(input.Title.MapTo<Title>());
			}
			return value;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Titles.Delete" })]
		public async Task DeleteTitle(IdInput<long> input)
		{
			await this.DeleteTitleImage(input);
			await this._titleRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Titles.Delete" })]
		private async Task<bool> DeleteTitleImage(IdInput<long> input)
		{
			Guid guid;
			Title async = await this._titleRepository.GetAsync(input.Id);
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

		[AbpAuthorize(new string[] { "Pages.Administration.Titles.Edit" })]
		public async Task<GetTitleForEditOutput> GetTitleForEdit(NullableIdInput<long> input)
		{
			TitleEditDto titleEditDto;
			if (!input.Id.HasValue)
			{
				titleEditDto = new TitleEditDto();
			}
			else
			{
				IRepository<Title, long> repository = this._titleRepository;
				Title async = await repository.GetAsync(input.Id.Value);
				titleEditDto = async.MapTo<TitleEditDto>();
			}
			return new GetTitleForEditOutput()
			{
				Title = titleEditDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Titles" })]
		public async Task<PagedResultOutput<TitleListDto>> GetTitles(GetTitlesInput input)
		{
			IQueryable<Title> all = this._titleRepository.GetAll();
			IQueryable<Title> titles = all.WhereIf<Title>(!input.Filter.IsNullOrEmpty(), (Title p) => p.Name.Contains(input.Filter) || p.Type.Contains(input.Filter));
			int num = await titles.CountAsync<Title>();
			List<Title> listAsync = await titles.OrderBy<Title>(input.Sorting, new object[0]).PageBy<Title>(input).ToListAsync<Title>();
			return new PagedResultOutput<TitleListDto>(num, listAsync.MapTo<List<TitleListDto>>());
		}

		public async Task<List<TitleListDto>> GetTitlesForTenant(long tenantId)
		{
			IQueryable<Title> all = this._titleRepository.GetAll();
			IQueryable<Title> titles = 
				from p in all
				where (long)p.TenantId == tenantId
				select p;
			List<Title> listAsync = await (
				from o in titles
				orderby o.Name
				select o).ToListAsync<Title>();
			return new List<TitleListDto>(listAsync.MapTo<List<TitleListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Titles.ExportData" })]
		public async Task<FileDto> GetTitlesToExcel()
		{
			List<Title> allListAsync = await this._titleRepository.GetAllListAsync();
			List<TitleListDto> titleListDtos = allListAsync.MapTo<List<TitleListDto>>();
			return this._titleListExcelExporter.ExportToFile(titleListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Titles.Create", "Pages.Administration.Titles.Edit" })]
		public async Task SaveTitleImageAsync(UpdateTitleImageInput input)
		{
			Title async = await this._titleRepository.GetAsync(input.TitleId);
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
			await this._titleRepository.UpdateAsync(async);
		}
	}
}