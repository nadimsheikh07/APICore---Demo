using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreApi.UserManagment.DataAccess;
using CoreApi.UserManagment.Business.Interfaces;
using CoreApi.Common.Models;
using CoreApi.UserManagment.Models;

namespace CoreApi.UserManagment.Business
{
    public class UserManager : IUserManager
    {
        private readonly UserManagemntDbContext _context;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// create constructor to make a role dbcontext for role details.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        public UserManager(UserManagemntDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// get all user creation grid.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="hierarchyid"></param>
        /// <param name="rObj"></param>
        /// <returns>List</returns>
        public void getAllUserCreation(string AuthenticationKey, int projectId, int hierarchyid, objectResponse rObj)
        {
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            if (rObj != null)
            {
                userManagementDA.getAllUserCreationObject(AuthenticationKey, projectId, hierarchyid, rObj);
            }
            else
            {
                ApiHttpException ApiCustomException = new ApiHttpException();
                ApiCustomException.message = "Error Massage";
                ApiCustomException.errorId = 807;
                throw ApiCustomException;
            }
        }

        /// <summary>
        /// to add user master.
        /// </summary>
        /// <param name="userdetail"></param>
        /// <returns>List</returns>
        public List<SelectListItem> addNewUsersMaster(string AuthenticationKey, int projectId, int userId, string cLoginId, string cPassword, string cConfirmPassword, string cEmployeeName, int nRoleid, string cEmailId, string cMobileNumber, string nStatus, string cGAA, string cServiceId, string cStoreId)
        {
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            return userManagementDA.addUserObject(AuthenticationKey, projectId, userId, cLoginId, cPassword, cConfirmPassword, cEmployeeName, nRoleid, cEmailId, cMobileNumber, nStatus, cGAA, cServiceId, cStoreId); //call interface function for get list
        }
        /// <summary>
        /// to update user master.
        /// </summary>
        /// <param name="userdetail"></param>
        /// <returns>List</returns>
        public List<SelectListItem> updateUserMasters(string AuthenticationKey, int id, int projectId, int userId, string cLoginId, string cPassword, string cConfirmPassword, string cEmployeeName, int nRoleid, string cEmailId, string cMobileNumber, string nStatus, string cGAA, string cServiceId, string cStoreId)
        {
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            return userManagementDA.updateUserMastersObject(AuthenticationKey, id, projectId, userId, cLoginId, cPassword, cConfirmPassword, cEmployeeName, nRoleid, cEmailId, cMobileNumber, nStatus, cGAA, cServiceId, cStoreId);
        }
        /// <summary>
        /// get all user login detail.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="ps"></param>
        /// <param name="rObj"></param>
        public void getAllUserLoginDetail(string userName, string ps, ResponseObject<UserLogInDetail> rObj)
        {
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            userManagementDA.getAllUserLoginDetail(userName, ps, rObj);
        }
        /// <summary>
        /// get all user menus detail.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="rObj"></param>
        public void getAllUserMenusDetail(int roleId, ResponseObject<userMenuDetail> rObj)
        {
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            userManagementDA.getAllUserMenusDetail(roleId, rObj);
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
            bool status = false;
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            return status = userManagementDA.changePsDetail(AuthenticationKey, oldps, newps, userId);
        }
        /// <summary>
        /// using for logout clear session detail.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public void LogOutClearSessionDetail(string AuthenticationKey, long userId)
        {
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            userManagementDA.LogOutClearSessionDetail(AuthenticationKey, userId);
        }
        /// <summary>
        /// using for check client authenticated.
        /// </summary>
        /// <param name="AuthenticationKey"></param>
        public void checkUserAuthentication(string AuthenticationKey)
        {
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            userManagementDA.checkClientAuthenticated(AuthenticationKey);
        }


 

    }
}
