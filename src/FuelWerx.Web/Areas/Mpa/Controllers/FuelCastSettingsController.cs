using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.FuelCastSettings;
using FuelWerx.FuelCastSettings.Dto;
using FuelWerx.Web.Areas.Mpa.Models.FuelCastSettings;
using FuelWerx.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] {  })]
	public class FuelCastSettingsController : FuelWerxControllerBase
	{
		private readonly IRepository<FuelCastSetting> _fuelCastSettingRepository;

		public FuelCastSettingsController(IRepository<FuelCastSetting> fuelCastSettingRepository)
		{
			this._fuelCastSettingRepository = fuelCastSettingRepository;
		}

		public async Task<ActionResult> Index()
		{
			int? impersonatorTenantId;
			int value;
			if (this.AbpSession.ImpersonatorTenantId.HasValue)
			{
				impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
				value = impersonatorTenantId.Value;
			}
			else
			{
				impersonatorTenantId = this.AbpSession.TenantId;
				value = impersonatorTenantId.Value;
			}
			int num = value;
			IRepository<FuelCastSetting> repository = this._fuelCastSettingRepository;
			List<FuelCastSetting> allListAsync = await repository.GetAllListAsync((FuelCastSetting m) => m.TenantId == num);
			List<FuelCastSetting> fuelCastSettings = allListAsync;
			GetSettingsOutput getSettingsOutput = new GetSettingsOutput()
			{
				Settings = new SettingsEditDto()
			};
			ManageSettings manageSetting = new ManageSettings(getSettingsOutput);
			if (fuelCastSettings.Count == 1)
			{
				manageSetting.Settings = fuelCastSettings[0].MapTo<SettingsEditDto>();
			}
			return this.View(manageSetting);
		}

		public async Task<ActionResult> Save(SettingsEditDto settings)
		{
			int? impersonatorTenantId;
			int value;
			if (!settings.Id.HasValue)
			{
				FuelCastSetting fuelCastSetting = settings.MapTo<FuelCastSetting>();
				FuelCastSetting fuelCastSetting1 = fuelCastSetting;
				if (this.AbpSession.ImpersonatorTenantId.HasValue)
				{
					impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
					value = impersonatorTenantId.Value;
				}
				else
				{
					impersonatorTenantId = this.AbpSession.TenantId;
					value = impersonatorTenantId.Value;
				}
				fuelCastSetting1.TenantId = value;
				await this._fuelCastSettingRepository.InsertAndGetIdAsync(fuelCastSetting);
			}
			else
			{
				IRepository<FuelCastSetting> repository = this._fuelCastSettingRepository;
				impersonatorTenantId = settings.Id;
				FuelCastSetting async = await repository.GetAsync(impersonatorTenantId.Value);
				if (async.AllowAnomolyModification)
				{
					settings.AllowAnomolyModification = true;
				}
				if (async.AllowAnomolyModificationDateTime.HasValue)
				{
					settings.AllowAnomolyModificationDateTime = new DateTime?(async.AllowAnomolyModificationDateTime.Value);
				}
				settings.MapTo<SettingsEditDto, FuelCastSetting>(async);
				await this._fuelCastSettingRepository.UpdateAsync(async);
			}
			ActionResult actionPermanent = this.RedirectToActionPermanent("Index", "FuelCastSettings", new { Area = "Mpa", Saved = "1" });
			return actionPermanent;
		}
	}
}