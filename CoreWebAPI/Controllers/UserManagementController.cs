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

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        /// <summary>
        /// make constructor for using interface provide functions for User Management  
        /// </summary>
        IRoleManager _RoleProvider;
        IUserManager _UserProvider;
        private Int32 id = 0, projectId = 0, userId = 0, hierarchyid = 0, nRoleid = 0, roleId = 0;
        private string cLoginId = "", cPassword = "", cConfirmPassword = "", cServiceId = "",
            cEmployeeName = "", cMobileNumber = "", cEmailId = "", cGAA = "", cStoreId = "", userName = "", ps = "", oldps = "", newps = "", cStatus = "";
        RoleDetail roleDetails = null;
        string AuthenticationKey = string.Empty;


        public UserManagementController(IRoleManager RoleConfigurationProvider, IUserManager UserConfigurationProvider)
        {
            _RoleProvider = RoleConfigurationProvider;
            _UserProvider = UserConfigurationProvider;
        }
        /// <summary>
        /// get all data of Role. 
        /// </summary>
        /// <param name="roleDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllRoleDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<RoleDetail>))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<RoleDetail> getAllRoleDetail([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<RoleDetail> rObj = new ResponseObject<RoleDetail>();
            rObj.rId = new CommonMethods().getRequestId(Request);
            projectId = int.Parse(new CommonMethods().getBodyParameters(json, "projectId"));
            _RoleProvider.getAllRoleDetails(AuthenticationKey, projectId, rObj); //call interface function for get Gaa id list
            return rObj;
        }

        /// <summary>
        /// Echo
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/echo")]
        public string echo()
        {
            return "Success";
        }

        /// <summary>
        /// add new record of Role.
        /// </summary>
        /// <param name="roleDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addRoleDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<CUDResult>))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<CUDResult> addRoleDetail([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<CUDResult> rObj = new ResponseObject<CUDResult>();
            List<CUDResult> cudResult = new List<CUDResult>();
            CUDResult cud = new CUDResult();
            rObj.rId = new CommonMethods().getRequestId(Request);
            string strroleDetails = new CommonMethods().getBodyParameters(json, "roleDetails");
            roleDetails = JsonConvert.DeserializeObject<RoleDetail>(strroleDetails);
            cud.apiResult = _RoleProvider.addRoleDetails(AuthenticationKey, roleDetails);
            cudResult.Add(cud);
            rObj.data = cudResult;
            return rObj;
        }
        /// <summary>
        /// get list of Project Hierarchy all records.
        /// </summary>
        /// <param name="roleDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getParentRoleList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<ParentRoleList>))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<ParentRoleList> getParentRoleList([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<ParentRoleList> rObj = new ResponseObject<ParentRoleList>();
            rObj.rId = new CommonMethods().getRequestId(Request);
            projectId = int.Parse(new CommonMethods().getBodyParameters(json, "projectId"));
            _RoleProvider.getProjectHierarchyForList(AuthenticationKey, projectId, rObj); //call interface function for get list
            return rObj;
        }

        #region "User Creation"

        /// <summary>
        /// get all User Creation details.
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns>List</returns>
        [HttpPost]
        [Route("getAllUserCreation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(objectResponse))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public objectResponse getAllUserCreation([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            objectResponse rObj = new objectResponse();
            rObj.rId = new CommonMethods().getRequestId(Request);
            projectId = int.Parse(new CommonMethods().getBodyParameters(json, "projectId"));
            hierarchyid = int.Parse(new CommonMethods().getBodyParameters(json, "hierarchyid"));
            _UserProvider.getAllUserCreation(AuthenticationKey, projectId, hierarchyid, rObj);
            return rObj;
        }
        /// <summary>
        /// to Add user master.
        /// </summary>
        /// <param name="userdetail"></param>
        /// <returns>List</returns> 
        [HttpPost]
        [Route("addNewUserMaster")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<SelectListItem>))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<SelectListItem> addNewUserMaster([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<SelectListItem> rObj = new ResponseObject<SelectListItem>();
            rObj.rId = new CommonMethods().getRequestId(Request);

            projectId = int.Parse(new CommonMethods().getBodyParameters(json, "projectId"));
            userId = int.Parse(new CommonMethods().getBodyParameters(json, "userId"));
            cLoginId = new CommonMethods().getBodyParameters(json, "cLoginId");
            cPassword = new CommonMethods().getBodyParameters(json, "cPassword");
            cConfirmPassword = new CommonMethods().getBodyParameters(json, "cConfirmPassword");
            cEmployeeName = new CommonMethods().getBodyParameters(json, "cEmployeeName");
            nRoleid = int.Parse(new CommonMethods().getBodyParameters(json, "nRoleid"));
            cEmailId = new CommonMethods().getBodyParameters(json, "cEmailId");
            cMobileNumber = new CommonMethods().getBodyParameters(json, "cMobileNumber");
            cStatus = new CommonMethods().getBodyParameters(json, "cStatus");
            cGAA = new CommonMethods().getBodyParameters(json, "cGAA");
            cServiceId = new CommonMethods().getBodyParameters(json, "cServiceId");
            cStoreId = new CommonMethods().getBodyParameters(json, "cStoreId");

            //string struserdetails = new CommonMethods().getBodyParameters(json, "userdetails");
            //userdetails = JsonConvert.DeserializeObject<UserDetail>(struserdetails);
            rObj.data = _UserProvider.addNewUsersMaster(AuthenticationKey, projectId, userId, cLoginId, cPassword, cConfirmPassword, cEmployeeName, nRoleid, cEmailId, cMobileNumber, cStatus, cGAA, cServiceId, cStoreId); //call interface function for get list
            return rObj;
        }

        /// <summary>
        /// using for Update user master.
        /// </summary>
        /// <param name="userdetail"></param>
        /// <returns>List</returns> 
        [HttpPost]
        [Route("updateUserMaster")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<SelectListItem>))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<SelectListItem> updateUserMaster([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<SelectListItem> rObj = new ResponseObject<SelectListItem>();
            rObj.rId = new CommonMethods().getRequestId(Request);
            id = int.Parse(new CommonMethods().getBodyParameters(json, "id"));
            projectId = int.Parse(new CommonMethods().getBodyParameters(json, "projectId"));
            userId = int.Parse(new CommonMethods().getBodyParameters(json, "userId"));
            cLoginId = new CommonMethods().getBodyParameters(json, "cLoginId");
            cPassword = new CommonMethods().getBodyParameters(json, "cPassword");
            cConfirmPassword = new CommonMethods().getBodyParameters(json, "cConfirmPassword");
            cEmployeeName = new CommonMethods().getBodyParameters(json, "cEmployeeName");
            nRoleid = int.Parse(new CommonMethods().getBodyParameters(json, "nRoleid"));
            cEmailId = new CommonMethods().getBodyParameters(json, "cEmailId");
            cMobileNumber = new CommonMethods().getBodyParameters(json, "cMobileNumber");
            cStatus = new CommonMethods().getBodyParameters(json, "cStatus");
            cGAA = new CommonMethods().getBodyParameters(json, "cGAA");
            cServiceId = new CommonMethods().getBodyParameters(json, "cServiceId");
            cStoreId = new CommonMethods().getBodyParameters(json, "cStoreId");
            rObj.data = _UserProvider.updateUserMasters(AuthenticationKey, id, projectId, userId, cLoginId, cPassword, cConfirmPassword, cEmployeeName, nRoleid, cEmailId, cMobileNumber, cStatus, cGAA, cServiceId, cStoreId); //call interface function for get list
            return rObj;
        }
        #endregion

        #region "User Login/LogOut"
        /// <summary>
        /// get all user login detail.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllUserLoginDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<UserLogInDetail>))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<UserLogInDetail> getAllUserLoginDetail([FromBody] ExpandoObject json)
        {
            ResponseObject<UserLogInDetail> rObj = new ResponseObject<UserLogInDetail>();
            rObj.rId = new CommonMethods().getRequestId(Request);
            userName = new CommonMethods().getBodyParameters(json, "userName");
            ps = new CommonMethods().getBodyParameters(json, "ps");
            _UserProvider.getAllUserLoginDetail(userName, ps, rObj);
            return rObj;
        }
        /// <summary>
        /// get all user menus detail.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllUserMenusDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<userMenuDetail>))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<userMenuDetail> getAllUserMenusDetail([FromBody] ExpandoObject json)
        {
            ResponseObject<userMenuDetail> rObj = new ResponseObject<userMenuDetail>();
            rObj.rId = new CommonMethods().getRequestId(Request);
            roleId = int.Parse(new CommonMethods().getBodyParameters(json, "roleId"));
            _UserProvider.getAllUserMenusDetail(roleId, rObj);
            return rObj;
        }
        /// <summary>
        /// using for change password detail.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("changePsDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<CUDResult>))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<CUDResult> changePsDetail([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<CUDResult> rObj = new ResponseObject<CUDResult>();
            List<CUDResult> cudResult = new List<CUDResult>();
            CUDResult cud = new CUDResult();
            rObj.rId = new CommonMethods().getRequestId(Request);
            oldps = new CommonMethods().getBodyParameters(json, "oldps");
            newps = new CommonMethods().getBodyParameters(json, "newps");
            userId = int.Parse(new CommonMethods().getBodyParameters(json, "userId"));
            cud.apiResult = _UserProvider.changePsDetail(AuthenticationKey, oldps, newps, userId);
            cudResult.Add(cud);
            rObj.data = cudResult;
            return rObj;
        }
        /// <summary>
        /// using for logout clear session detail.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LogOutClearSessionDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<CUDResult>))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<CUDResult> LogOutClearSessionDetail([FromBody] ExpandoObject json)
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<CUDResult> rObj = new ResponseObject<CUDResult>();
            List<CUDResult> cudResult = new List<CUDResult>();
            CUDResult cud = new CUDResult();
            rObj.rId = new CommonMethods().getRequestId(Request);
            userId = int.Parse(new CommonMethods().getBodyParameters(json, "userId"));
            _UserProvider.LogOutClearSessionDetail(AuthenticationKey, userId);
            cud.apiResult = true;
            cudResult.Add(cud);
            rObj.data = cudResult;
            return rObj;
        }

        /// <summary>
        /// using for check user authenticated.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("checkUserAuthentication")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<CUDResult>))]
        [ProducesDefaultResponseType(typeof(ApiHttpException))]
        public ResponseObject<CUDResult> checkUserAuthentication()
        {
            string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
            ResponseObject<CUDResult> rObj = new ResponseObject<CUDResult>();
            List<CUDResult> cudResult = new List<CUDResult>();
            CUDResult cud = new CUDResult();
            _UserProvider.checkUserAuthentication(AuthenticationKey);
            cud.apiResult = true;
            cudResult.Add(cud);
            rObj.data = cudResult;
            return rObj;
        }


        //[HttpPost]
        //[Route("getAllRoleObject_SHO")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject<ProductTypeModel>))]
        //[ProducesDefaultResponseType(typeof(ApiHttpException))]
        //public ResponseObject<ProductTypeModel> getAllRoleObject_SHO([FromBody] ExpandoObject json)
        //{
        //    string AuthenticationKey = new CommonMethods().getAuthenticationKey(Request);
        //    ResponseObject<ProductTypeModel> rObj = new ResponseObject<ProductTypeModel>();
        //    rObj.rId = new CommonMethods().getRequestId(Request);
        //    //projectId = int.Parse(new CommonMethods().getBodyParameters(json, "projectId"));
        //    _RoleProvider.getAllRoleObject_SHO(AuthenticationKey, rObj); //call interface function for get Gaa id list
        //    return rObj;
        //}


         

        #endregion
    }
}
