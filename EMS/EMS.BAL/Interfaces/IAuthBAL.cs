using EMS.DAL.DTO;
using EMS.DAL.Models;
namespace EMS.BAL.Interfaces;

public interface IAuthBAL
{
    public Task<AuthenticateResponse?> AuthenticateAsync(string email);
    public Task<AuthenticateResponse?> SignUpAsync(EmployeeDto employee);
    public Task LogoutAsync(int Id);
}
