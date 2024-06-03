using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using CoreApi.UserManagment.Business.Interfaces;
using CoreApi.Common.Models;
using CoreApi.UserManagment.Models;
using System.Net.NetworkInformation;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeManagementController : ControllerBase
    {
        IProductTypeManager _ProductTypeProvider;
        string AuthenticationKey = string.Empty;
        private int ID;
        private string Name = "";

        public ProductTypeManagementController(IProductTypeManager ProductTypeConfigurationProvider)
        {
            _ProductTypeProvider = ProductTypeConfigurationProvider;
        }

        [HttpPost]
        [Route("getAllProductTypeObject")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<ProductTypeModel>))]
        //[ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<ProductTypeModel> getAllProductTypeObject([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<ProductTypeModel> rObj = new ResponseObject<ProductTypeModel>();
            rObj.rId = new CommonMethods().getRequestId(Request);
            _ProductTypeProvider.getAllProductTypeObject(AuthenticationKey, rObj);
            return rObj;
        }


        [HttpPost]
        [Route("addNewProductTypeMaster")]
        public int addNewProductTypeMaster([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<int> rObj = new ResponseObject<int>();
            Name = new CommonMethods().getBodyParameters(json, "Name");
            int result = _ProductTypeProvider.addNewProductTypeMaster(AuthenticationKey, Name);
            return result;
        }

        [HttpPost]
        [Route("updateProductTypeMaster")]
        public int updateProductTypeMaster([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<int> rObj = new ResponseObject<int>();

            ID = int.Parse(new CommonMethods().getBodyParameters(json, "ID"));
            Name = new CommonMethods().getBodyParameters(json, "Name");
            int result = _ProductTypeProvider.updateProductTypeMaster(AuthenticationKey, ID, Name);
            return result;
        }

        [HttpPost]
        [Route("deleteProductTypeMaster")]
        public int deleteProductTypeMaster([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<int> rObj = new ResponseObject<int>();

            ID = int.Parse(new CommonMethods().getBodyParameters(json, "ID"));
            int result = _ProductTypeProvider.deleteProductTypeMaster(AuthenticationKey, ID);
            return result;
        }


    }
}
