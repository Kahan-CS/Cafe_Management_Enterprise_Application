using AdminClient.Entities;
using AdminClient.Shared.Enums;
using System.Data;

namespace AdminClient.Shared
{
	public static class EnumFunctions
	{
		public static string ToString(this OrderStatus status)
		{
			return status switch
			{
				OrderStatus.Pending      => "Pending",
				OrderStatus.InProgress   => "In Progress",
				OrderStatus.Ready        => "Ready for Pickup",
				OrderStatus.Completed    => "Completed",
				OrderStatus.Cancelled    => "Cancelled",
				_ => status.ToString()
			};
		}

		public static string ToString(this InvitationStatus status)
		{
			return status switch
			{
				InvitationStatus.InviteNotSent  => "Invite Not Sent",
				InvitationStatus.InviteSent     => "Invite Sent",
				InvitationStatus.RespondedYes   => "Responded Yes",
				InvitationStatus.RespondedNo    => "Responded No",
				_ => status.ToString()
			};
		}

		public static string ToString(this UserRole role)
		{
			return role switch
			{
				UserRole.Admin          => "Administrator",
				UserRole.Customer       => "Customer",
				_ => role.ToString()
			};
		}
	}
}
