using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApi.Common.Models
{
    public class CommonData
    {

    }

    

 
    public class ResponseObjectTask<T>
    {
        public int rId { get; set; } // requestId
        public Task<ApiResponseModel>? data { get; set; } // return Object as a API response data
    }
    public class ApiResponseModel
    {
        public int StatusCode { get; set; }
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? ReasonPhrase { get; set; }
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? Data { get; set; }
    }

    
    public class CommonModel
    {
        public long id { get; set; }    // Id
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? v { get; set; }   // Value
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? nm { get; set; }  // Name
        public int pid { get; set; }   // ParentId
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? jtc { get; set; }   // JobTypeCode
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? mk { get; set; }   // masterkey
    }

  

   
     
    
 
  
     

    

    

    public class EventTamperBitmaskMaster
    {
        public int pos { get; set; } //setbitpos
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? descp { get; set; } //event description
    }

    public class MobileAppInfo
    {
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? cav { get; set; } // currApkVersion
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? mav { get; set; } // minApkVersion
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? dp { get; set; } // downloadPath
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? asi { get; set; } // size
    }

    

}
