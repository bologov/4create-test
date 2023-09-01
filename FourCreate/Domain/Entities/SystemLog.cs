using Domain.Shared;

namespace Domain.Entities
{
	public class SystemLog : Entity<int>
	{
		public SystemLog(string resourceType, string resourceId, DateTime createdAt, SystemLogType type, string changes, string comment) : base (createdAt)
		{
			ResourceType = resourceType;
			ResourceId = resourceId;
            Type = type;
            Changes = changes;
			Comment = comment;
		}

		public string ResourceType { get; init; }

		public string ResourceId { get; init; }

		public SystemLogType Type { get; init; }

		public string Changes { get; init; }

		public string Comment { get; init; }
	}
}

