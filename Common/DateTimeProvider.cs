namespace Common
{
	public class DateTimeProvider : IDateTimeProvider
	{
		public DateTime GetUtcDateTimeNow()
		{
			return DateTime.UtcNow;
		}
	}
}

