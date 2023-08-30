namespace Domain.Shared
{
	public class AggregateRoot<T>
	{
		public AggregateRoot(T id)
		{
			Id = id;
		}

		public T Id { get; set; }
	}
}

