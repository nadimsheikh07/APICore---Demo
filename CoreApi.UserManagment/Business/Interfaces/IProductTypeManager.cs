using CoreApi.Common.Models;
using CoreApi.UserManagment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CoreApi.UserManagment.Business.Interfaces
{
    public interface IProductTypeManager
    {
        void getAllProductTypeObject(string AuthenticationKey, ResponseObject<ProductTypeModel> rObj);
        int addNewProductTypeMaster(string AuthenticationKey, string Name);
        int updateProductTypeMaster(string AuthenticationKey, int ID, string Name);
        int deleteProductTypeMaster(string AuthenticationKey, int ID);

    }
}
