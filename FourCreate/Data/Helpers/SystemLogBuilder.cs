using Domain.Entities;
using Domain.Shared;
using Newtonsoft.Json;

namespace Data.Helpers
{
	internal class SystemLogBuilder
	{
        public string ResourceType { get; set; }

        public object ResourceId { get; set; }

        public object? ResourceUniqueValue { get; set; }

        public SystemLogType SystemLogType { get; set; }

        public DateTime CreatedAt { get; set; }

        private Dictionary<string, PropertyContainer> Changeset { get; } = new Dictionary<string, PropertyContainer>();

        public void AddPropertyValues(string propertyName, object previousValue, object currentValue)
        {
            Changeset[propertyName] = new PropertyContainer(previousValue, currentValue);
        }

        public SystemLog ToSystemLog()
        {
            var resourceId = JsonConvert.SerializeObject(ResourceId);
            string changedAttributes = JsonConvert.SerializeObject(Changeset);

            var comment = GenerateComment();

            return new SystemLog(ResourceType, resourceId, CreatedAt, SystemLogType, changedAttributes, comment);
        }

        private class PropertyContainer
        {
            public PropertyContainer(object previousValue, object currentValue)
            {
                PreviousValue = previousValue;
                CurrentValue = currentValue;
            }

            public object PreviousValue { get; set; }

            public object CurrentValue { get; set; }
        }

        private string GenerateComment()
        {
            var uniqueValueString = ResourceUniqueValue is not null ? $" {JsonConvert.SerializeObject(ResourceUniqueValue)}" : string.Empty;

            return SystemLogType switch
            {
                SystemLogType.Create => $"New {ResourceType.ToLower()}{uniqueValueString} was created.",
                SystemLogType.Delete => $"{ResourceType}{uniqueValueString} was deleted.",
                SystemLogType.Update => $"{ResourceType}{uniqueValueString} was updated.",
                _ => string.Empty
            };
        }
    }
}

