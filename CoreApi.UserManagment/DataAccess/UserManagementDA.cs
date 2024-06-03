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

namespace CoreApi.UserManagment.DataAccess
{
    public class UserManagementDA
    {
        string strquery = "";
        private readonly UserManagemntDbContext _context;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// using for check client authenticated.
        /// </summary>
        /// <param name="AuthenticationKey"></param>
        public void checkClientAuthenticated(string AuthenticationKey)
        {
            DataTable dtSession = clientAuthenticated(AuthenticationKey);
            if (Convert.ToInt32(dtSession.Rows[0][0]) == 0)
            {
                ApiHttpException ApiCustomException = new ApiHttpException();
                ApiCustomException.message = Constants.unAuthorizeuser;
                ApiCustomException.errorId = (int)ErrorDetails.unAuthorizeuser; //802
                throw ApiCustomException;
            }
        }
        /// <summary>
        /// using for check authenticated detail in db.
        /// </summary>
        /// <param name="AuthenticationKey"></param>
        /// <returns></returns>
        public DataTable clientAuthenticated(string AuthenticationKey)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("Select count(*) From clientsessiondetail Where AuthenticationKey = @authenticationKey ", conn);

                cmd.Parameters.AddWithValue("@authenticationKey", AuthenticationKey ?? "''");
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.Dispose();
                adapter.Dispose();
                conn.Close();
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// create constructor to make a global dbcontext for user.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        public UserManagementDA(UserManagemntDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        /// <summary>
        ///  get function of list of role all records.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="rObj"></param>
        public void getAllRoleObject(string AuthenticationKey, int projectId, ResponseObject<RoleDetail> rObj)
        {
            checkClientAuthenticated(AuthenticationKey);
            string sql = " select * from grid_rolemaster(" + projectId + " ,'aasaancur'); fetch all in \"aasaancur\";";
            DataTable dt = Helper.Helper.executeDataTableSqlFunction(sql, _configuration);
            List<RoleDetail> lst = new List<RoleDetail>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RoleDetail lstItem = new RoleDetail();
                    lstItem.roleId = CommonFunction.ConvertInt32(dt.Rows[i]["0nRoleid"]);
                    lstItem.parentRoleId = CommonFunction.ConvertInt32(dt.Rows[i]["0Parent role"]);
                    lstItem.projectHierarchyId = CommonFunction.ConvertInt32(dt.Rows[i]["0Project hierarchy"]);
                    lstItem.projectHierarchy = Convert.ToString(dt.Rows[i]["Project hierarchy"]);
                    lstItem.roleName = Convert.ToString(dt.Rows[i]["0cRolename"]);
                    lst.Add(lstItem);
                }
            }
            rObj.data = lst;
        }        
        /// <summary>
        /// add new role details.
        /// </summary>
        /// <param name="roleDetail"></param>
        /// <returns></returns>
        public bool addRoleDetails(string AuthenticationKey, RoleDetail roleDetail)
        {
            checkClientAuthenticated(AuthenticationKey);
            bool response = false;
            var Rolejson = "";
            var RoleData = new
            {
                Permission = roleDetail.perm,
                Globalid1 = roleDetail.roleId,
                Rolename = roleDetail.roleName,
                Projecthierarchy = roleDetail.projectHierarchyId,
                sesProjectid = roleDetail.projectId,
                sesUserid = roleDetail.userId
            };
            Rolejson = System.Text.Json.JsonSerializer.Serialize(RoleData);
            string strquery = " select * From insertupdaterolemaster('[" + Rolejson + "]') ";
            DataTable dt = Helper.Helper.executeDataTableSqlQuery(strquery, _configuration);
            if (CommonFunction.ConvertInt32(dt.Rows[0][0]) == -1)
            {
                ApiHttpException ApiCustomException = new ApiHttpException();
                ApiCustomException.message = dt.Rows[0][1].ToString();
                ApiCustomException.errorId = (int)ErrorDetails.InsertUpdateRoleMasterAdd;
                throw ApiCustomException;
            }
            if (dt.Rows.Count > 0)
            {
                response = true;
            }
            return response;
        }       
        /// <summary>
        /// get list of parent role.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="rObj"></param>
        public void getParentRoleObjectForList(string AuthenticationKey, int projectId, ResponseObject<ParentRoleList> rObj)
        {
            checkClientAuthenticated(AuthenticationKey);
            strquery = " select * from combo_projecthierarchymaster(" + projectId + ", 'aasaancur'); fetch all in \"aasaancur\"; ";
            DataTable dt = Helper.Helper.executeDataTableSqlFunction(strquery, _configuration);
            var reult = (from rw in dt.AsEnumerable()
                         select new
                         {
                             Text = Convert.ToString(rw["cHierarchyname"]),
                             Value = CommonFunction.ConvertInt32(rw["nHierarchyid"])
                         }).ToList();
            //var ddProjectHierarchy = reult.ConvertAll<object>(o => (object)o);
            var lstParentRoleList = new List<ParentRoleList>();
            lstParentRoleList = reult.Select(p => new ParentRoleList
            {
                pRoleId = p.Value,
                pRoleName = p.Text
            }).ToList();
            rObj.data = lstParentRoleList;
        }
        /// <summary>
        /// get list of fuser by session gaa.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="rObj"></param>
        
        #region "Private Functions"
        /// <summary>
        /// get columns of user creation.
        /// </summary>
        /// <param name="dtColumn"></param>
        /// <returns></returns>
        private string[] GetColumnData_UserCreation(DataTable dtColumn)
        {
            //string[] columnNames = dtColumn.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

            string[] arr1 = new string[dtColumn.Columns.Count];
            CommonFunction objCommon = new CommonFunction();

            string strWidth = string.Empty;

            for (int i = 0; i < dtColumn.Columns.Count; i++)
            {
                //arr2[i] += "$";
                arr1[i] += "{";
                arr1[i] += "'text':'" + dtColumn.Columns[i] + "',";
                arr1[i] += "'datafield':'" + dtColumn.Columns[i] + "',";
                strWidth = objCommon.GetColumnWidth(dtColumn.Columns[i].DataType.ToString());

                if (Convert.ToString(dtColumn.Columns[i]) == "0nRoleid" || Convert.ToString(dtColumn.Columns[i]) == "?column?" || Convert.ToString(dtColumn.Columns[i]) == "0nParentuserid" || Convert.ToString(dtColumn.Columns[i]) == "0Serviceid" || Convert.ToString(dtColumn.Columns[i]) == "0Storeid" || Convert.ToString(dtColumn.Columns[i]) == "0Manager" || Convert.ToString(dtColumn.Columns[i]) == "0treeval" || Convert.ToString(dtColumn.Columns[i]) == "0cGAAdetailid" || Convert.ToString(dtColumn.Columns[i]) == "0cStatus" || Convert.ToString(dtColumn.Columns[i]) == "0Password" || Convert.ToString(dtColumn.Columns[i]) == "0nParentuserid")
                {
                    //arr1[i] += "'width':'0',";
                    arr1[i] += "'hidden' : 'true'";
                }
                else
                {
                    arr1[i] += "'width':'" + strWidth + "'";
                }

                arr1[i] += "}";
            }

            return arr1;
        }
        #endregion
        #region UserCreation
        /// <summary>
        /// get all list of user details.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="hierarchyid"></param>
        /// <param name="rObj"></param>
        /// <returns></returns>
        public void getAllUserCreationObject(string AuthenticationKey, int projectId, int hierarchyid, objectResponse rObj)
        {
            checkClientAuthenticated(AuthenticationKey);
            CommonFunction objCommon = new CommonFunction();
            strquery = "Select * From grid_userdetail(" + hierarchyid + "," + projectId + ",'aasaancur'); FETCH ALL IN \"aasaancur\";";
            DataTable dt = Helper.Helper.executeDataTableSqlFunction(strquery, _configuration);
            string strHideColumns = "0";
            DataTable dtGridDynamic = objCommon.GetDynamicDT(dt, strHideColumns);
            dtGridDynamic.Columns.Add(new DataColumn("Edit"));
            string JSONStr = string.Empty;
            if (dtGridDynamic.Rows.Count > 0)
            {

                for (int i = 0; i < dtGridDynamic.Columns.Count; i++)
                {
                    if (dtGridDynamic.Columns[i].ToString() == "GAA")
                    {
                        for (int j = 0; j < dtGridDynamic.Rows.Count; j++)
                        {
                            var chkgaa = dtGridDynamic.Rows[j]["0cGAAdetailid"].ToString();
                            dtGridDynamic.Rows[j][i] = "<a  href=# onclick=GetGAAData(this) gaaIds=" + chkgaa + " >View GAA</a>";
                        }
                    }
                    if (dtGridDynamic.Columns[i].ToString() == "Service")
                    {
                        for (int j = 0; j < dtGridDynamic.Rows.Count; j++)
                        {
                            var chkservice = dtGridDynamic.Rows[j]["0Serviceid"].ToString();
                            dtGridDynamic.Rows[j][i] = "<a  href=# onclick=GetServiceData(this) serviceIds=" + chkservice + ">View Service</a>";
                        }
                    }
                    if (dtGridDynamic.Columns[i].ToString() == "Store")
                    {
                        for (int j = 0; j < dtGridDynamic.Rows.Count; j++)
                        {
                            var chkstore = dtGridDynamic.Rows[j]["0Storeid"].ToString();
                            dtGridDynamic.Rows[j][i] = "<a  href=# onclick=GetStoreData(this)  storeIds=" + chkstore + ">View Store</a>";
                        }
                    }
                    if (dtGridDynamic.Columns[i].ToString() == "Edit")
                    {
                        for (int j = 0; j < dtGridDynamic.Rows.Count; j++)
                        {
                            var value = dtGridDynamic.Rows[j]["0nUserid"].ToString();
                            dtGridDynamic.Rows[j][i] = "<a href=# onclick=getUpdateData(" + value + ")> Edit </ a > ";
                        }
                    }

                }
                //objectResponse obj = new objectResponse();

                string[] strColumnData = GetColumnData_UserCreation(dtGridDynamic);
                string strRows = objCommon.GetJsonFromDT(dtGridDynamic);
                string strColumnType = objCommon.GetJobColumnDataType(dtGridDynamic);
                string str = JsonConvert.SerializeObject(strColumnData) + JsonConvert.SerializeObject(strRows) + JsonConvert.SerializeObject(strColumnType);
                JSONStr = str;
            }
            //return dtGridDynamic;

            //List<objectResponse> objectResponsesList = new List<objectResponse>();

            rObj.data = JSONStr;
        }
        /// <summary>
        /// to add manage field user.
        /// </summary>
        /// <param name="userdetail"></param>
        /// <returns>List</returns>
        public List<SelectListItem> addUserObject(string AuthenticationKey, int projectId, int userId, string cLoginId, string cPassword, string cConfirmPassword, string cEmployeeName, int nRoleid, string cEmailId, string cMobileNumber, string nStatus, string cGAA, string cServiceId, string cStoreId)
        {
            checkClientAuthenticated(AuthenticationKey);
            DataTable strEncrypt = new DataTable();
            CommonFunction objCommon = new CommonFunction();
            string original = cPassword;
            string? enckeydb = getEncryptionKey("EncryptionKey");
            strEncrypt = objCommon.Encrypt(original, enckeydb ?? "");
            //string strDecrypt = objCommon.Decrypt(strEncrypt, enckeydb ?? "");
            strquery = "select * From insertupdateusercreation('[{\"Permission\":\"N\",\"Globalid1\":\"0\",\"Globalid2\":0,\"Loginid\":\"" + cLoginId + "\",\"Password\":\"" + Convert.ToString(strEncrypt.Columns[0]) + "\",\"Confirmpassword\":\"" + Convert.ToString(strEncrypt.Columns[0]) + "\",\"Employeename\":\"" + cEmployeeName + "\",\"Roleid\":\"" + nRoleid + "\",\"Email\":\"" + cEmailId + "\",\"Mobile\":\"" + cMobileNumber + "\",\"GAAdetailid\":\"" + cGAA + "\",\"Status\":\"" + nStatus + "\",\"Serviceid\":\"" + cServiceId + "\",\"nStoreid\":\"" + cStoreId + "\",\"sesUserid\":\"" + userId + "\",\"sesProjectid\":\"" + projectId + "\"}]', '" + Convert.ToString(strEncrypt.Columns[1]) + "')";
            DataTable dt = Helper.Helper.executeDataTableSqlFunctionMsg(strquery, _configuration);
            List<SelectListItem> ddUser = new List<SelectListItem>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SelectListItem lstItem = new SelectListItem();
                    lstItem.Text = Convert.ToString(dt.Rows[i]["value1"]);
                    lstItem.Value = Convert.ToString(dt.Rows[i]["msg"]);

                    ddUser.Add(lstItem);
                }

            }
            return ddUser;
        }
        /// <summary>
        /// to update manage field user.
        /// </summary>
        /// <param name="userdetail"></param>
        /// <returns>List</returns>
        public List<SelectListItem> updateUserMastersObject(string AuthenticationKey, int id, int projectId, int userId, string cLoginId, string cPassword, string cConfirmPassword, string cEmployeeName, int nRoleid, string cEmailId, string cMobileNumber, string nStatus, string cGAA, string cServiceId, string cStoreId)
        {
            checkClientAuthenticated(AuthenticationKey);
            DataTable strEncrypt = new DataTable();
            CommonFunction objCommon = new CommonFunction();
            string original = cPassword;
            string? enckeydb = getEncryptionKey("EncryptionKey");
            strEncrypt = objCommon.Encrypt(original, enckeydb ?? "");
            //string strDecrypt = objCommon.Decrypt(strEncrypt, enckeydb ?? "");
            strquery = "select * From insertupdateusercreation('[{\"Permission\":\"E\",\"Globalid1\":\"" + id + "\",\"Globalid2\":" + id + ",\"Loginid\":\"" + cLoginId + "\",\"Password\":\"" + Convert.ToString(strEncrypt.Columns[0]) + "\",\"Confirmpassword\":\"" + Convert.ToString(strEncrypt.Columns[0]) + "\",\"Employeename\":\"" + cEmployeeName + "\",\"Roleid\":\"" + nRoleid + "\",\"Email\":\"" + cEmailId + "\",\"Mobile\":\"" + cMobileNumber + "\",\"GAAdetailid\":\"" + cGAA + "\",\"Status\":\"" + nStatus + "\",\"Serviceid\":\"" + cServiceId + "\",\"nStoreid\":\"" + cStoreId + "\",\"sesUserid\":\"" + userId + "\",\"sesProjectid\":\"" + projectId + "\"}]', '" + Convert.ToString(strEncrypt.Columns[1]) + "')";
            DataTable dt = Helper.Helper.executeDataTableSqlFunctionMsg(strquery, _configuration);
            List<SelectListItem> ddUser = new List<SelectListItem>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SelectListItem lstItem = new SelectListItem();
                    lstItem.Text = Convert.ToString(dt.Rows[i]["value1"]);
                    lstItem.Value = Convert.ToString(dt.Rows[i]["msg"]);

                    ddUser.Add(lstItem);
                }

            }
            return ddUser;
        }
        #endregion
    
        /// <summary>
        /// get encryption key by db.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string? getEncryptionKey(string key)
        {
            string? encKey = "";
            strquery = "select value from tb_encryption_detail where \"key\" = '" + key + "'";
            DataTable dt = Helper.Helper.executeDataTableSqlFunctionMsg(strquery, _configuration);
            encKey = Convert.ToString(dt.Rows[0]["value"]);
            return encKey;
        }

        
        public void getAllRoleObject_SHO(string AuthenticationKey, ResponseObject<ProductTypeModel> rObj)
        {
           // checkClientAuthenticated(AuthenticationKey);
            string sql = " select * from city";
            DataTable dt = Helper.Helper.executeDataTableSqlFunctionMsg_SHO(sql, _configuration);
            List<ProductTypeModel> lst = new List<ProductTypeModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductTypeModel lstItem = new ProductTypeModel();
                     lstItem.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    lst.Add(lstItem);
                }
            }
            rObj.data = lst;
        }




        #region "User Login/LogOut"
        /// <summary>
        /// get all user login detail.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="ps"></param>
        /// <param name="rObj"></param>
        public void getAllUserLoginDetail(string userName, string ps, ResponseObject<UserLogInDetail> rObj)
        {
            string sql = "";
            string strEncrypt = "";
            CommonFunction objCommon = new CommonFunction();
            string guid = "";
            string original = ps;
            string? enckeydb = getEncryptionKey("EncryptionKey");

            sql = " Select \"salttext\" from \"Usermaster\" where \"cLoginid\" = '" + userName + "' ; ";
            DataTable dtsalt = Helper.Helper.executeDataTableSqlQuery(sql, _configuration);

            if (dtsalt.Rows.Count > 0)
            {
                strEncrypt = objCommon.validateEncryption(original, enckeydb ?? "", Convert.ToString(dtsalt.Rows[0]["salttext"]) ?? "");
            }

            //strEncrypt = objCommon.Encrypt(original, enckeydb ?? "");

            sql = " Select * from userlogin('" + userName + "','" + strEncrypt + "');";
            DataTable dt = Helper.Helper.executeDataTableSqlFunctionMsg(sql, _configuration);
            List<UserLogInDetail> lst = new List<UserLogInDetail>();
            if (dt.Rows.Count > 0)
            {
                if(CommonFunction.ConvertInt32(dt.Rows[0]["msg"]) == 1)
                {
                    guid = System.Guid.NewGuid().ToString();
                    if (Convert.ToInt64(dt.Rows[0]["nuserid"]) > 0)
                    {
                        insertClientSessionDetail(Convert.ToInt64(dt.Rows[0]["nuserid"]), guid);
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserLogInDetail lstItem = new UserLogInDetail();
                    lstItem.msg = CommonFunction.ConvertInt32(dt.Rows[i]["msg"]);
                    lstItem.value1 = Convert.ToString(dt.Rows[i]["value1"]);
                    lstItem.cuserName = Convert.ToString(dt.Rows[i]["cusername"]);
                    lstItem.ps = Convert.ToString(dt.Rows[i]["cpassword"]);
                    lstItem.nRoleId = CommonFunction.ConvertInt32(dt.Rows[i]["nroleid"]);
                    lstItem.nuserId = CommonFunction.ConvertInt32(dt.Rows[i]["nuserid"]);
                    lstItem.nprojectId = CommonFunction.ConvertInt32(dt.Rows[i]["nprojectid"]);
                    lstItem.nhierarchyId = CommonFunction.ConvertInt32(dt.Rows[i]["nhierarchyid"]);
                    lstItem.roleName = Convert.ToString(dt.Rows[i]["rolename"]);
                    lstItem.projectName = Convert.ToString(dt.Rows[i]["projectname"]);
                    if (Convert.ToString(dt.Rows[i]["nfueltype"]) == "")
                    {
                        lstItem.nfuelType = 0;
                    }
                    else
                    {
                        lstItem.nfuelType = CommonFunction.ConvertInt32(dt.Rows[i]["nfueltype"]);
                    }
                    lstItem.authenticationKey = guid;
                    lst.Add(lstItem);
                }
            }
            rObj.data = lst;
        }
        /// <summary>
        /// insert client session detail.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="authenticationKey"></param>
        /// <returns></returns>
        public int insertClientSessionDetail(long userId, string authenticationKey)
        {
            int i = 0;
            using (NpgsqlConnection conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    conn.Open();
                    //Delete all login entry of userid
                    string taskQuery = "Insert into clientsessionhistory(userid, authenticationkey) "
                            + " select userid, authenticationkey from clientsessiondetail where UserId = @userId";
                    NpgsqlCommand cmd = new NpgsqlCommand(taskQuery, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    NpgsqlCommand cmd1 = new NpgsqlCommand("Delete FROM clientsessiondetail Where UserId = @userId", conn);
                    cmd1.Parameters.AddWithValue("@userId", userId);
                    cmd1.ExecuteNonQuery();
                    cmd1.Dispose();

                    NpgsqlCommand cmd2 = new NpgsqlCommand("Insert into clientsessiondetail(UserId, AuthenticationKey) values (@userId, @authenticationKey)", conn);
                    cmd2.Parameters.AddWithValue("@userId", userId);
                    cmd2.Parameters.AddWithValue("@authenticationKey", authenticationKey);
                    i = cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
                    conn.Close();
                    return i;
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    throw new Exception(ex.Message);
                }

            }
        }

        /// <summary>
        /// get all user menus detail.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="rObj"></param>
        public void getAllUserMenusDetail(int roleId, ResponseObject<userMenuDetail> rObj)
        {
            string sql = " SELECT * from getusermenu('" + roleId + "');";
            DataTable dt = Helper.Helper.executeDataTableSqlFunctionMsg(sql, _configuration);
            List<userMenuDetail> lst = new List<userMenuDetail>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    userMenuDetail lstItem = new userMenuDetail();
                    lstItem.nid = CommonFunction.ConvertInt32(dt.Rows[i]["nid"]);
                    lstItem.nindex = CommonFunction.ConvertInt32(dt.Rows[i]["nindex"]);
                    lstItem.cparentnode = Convert.ToString(dt.Rows[i]["cparentnode"]);
                    lstItem.cpagetitle = Convert.ToString(dt.Rows[i]["cpagetitle"]);
                    lstItem.cpagename = Convert.ToString(dt.Rows[i]["cpagename"]);
                    lstItem.cpermission = Convert.ToString(dt.Rows[i]["cpermission"]);
                    lstItem.pgname = Convert.ToString(dt.Rows[i]["pgname"]);
                    lstItem.childindex = CommonFunction.ConvertInt32(dt.Rows[i]["childindex"]);
                    lstItem.menuURL = Convert.ToString(dt.Rows[i]["cMenuURL"]);
                    lst.Add(lstItem);
                }
            }
            rObj.data = lst;
        }
        /// <summary>
        /// using for change password detail.
        /// </summary>
        /// <param name="oldps"></param>
        /// <param name="newps"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool changePsDetail(string AuthenticationKey, string oldps, string newps, int userId)
        {
            checkClientAuthenticated(AuthenticationKey);
            bool response = false;
            DataTable strEncryptOldps = new DataTable();
            DataTable strEncryptNewps = new DataTable();
            CommonFunction objCommon = new CommonFunction();
            string originalOldps = oldps;
            string originalNewps = newps;
            string? enckeydb = getEncryptionKey("EncryptionKey");
            strEncryptOldps = objCommon.Encrypt(originalOldps, enckeydb ?? "");
            strEncryptNewps = objCommon.Encrypt(originalNewps, enckeydb ?? "");
            string sql = " Select * from changepassword('" + Convert.ToString(strEncryptOldps.Rows[0][0]) + "','" + Convert.ToString(strEncryptNewps.Rows[0][0]) + "'," + userId + ");";
            DataTable dt = Helper.Helper.executeDataTableSqlFunctionMsg(sql, _configuration);
            if (CommonFunction.ConvertInt32(dt.Rows[0][0]) == -1)
            {
                ApiHttpException ApiCustomException = new ApiHttpException();
                ApiCustomException.message = dt.Rows[0][1].ToString();
                ApiCustomException.errorId = (int)ErrorDetails.ChangePassword;
                throw ApiCustomException;
            }
            if (dt.Rows.Count > 0)
            {
                response = true;
            }
            return response;
        }
        /// <summary>
        /// using for logout clear session detail.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public void LogOutClearSessionDetail(string AuthenticationKey, long userId)
        {
            checkClientAuthenticated(AuthenticationKey);
            using (NpgsqlConnection conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    conn.Open();
                    //Delete all login entry of userid
                    string taskQuery = "Insert into clientsessionhistory(userid, authenticationkey) "
                            + " select userid, authenticationkey from clientsessiondetail where UserId = @userId";
                    NpgsqlCommand cmd = new NpgsqlCommand(taskQuery, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    NpgsqlCommand cmd1 = new NpgsqlCommand("Delete FROM clientsessiondetail Where UserId = @userId", conn);
                    cmd1.Parameters.AddWithValue("@userId", userId);
                    cmd1.ExecuteNonQuery();
                    cmd1.Dispose();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    throw new Exception(ex.Message);
                }
            }
        }






       
        #endregion
    }

 
}
