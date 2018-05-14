using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Dto
{
	public class FileDto : IDoubleWayDto, IInputDto, IDto, IValidate, IOutputDto
	{
		[Required]
		public string FileName
		{
			get;
			set;
		}

		[Required]
		public string FileToken
		{
			get;
			set;
		}

		[Required]
		public string FileType
		{
			get;
			set;
		}

		public FileDto()
		{
		}

		public FileDto(string fileName, string fileType)
		{
			this.FileName = fileName;
			this.FileType = fileType;
			this.FileToken = Guid.NewGuid().ToString("N");
		}
	}
}