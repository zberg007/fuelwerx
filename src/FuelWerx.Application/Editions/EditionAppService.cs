using Abp;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using FuelWerx;
using FuelWerx.Editions.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Editions
{
	[AbpAuthorize(new string[] { "Pages.Editions" })]
	public class EditionAppService : FuelWerxAppServiceBase, IEditionAppService, IApplicationService, ITransientDependency
	{
		private readonly EditionManager _editionManager;

		public EditionAppService(EditionManager editionManager)
		{
			this._editionManager = editionManager;
		}

		[AbpAuthorize(new string[] { "Pages.Editions.Create" })]
		protected virtual async Task CreateEditionAsync(CreateOrUpdateEditionDto input)
		{
			Edition edition = new Edition(input.Edition.DisplayName);
			await this._editionManager.CreateAsync(edition);
			await this.CurrentUnitOfWork.SaveChangesAsync();
			await this.SetFeatureValues(edition, input.FeatureValues);
		}

		[AbpAuthorize(new string[] { "Pages.Editions.Create", "Pages.Editions.Edit" })]
		public async Task CreateOrUpdateEdition(CreateOrUpdateEditionDto input)
		{
			if (input.Edition.Id.HasValue)
			{
				await this.UpdateEditionAsync(input);
			}
			else
			{
				await this.CreateEditionAsync(input);
			}
		}

		[AbpAuthorize(new string[] { "Pages.Editions.Delete" })]
		public async Task DeleteEdition(EntityRequestInput input)
		{
			Edition byIdAsync = await this._editionManager.GetByIdAsync(input.Id);
			await this._editionManager.DeleteAsync(byIdAsync);
		}

		[AbpAuthorize(new string[] { "Pages.Editions.Create", "Pages.Editions.Edit" })]
		public async Task<GetEditionForEditOutput> GetEditionForEdit(NullableIdInput input)
		{
			EditionEditDto editionEditDto;
			List<NameValue> list;
			IReadOnlyList<Feature> all = this.FeatureManager.GetAll();
			if (!input.Id.HasValue)
			{
				editionEditDto = new EditionEditDto();
				IReadOnlyList<Feature> features = all;
				list = (
					from f in features
					select new NameValue(f.Name, f.DefaultValue)).ToList<NameValue>();
			}
			else
			{
				EditionManager editionManager = this._editionManager;
				int? id = input.Id;
				Edition edition = await editionManager.FindByIdAsync(id.Value);
				EditionManager editionManager1 = this._editionManager;
				id = input.Id;
				IReadOnlyList<NameValue> featureValuesAsync = await editionManager1.GetFeatureValuesAsync(id.Value);
				list = featureValuesAsync.ToList<NameValue>();
				editionEditDto = edition.MapTo<EditionEditDto>();
				edition = null;
			}
			GetEditionForEditOutput getEditionForEditOutput = new GetEditionForEditOutput()
			{
				Edition = editionEditDto
			};
			List<FlatFeatureDto> flatFeatureDtos = all.MapTo<List<FlatFeatureDto>>();
			getEditionForEditOutput.Features = (
				from f in flatFeatureDtos
				orderby f.DisplayName
				select f).ToList<FlatFeatureDto>();
			List<NameValue> nameValues = list;
			getEditionForEditOutput.FeatureValues = (
				from fv in nameValues
				select new NameValueDto(fv)).ToList<NameValueDto>();
			return getEditionForEditOutput;
		}

		public async Task<ListResultOutput<EditionListDto>> GetEditions()
		{
			List<Edition> listAsync = await this._editionManager.Editions.ToListAsync<Edition>();
			return new ListResultOutput<EditionListDto>(listAsync.MapTo<List<EditionListDto>>());
		}

		private Task SetFeatureValues(Edition edition, List<NameValueDto> featureValues)
		{
			return this._editionManager.SetFeatureValuesAsync(edition.Id, (
				from fv in featureValues
				select new NameValue(fv.Name, fv.Value)).ToArray<NameValue>());
		}

		[AbpAuthorize(new string[] { "Pages.Editions.Edit" })]
		protected virtual async Task UpdateEditionAsync(CreateOrUpdateEditionDto input)
		{
			EditionManager editionManager = this._editionManager;
			int? id = input.Edition.Id;
			Edition byIdAsync = await editionManager.GetByIdAsync(id.Value);
			byIdAsync.DisplayName = input.Edition.DisplayName;
			await this.SetFeatureValues(byIdAsync, input.FeatureValues);
		}
	}
}