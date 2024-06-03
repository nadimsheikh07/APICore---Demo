using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreApi.UserManagment.DataAccess;
using CoreApi.UserManagment.Business.Interfaces;
using CoreApi.Common.Models;
using CoreApi.UserManagment.Models;

namespace CoreApi.UserManagment.Business
{
    public class ProductTypeManager : IProductTypeManager
    {
        private readonly UserManagemntDbContext _context;
        private readonly IConfiguration _configuration;
        
        public ProductTypeManager(UserManagemntDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void getAllProductTypeObject(string AuthenticationKey, ResponseObject<ProductTypeModel> rObj)
        {
            ProductTypeManagementDA producttypeManagementDA = new ProductTypeManagementDA(_context, _configuration);
            producttypeManagementDA.getAllProductTypeObject(AuthenticationKey, rObj);
        }

        public int addNewProductTypeMaster(string AuthenticationKey, string Name)
        {
            ProductTypeManagementDA producttypeManagementDA = new ProductTypeManagementDA(_context, _configuration);
            return producttypeManagementDA.addNewProductTypeMaster(AuthenticationKey, Name);
        }


        public int updateProductTypeMaster(string AuthenticationKey, int ID, string Name)
        {
            ProductTypeManagementDA producttypeManagementDA = new ProductTypeManagementDA(_context, _configuration);
            return producttypeManagementDA.UpdateProductTypeMaster(AuthenticationKey, ID, Name);
        }

        public int deleteProductTypeMaster(string AuthenticationKey, int ID)
        {
            ProductTypeManagementDA producttypeManagementDA = new ProductTypeManagementDA(_context, _configuration);
            return producttypeManagementDA.DeleteProductTypeMaster(AuthenticationKey, ID);
        }

    }
}
