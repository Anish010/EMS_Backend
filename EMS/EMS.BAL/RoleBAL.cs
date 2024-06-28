using EMS.BAL.Interfaces;
using EMS.DAL.Interfaces;
using Serilog;
using EMS.DAL.DTO;

namespace EMS.BAL;

public class RoleBAL : IRoleBAL
{
    private readonly IRoleDAL _roleDal;
    private readonly ILogger _logger;

    public RoleBAL(IRoleDAL roleDal, ILogger logger)
    {
        _roleDal = roleDal;
        _logger = logger;
    }

    public async Task<int> AddRoleAsync(RoleDto role)
    {
        try
        {
            return await _roleDal.InsertAsync(role);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while adding new role: {ex.Message}");
            throw;
        }

    }

    public async Task<List<RoleDto>?> GetAllAsync(RoleFilters? filters)
    {
        try
        {
            var roles = await _roleDal.RetrieveAllAsync(filters!) ?? [];
            return roles;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while retrieving all roles: {ex.Message}");
            throw;
        }
    }

    public async Task<List<RoleDto>> GetRolesByDeptIdAsync(RoleFilters filter)
    {
        try
        {
            List<RoleDto> roles;
            roles = await _roleDal.RetrieveByDeptIdAsync(filter) ?? [];
            return roles;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while retrieving roles by department id: {ex.Message}");
            throw;
        }
    }

    public async Task<List<RoleDto>> GetRolesByLocIdAsync(RoleFilters filter)
    {
        try
        {
            List<RoleDto> roles;
            roles = await _roleDal.RetrieveByLocIdAsync(filter) ?? [];
            return roles;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while retrieving roles by location id: {ex.Message}");
            throw;
        }
    }

    public async Task<List<RoleDto>> GetRolesByRoleIdAsync(RoleFilters filter)
    {
        try
        {
            List<RoleDto> roles;
            roles = await _roleDal.RetrieveByRoleIdAsync(filter) ?? [];
            return roles;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while retrieving roles by role id: {ex.Message}");
            throw;
        }
    }

    public async Task<List<RoleDto>?> FilterRolesAsync(RoleFilters? filters)
    {
        try
        {
            List<RoleDto> filteredRoles;
            filteredRoles = await _roleDal.FilterAsync(filters) ?? [];
            if (filteredRoles != null && filteredRoles.Count > 0)
            {
                return filteredRoles;
            }
            return [];
        }
        catch (Exception ex)
        {
            _logger.Error($"Error Filtering: {ex.Message}");
            throw;
        }
    }
}