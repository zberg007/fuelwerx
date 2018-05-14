using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Contacts.Dto
{
	public class UpdateContactImageInput
	{
		public long ContactId
		{
			get;
			set;
		}

		public Guid? ImageId
		{
			get;
			set;
		}

		public UpdateContactImageInput()
		{
		}
	}
}