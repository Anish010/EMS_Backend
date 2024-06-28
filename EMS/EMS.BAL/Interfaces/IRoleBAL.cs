using EMS.DAL.DTO;

namespace EMS.BAL.Interfaces;

public interface IRoleBAL
{
    public Task<int> AddRoleAsync(RoleDto role);
    public Task<List<RoleDto>?> GetAllAsync(RoleFilters? filters);
    public Task<List<RoleDto>> GetRolesByDeptIdAsync(RoleFilters filter);
    public Task<List<RoleDto>> GetRolesByLocIdAsync(RoleFilters filter);
    public Task<List<RoleDto>> GetRolesByRoleIdAsync(RoleFilters filter);
    public Task<List<RoleDto>?> FilterRolesAsync(RoleFilters? filters);
}