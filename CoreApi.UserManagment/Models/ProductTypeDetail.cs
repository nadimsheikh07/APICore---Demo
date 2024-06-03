using CoreApi.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreApi.UserManagment.Models
{
    public class ProductTypeModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
    }
}
