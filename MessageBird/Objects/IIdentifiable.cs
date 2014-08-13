namespace MessageBird.Objects
{
    /// <summary>
    /// Base for identifiable types with a specifcly typed `Id` field.
    /// Contravariant, see `IEnumerable` of `T` at http://msdn.microsoft.com/en-us/library/9eekhta0
    /// so you can pass more specified instance (like an `IIdentifiable` of `Balance`) to something 
    /// that expects a more generic (like `IIdentifiable` of `T`) instance.
    /// </summary>
    /// <typeparam name="T">Type for the `Id` field.</typeparam>
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}