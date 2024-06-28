using EMS.DAL.Models;
using EMS.DB.Models;

namespace EMS.DAL.Interfaces;

public interface IAuthMapper
{
    public AuthenticateResponse? ToAuthResponse(Employee employee, bool isAuth);
}
