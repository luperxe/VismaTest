using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTest.Models.Generic
{
    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
