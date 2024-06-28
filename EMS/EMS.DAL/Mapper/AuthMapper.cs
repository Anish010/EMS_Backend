using EMS.DAL.Interfaces;
using EMS.DAL.Models;
using EMS.DB.Models;

namespace EMS.DAL.Mapper;

public class AuthMapper : IAuthMapper
{
    public AuthenticateResponse? ToAuthResponse(Employee employee, bool isAuth)
    {
        if (employee == null)
        {
            return new AuthenticateResponse();
        }

        return new AuthenticateResponse
        {
            Password = employee.Password,
            IsAuthenticated = isAuth,
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            UID = employee.UID,
            RoleName = employee.Role?.Name,
            ProfileImagePath = employee.ProfileImagePath,
        };
    }
}