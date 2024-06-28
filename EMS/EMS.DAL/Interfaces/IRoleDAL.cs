using EMS.DAL.DTO;

namespace EMS.DAL.Interfaces;

public interface IRoleDAL
{
    public Task<int> InsertAsync(RoleDto role);
    public Task<List<RoleDto>> RetrieveAllAsync(RoleFilters filters);
    public Task<List<RoleDto>> RetrieveByDeptIdAsync(RoleFilters filter);
    public Task<List<RoleDto>> RetrieveByLocIdAsync(RoleFilters filters);
    public Task<List<RoleDto>> RetrieveByRoleIdAsync(RoleFilters filter);
    public Task<List<RoleDto>?> FilterAsync(RoleFilters? filters);
    
}