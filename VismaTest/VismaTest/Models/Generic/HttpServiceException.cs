using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTest.Models.Generic
{
    public class HttpServiceException<T> : Exception
    {
        /// <summary>
        /// Initializes a new instance of the HttpServiceException<typeparamref name="T"/> with a specific status code and response.  
        /// </summary>
        /// <param name="responseStatusCode">HTTP status code returned into HttpResponseMessage. <see cref="HttpStatusCode"/></param>
        /// <param name="textResponse">String content returned into HttpResponseMessage.</param>
        public HttpServiceException(int responseStatusCode, string textResponse) : base(textResponse)
        {
            this.ResponseStatusCode = responseStatusCode;
            Error = JsonConvert.DeserializeObject<T>(textResponse);
        }

        /// <summary>
        /// Gets or sets the desiarilized response error.
        /// </summary>
        public T Error { get; private set; }

        /// <summary>Gets or sets the HTTP status code returned in the response.</summary>
        public int ResponseStatusCode { get; private set; }
    }
}
