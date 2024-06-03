using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Common.Models
{
    /// <summary>
    /// This model will be used for create, update and delete api result
    /// </summary>
    public class CUDResult
    {
        [JsonProperty("apiResult")]
        public bool apiResult { get; set; }
        [JsonProperty("msg")]
        public Int32? msg { get; set; }
        [JsonProperty("value1")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public String? value1 { get; set; }
        [JsonProperty("value2")]
        public Int32? value2 { get; set; }

    }
}
