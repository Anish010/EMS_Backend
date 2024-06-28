using EMS.DAL.DTO;
using EMS.DAL.Models;

namespace EMS.DAL.Interfaces;

public interface IAuthDAL
{
    public Task<AuthenticateResponse?> AuthenticateAsync(string email);
    public Task<AuthenticateResponse?> RegisterAsync(EmployeeDto employee);
    public Task LogoutAsync(int Id);
}
