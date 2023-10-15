using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagementService.Application.Helpers
{
    public class DmsException : Exception
    {
        public string Code { get; }
        public int StatusCode { get; }

        public DmsException(string code, int statusCode, string message) : base(message)
        {
            Code = code;
            StatusCode = statusCode;
        }
    }
}