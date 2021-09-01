using VismaTest.Constants;
using VismaTest.Services.HttpConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTest.Services.WebServices
{
    public class BaseWebService
    {
        protected IHttpService httpService;
        public BaseWebService(IHttpService conection)
        {
            this.httpService = conection;
        }

        protected string BaseUrlServices(string url)
        {
            return ConfigConstants.baseUrl + url;
        }

    }
}
