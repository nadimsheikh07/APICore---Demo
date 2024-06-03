using CoreApi.Common.Models;
using CoreApi.UserManagment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CoreApi.UserManagment.Business.Interfaces
{
    /// <summary>
    /// create interface for all the data access / entity class database functions define.
    /// </summary>    
    public interface IUserManager
    {
        void getAllUserCreation(string AuthenticationKey, int projectId, int hierarchyid, objectResponse rObj);
        List<SelectListItem> addNewUsersMaster(string AuthenticationKey, int projectId, int userId, string cLoginId, string cPassword, string cConfirmPassword, string cEmployeeName, int nRoleid, string cEmailId, string cMobileNumber, string nStatus, string cGAA, string cServiceId, string cStoreId);

        List<SelectListItem> updateUserMasters(string AuthenticationKey, int id, int projectId, int userId, string cLoginId, string cPassword, string cConfirmPassword, string cEmployeeName, int nRoleid, string cEmailId, string cMobileNumber, string nStatus, string cGAA, string cServiceId, string cStoreId);
        
        void getAllUserLoginDetail(string userName, string ps, ResponseObject<UserLogInDetail> rObj);
        void getAllUserMenusDetail(int roleId, ResponseObject<userMenuDetail> rObj);
        bool changePsDetail(string AuthenticationKey, string oldps, string newps, int userId);
        void LogOutClearSessionDetail(string AuthenticationKey, long userId);
        void checkUserAuthentication(string AuthenticationKey);

 
    }
}
