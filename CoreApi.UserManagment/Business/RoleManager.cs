using Microsoft.Extensions.Configuration;
using CoreApi.UserManagment.DataAccess;
using CoreApi.Common.Models;
using CoreApi.UserManagment.Models;
using CoreApi.UserManagment.Business.Interfaces;

namespace CoreApi.UserManagment.Business
{
    public class RoleManager : IRoleManager
    {
        private readonly UserManagemntDbContext _context;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// create constructor to make a role dbcontext for role details.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        public RoleManager(UserManagemntDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        /// <summary>
        /// get function of list of role all records.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="rObj"></param>
        public void getAllRoleDetails(string AuthenticationKey, int projectId, ResponseObject<RoleDetail> rObj)
        {
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            userManagementDA.getAllRoleObject(AuthenticationKey, projectId, rObj);
        }
        /// <summary>
        /// create new role.
        /// </summary>
        /// <param name="roleDetail"></param>
        /// <returns></returns>
        public bool addRoleDetails(string AuthenticationKey, RoleDetail roleDetail)
        {
            bool status = false;
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            if (roleDetail != null)
            {
                if (roleDetail.projectHierarchyId == 0)
                {
                    ApiHttpException ApiCustomException = new ApiHttpException();
                    ApiCustomException.message = Constants.BLANK_CONFIGURATION_TYPE;
                    ApiCustomException.errorId = (int)ErrorDetails.ProjectHierarchyRole;
                    throw ApiCustomException;
                }
                else if (roleDetail.roleName == "")
                {
                    ApiHttpException ApiCustomException = new ApiHttpException();
                    ApiCustomException.message = Constants.BLANK_CONFIGURATION_TYPE;
                    ApiCustomException.errorId = (int)ErrorDetails.RoleName;
                    throw ApiCustomException;
                }
                else
                {
                    status = userManagementDA.addRoleDetails(AuthenticationKey, roleDetail);
                }
            }
            return status;
        }
        /// <summary>
        /// get list of project hierarchy.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="rObj"></param>
        public void getProjectHierarchyForList(string AuthenticationKey, int projectId, ResponseObject<ParentRoleList> rObj)
        {
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            userManagementDA.getParentRoleObjectForList(AuthenticationKey, projectId, rObj);
        }

       public void getAllRoleObject_SHO(string AuthenticationKey, ResponseObject<ProductTypeModel> rObj)
        {
            UserManagementDA userManagementDA = new UserManagementDA(_context, _configuration);
            userManagementDA.getAllRoleObject_SHO(AuthenticationKey, rObj);
        }

    }
}
