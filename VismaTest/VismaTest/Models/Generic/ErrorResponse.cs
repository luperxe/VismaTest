using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTest.Models.Generic
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
