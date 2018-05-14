using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Titles.Dto
{
	public class UpdateTitleImageInput
	{
		public Guid? ImageId
		{
			get;
			set;
		}

		public long TitleId
		{
			get;
			set;
		}

		public UpdateTitleImageInput()
		{
		}
	}
}