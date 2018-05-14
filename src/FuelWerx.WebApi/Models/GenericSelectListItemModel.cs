using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.WebApi.Models
{
	public class GenericSelectListItemModel
	{
		public bool Disabled
		{
			get;
			set;
		}

		public string Group
		{
			get;
			set;
		}

		public bool Selected
		{
			get;
			set;
		}

		[Required]
		public string Text
		{
			get;
			set;
		}

		[Required]
		public string Value
		{
			get;
			set;
		}

		public GenericSelectListItemModel()
		{
		}
	}
}