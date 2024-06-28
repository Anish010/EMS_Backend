using EMS.BAL.Interfaces;
using Serilog;
using EMS.DAL.Interfaces;
using EMS.DAL.Models;
using EMS.DAL.DTO;

namespace EMS.BAL;
public class AuthBAL : IAuthBAL
{
    private readonly IAuthDAL _authDal;
    private readonly ILogger _logger;

    public AuthBAL(ILogger logger, IAuthDAL authDal)
    {
        _authDal = authDal;
        _logger = logger;
    }

    public async Task<AuthenticateResponse?> AuthenticateAsync(string email)
    {
        try
        {
            return await _authDal.AuthenticateAsync(email) ?? null;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error in authenticating employee: {ex.Message}");
            throw;
        }
    }

    public async Task<AuthenticateResponse?> SignUpAsync(EmployeeDto employee)
    {
        try
        {
            return await _authDal.RegisterAsync(employee);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while inserting employee: {ex.Message}");
            throw;
        }
    }

    public async Task LogoutAsync(int Id)
    {
        try
        {
            await _authDal.LogoutAsync(Id);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while logging out: {ex.Message}");
            throw;
        }
    }
}
