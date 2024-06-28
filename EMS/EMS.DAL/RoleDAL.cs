using EMS.DAL.DTO;
using EMS.DAL.Interfaces;
using EMS.DAL.Mapper;
using EMS.DB.Context;
using EMS.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.DAL;

public class RoleDAL : IRoleDAL
{
    private readonly EMSContext _context;
    private readonly IRoleMapper _roleMapper;
    private readonly IEmployeeMapper _employeeMapper;

    public RoleDAL(EMSContext context, IRoleMapper roleMapper, IEmployeeMapper employeeMapper)
    {
        _context = context;
        _roleMapper = roleMapper;
        _employeeMapper = employeeMapper;
    }

    public async Task<int> InsertAsync(RoleDto roleDto)
    {
        Role role = _roleMapper.ToRoleModel(roleDto);
        _context.Role.Add(role);
        await _context.SaveChangesAsync();

        return role.Id;
    }

    public async Task<List<RoleDto>> RetrieveAllAsync(RoleFilters? filters)
    {
        var roles = await _context.Role
                .Skip((filters!.PageNumber - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .Include(r => r.Department)
                .Include(r => r.Location)
                .Include(r => r.Employee)
                .ToListAsync();


        return _roleMapper.ToRoleDto(roles);
    }

    public async Task<List<RoleDto>> RetrieveByDeptIdAsync(RoleFilters filters)
    {
        var roles = await _context.Role
                .Include(r => r.Department)
                .Include(r => r.Location)
                .Include(r => r.Employee)
                .Where(r => r.DepartmentId == filters.DepartmentId)
                .Skip((filters.PageNumber - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .ToListAsync();

        return _roleMapper.ToRoleDto(roles);
    }

    public async Task<List<RoleDto>> RetrieveByLocIdAsync(RoleFilters filters)
    {
        var roles = await _context.Role
                .Include(r => r.Department)
                .Include(r => r.Location)
                .Include(r => r.Employee)
                .Where(r => r.LocationId == filters.LocationId)
                .Skip((filters.PageNumber - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .ToListAsync();

        return _roleMapper.ToRoleDto(roles);
    }

    public async Task<List<RoleDto>> RetrieveByRoleIdAsync(RoleFilters filters)
    {
        var roles = await _context.Role
            .Include(r => r.Department)
            .Include(r => r.Location)
            .Include(r => r.Employee)
            .Where(r => r.Id == filters.RoleId).ToListAsync();
        return _roleMapper.ToRoleDto(roles);
    }

    public async Task<List<RoleDto>?> FilterAsync(RoleFilters? filters)
    {
        if (filters == null)
        {
            return null;
        }

        var roles = _context.Role.Include(r => r.Department)
            .Include(r => r.Location)
            .Include(r => r.Location)
            .Include(r => r.Employee)
            .AsQueryable();

        if (filters.Departments != null && filters.Departments.Count != 0)
        {
            roles = roles.Where(r => r.DepartmentId.HasValue && filters.Departments.Contains(r.DepartmentId.Value));
        }

        if (filters.Locations != null && filters.Locations.Count != 0)
        {
            roles = roles.Where(r => r.LocationId.HasValue && filters.Locations.Contains(r.LocationId.Value));
        }

        if (!string.IsNullOrWhiteSpace(filters.Search))
        {
            roles = roles.Where(r =>
                (r.Name != null && r.Name.Contains(filters.Search)) ||
                (r.Location!.Name != null && r.Location.Name.Contains(filters.Search)) ||
                (r.Department!.Name != null && r.Department.Name.Contains(filters.Search)));
        }

        // Apply pagination
        roles = roles
            .Skip((filters.PageNumber - 1) * filters.PageSize)
            .Take(filters.PageSize);

        var result = await roles.ToListAsync();
        return _roleMapper.ToRoleDto(result);
    }
}
