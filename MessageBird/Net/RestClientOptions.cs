namespace MessageBird.Net
{
    /// <summary>
    /// Holds additional options for RestClient.
    /// </summary>
    public class RestClientOptions
    {
        /// <summary>
        /// Determines what HTTP method should be used for updates: some API's
        /// use PATCH and others require PUT. We can unfortunately not just add
        /// a Patch method or set this property on this class. That would be a
        /// breaking change: we can't just change the public interface.
        /// </summary>
        public static UpdateMode UpdateMode { get; set; }
    }
    
    public enum UpdateMode
    {
        Patch,

        Put,
    }
}
