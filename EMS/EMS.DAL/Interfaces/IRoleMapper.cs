using EMS.DAL.DTO;
using EMS.DB.Models;

namespace EMS.DAL.Interfaces;

public interface IRoleMapper
{
    public List<RoleDto> ToRoleDto(IEnumerable<Role> roles);
    public RoleDto? ToRoleDto(Role role);
    public Role ToRoleModel(RoleDto RoleDto);
}