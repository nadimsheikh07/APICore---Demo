using CoreApi.Common.Models;
using CoreApi.UserManagment.Models;

namespace CoreApi.UserManagment.Business.Interfaces
{
    /// <summary>
    /// create interface for all the data access / entity class database functions define.
    /// </summary>
    public interface IRoleManager
    {
        void getAllRoleDetails(string AuthenticationKey, int projectId, ResponseObject<RoleDetail> rObj);
        bool addRoleDetails(string AuthenticationKey, RoleDetail roleDetail);
        void getProjectHierarchyForList(string AuthenticationKey, int projectId, ResponseObject<ParentRoleList> rObj);

        void getAllRoleObject_SHO(string AuthenticationKey, ResponseObject<ProductTypeModel> rObj);
    }
}
