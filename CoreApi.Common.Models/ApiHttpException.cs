using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CoreApi.Common.Models
{
    [Serializable()]
    public class ApiHttpException : Exception, ISerializable
    {
        //[jsonpropertyname("msg")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? message { get; set; }
       //[jsonpropertyname("eid")]
        public int errorId { get; set; }

    }
}
