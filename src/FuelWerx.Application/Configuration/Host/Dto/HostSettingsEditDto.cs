using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Host.Dto
{
	public class HostSettingsEditDto : IDoubleWayDto, IInputDto, IDto, IValidate, IOutputDto
	{
		[Required]
		public EmailSettingsEditDto Email
		{
			get;
			set;
		}

		[Required]
		public GeneralSettingsEditDto General
		{
			get;
			set;
		}

		[Required]
		public HostUserManagementSettingsEditDto UserManagement
		{
			get;
			set;
		}

		public HostSettingsEditDto()
		{
		}
	}
}