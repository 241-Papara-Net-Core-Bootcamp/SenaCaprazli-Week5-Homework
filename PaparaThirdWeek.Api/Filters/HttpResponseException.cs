using System;

namespace PaparaThirdWeek.Api.Filters
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; }
        public object Value { get; set; }
        public HttpResponseException(string value)
        {
            Value = value;
        }
    }
}
