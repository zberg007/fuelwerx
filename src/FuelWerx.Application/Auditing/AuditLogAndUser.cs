using Abp.Auditing;
using FuelWerx.Authorization.Users;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Auditing
{
	public class AuditLogAndUser
	{
		public Abp.Auditing.AuditLog AuditLog
		{
			get;
			set;
		}

		public FuelWerx.Authorization.Users.User User
		{
			get;
			set;
		}

		public AuditLogAndUser()
		{
		}
	}
}