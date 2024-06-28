using EMS.API.Helpers;
using EMS.BAL;
using EMS.BAL.Interfaces;
using EMS.DAL;
using EMS.DAL.DTO;
using EMS.DAL.Interfaces;
using EMS.DB.Context;
using EMS.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EMS.API.Controllers;

[Route("/Api/Role")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleBAL _roleBal;
    private readonly Serilog.ILogger _logger;

    public RoleController(EMSContext context, Serilog.ILogger logger, IRoleBAL roleBAL)
    {
        _logger = logger;
        _roleBal = roleBAL;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddRoleAsync([FromBody] RoleDto role)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        else
        {
            try
            {
                var result = await _roleBal.AddRoleAsync(role);
                return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), result);
            }
            catch (Exception)
            {
                return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.FAILED_TO_ADD_ROLE.ToString());
            }
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRolesAsync([FromQuery] RoleFilters filters)
    {
        try
        {
            if (filters != null)
            {
                if (filters.DepartmentId != null)
                {
                    var rolesByDept = await _roleBal.GetRolesByDeptIdAsync(filters);
                    if (rolesByDept == null || rolesByDept.Count == 0)
                    {
                        return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.ROLES_NOT_FOUND.ToString());
                    }
                    return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), rolesByDept);
                }
                else if (filters.RoleId != null)
                {
                    var role = await _roleBal.GetRolesByRoleIdAsync(filters);
                    if (role == null || role.Count == 0)
                    {
                        return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.ROLES_NOT_FOUND.ToString());
                    }
                    return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), role);
                }
                else if(filters.LocationId != null)
                {
                    var role = await _roleBal.GetRolesByLocIdAsync(filters);
                    if (role == null || role.Count == 0)
                    {
                        return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.ROLES_NOT_FOUND.ToString());
                    }
                    return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), role);
                }
                else
                {
                    var roles = await _roleBal.FilterRolesAsync(filters);
                    if (roles == null || roles.Count == 0)
                    {
                        return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.ROLES_NOT_FOUND.ToString());
                    }
                    return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), roles);
                }
            }
            else
            {
                var roles = await _roleBal.GetAllAsync(filters);
                if (roles == null)
                {
                    return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.ROLES_NOT_FOUND.ToString());
                }
                return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), roles);
            }

        }
        catch (Exception)
        {
            return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.INTERNAL_SERVER_ERROR.ToString());
        }
    }
}
