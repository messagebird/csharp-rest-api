namespace MessageBird.Objects
{
    public interface IIdentifiable<T>
    {
        T Id { get; }
    }
}
