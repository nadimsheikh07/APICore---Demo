using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreApi.Common.Models
{
    public class CommonJsonParam
    {
        [JsonPropertyName("uId")]
        public int userId { get; set; }
        [JsonPropertyName("pId")]
        public int projectId { get; set; }
        
    }
    public class TaskData
    {
        public int? nTaskid { get; set; }
        public string? Consumernumber { get; set; }
        public string? jPreviousValue { get; set; }
    }
}
