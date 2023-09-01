namespace Domain.Shared
{
    /// <summary>
    /// Enum of possible system log types.
    /// </summary>
    public enum SystemLogType
    {
        // start from 1, so default value of the underlying type - 0, won't be valid enum.
        Create = 1,
        Update,
        Delete
    }
}

