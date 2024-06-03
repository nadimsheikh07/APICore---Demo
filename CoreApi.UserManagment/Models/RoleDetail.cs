using CoreApi.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreApi.UserManagment.Models
{
    /// <summary>
    /// data model for role detail.
    /// </summary>
    public class RoleDetail
    {
        [JsonPropertyName("rId")]
        public int roleId { get; set; }
        [JsonPropertyName("phId")]
        public int projectHierarchyId { get; set; }
        [JsonPropertyName("prId")]
        public int parentRoleId { get; set; }
        [JsonPropertyName("prjHrchy")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? projectHierarchy { get; set; }
        [JsonPropertyName("rName")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? roleName { get; set; }
        [JsonPropertyName("perm")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? perm { get; set; }
        [JsonPropertyName("pId")]
        public int projectId { get; set; }
        [JsonPropertyName("uId")]
        public int userId { get; set; }
    }
    public class ParentRoleList
    {
        [JsonPropertyName("prId")]
        public int pRoleId { get; set; }
        [JsonPropertyName("prName")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(int.MaxValue)]
        [RegularExpression(Constants.StringRegularExpression)]
        public string? pRoleName { get; set; }
    }
    //public class CityModel
    //{
    //    public int cityID { get; set; }
    //    public string? CityName { get; set; }
    //}
    }
