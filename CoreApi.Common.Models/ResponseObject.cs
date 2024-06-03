using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApi.Common.Models
{
    public class ResponseObject<T>
    {
        public int rId { get; set; } // requestId
        public List<T>? data { get; set; } // return Object as a API response data
    }
}
