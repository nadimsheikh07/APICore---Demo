using CoreApi.Common.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreApi.UserManagment.Models
{
    public class UserLogInDetail
    {
        [JsonPropertyName("msg")]
        public int msg { get; set; }
        [JsonPropertyName("value1")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? value1 { get; set; }
        [JsonPropertyName("cusername")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? cuserName { get; set; }
        [JsonPropertyName("cpassword")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? ps { get; set; }
        [JsonPropertyName("nroleid")]
        public int nRoleId { get; set; }
        [JsonPropertyName("nuserid")]
        public int nuserId { get; set; }
        [JsonPropertyName("nprojectid")]
        public int nprojectId { get; set; }
        [JsonPropertyName("nhierarchyid")]
        public int nhierarchyId { get; set; }
        [JsonPropertyName("rolename")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? roleName { get; set; }
        [JsonPropertyName("projectname")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? projectName { get; set; }
        [JsonPropertyName("nfueltype")]
        public int nfuelType { get; set; }
        [JsonPropertyName("authenticationKey")]
        public string? authenticationKey { get; set; }
    }

    public class userMenuDetail
    {
        [JsonPropertyName("nid")]
        public int nid { get; set; }
        [JsonPropertyName("nindex")]
        public int nindex { get; set; }
        [JsonPropertyName("cparentnode")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? cparentnode { get; set; }
        [JsonPropertyName("cpagetitle")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? cpagetitle { get; set; }
        [JsonPropertyName("cpagename")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? cpagename { get; set; }
        [JsonPropertyName("cpermission")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? cpermission { get; set; }
        [JsonPropertyName("pgname")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? pgname { get; set; }
        [JsonPropertyName("childindex")]
        public int childindex { get; set; }
        [JsonPropertyName("menuURL")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? menuURL { get; set; }
    }
}
