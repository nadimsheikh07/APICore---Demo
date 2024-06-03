using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CoreApi.Common.Models
{
    public class ErrorLogModel
    {
        [JsonPropertyName("eId")]
        public int errorId { get; set; }
        [JsonPropertyName("rId")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? requestId { get; set; }
        [JsonPropertyName("eDesc")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? errorDescription { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
