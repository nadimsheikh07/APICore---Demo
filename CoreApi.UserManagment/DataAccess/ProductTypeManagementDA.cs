using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Npgsql;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Cryptography;
using CoreApi.Common.Models;
using CoreApi.UserManagment.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CoreApi.UserManagment.DataAccess
{
    public class ProductTypeManagementDA
    {
        string strquery = "";
        private readonly UserManagemntDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductTypeManagementDA(UserManagemntDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void getAllProductTypeObject(string AuthenticationKey, ResponseObject<ProductTypeModel> rObj)
        {
            // checkClientAuthenticated(AuthenticationKey);
            string sql = " select * from ProductType";
            DataTable dt = Helper.Helper.executeDataTableSqlFunctionMsg_SHO(sql, _configuration);
            List<ProductTypeModel> lst = new List<ProductTypeModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductTypeModel lstItem = new ProductTypeModel();
                    lstItem.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    lstItem.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    lst.Add(lstItem);
                }
            }
            rObj.data = lst;
        }
        //


        public int addNewProductTypeMaster(string AuthenticationKey, string Name)
        {
            //checkClientAuthenticated(AuthenticationKey);
            CommonFunction objCommon = new CommonFunction();
            strquery = "Insert into ProductType (Name) VALUES ( '" + Name + "')";
            int result = Helper.Helper.executeDataTableSqlFunctionMsgSHO(strquery, _configuration);
            return result;
        }


        public int UpdateProductTypeMaster(string AuthenticationKey, int ID, string Name)
        {
            //checkClientAuthenticated(AuthenticationKey);
            CommonFunction objCommon = new CommonFunction();
            strquery = "UPDATE ProductType SET Name =  '" + Name + "' WHERE ID = '" + ID + "'  ";
            int result = Helper.Helper.executeDataTableSqlFunctionMsgSHO(strquery, _configuration);
            return result;
        }

        public int DeleteProductTypeMaster(string AuthenticationKey, int ID)
        {
            //checkClientAuthenticated(AuthenticationKey);
            CommonFunction objCommon = new CommonFunction();
            strquery = "delete from ProductType where ID = '" + ID + "'";
            int result = Helper.Helper.executeDataTableSqlFunctionMsgSHO(strquery, _configuration);
            return result;
        }

      

    }


}
