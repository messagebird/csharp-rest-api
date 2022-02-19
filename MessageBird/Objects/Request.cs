using System;
using System.Text;

namespace MessageBird.Objects
{
    /**
     * Structure used for request signature verification via MessageBird.RequestSigner
     * When user receive webhook, it should use this structure with
     *  * MessageBird-Request-Timestamp header
     *  * query string of request
     *  * and request body
     *
     * @see more on https://developers.messagebird.com/docs/verify-http-requests 
     */
    [Obsolete("Use RequestValidator instead.", false)]
    public class Request
    {
        internal string Timestamp { get; private set; }
        internal string QueryParameters { get; private set; }
        internal byte[] Data { get; private set; }

        private const char QueryParametersDelimiter = '&';

        /**
         * Constructs a new request instance.
         *
         * @param timestamp Timestamp provided in the MessageBird-Request-Timestamp
         *                  header.
         * @param queryParameters Query parameters in abc=foo&def=ghi format.
         * @param data Raw body of this request.
         */
        [Obsolete("Use RequestValidator instead.", false)]
        public Request(string timestamp, string queryParameters, byte[] data) {
            if (string.IsNullOrEmpty(timestamp)) {
                throw new ArgumentNullException("timestamp");
            }

            Timestamp = timestamp;
            QueryParameters = queryParameters;
            Data = data;
        }


        internal string SortedQueryParameters() {
            var queryParams = QueryParameters.Split(QueryParametersDelimiter);
            Array.Sort(queryParams);
            return string.Join(QueryParametersDelimiter.ToString(), queryParams);
        }
    }
}
