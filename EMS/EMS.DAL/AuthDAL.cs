using EMS.DAL.DTO;
using EMS.DAL.Interfaces;
using EMS.DAL.Mapper;
using EMS.DAL.Models;
using EMS.DB.Context;
using EMS.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.DAL;

public class AuthDAL : IAuthDAL
{

    private readonly EMSContext _context;
    private readonly IAuthMapper _authMapper;
    private readonly IEmployeeMapper _employeeMapper;

    public AuthDAL(EMSContext context)
    {
        _context = context;
        _authMapper = new AuthMapper();
        _employeeMapper = new EmployeeMapper();
    }

    public async Task<AuthenticateResponse?> AuthenticateAsync(string email)
    {
        Employee? employee = await _context.Employee
            .Include(e => e.Role).
            SingleOrDefaultAsync(x => x.Email == email);

        if (employee == null)
        {
            return null;
        }

        employee.Status = true;
        _context.Employee.Update(employee);
        await _context.SaveChangesAsync();

        return _authMapper.ToAuthResponse(employee, true);
    }

    public async Task<AuthenticateResponse?> RegisterAsync(EmployeeDto employeeDto)
    {
        var newEmployee = _employeeMapper.ToEmployeeModel(employeeDto);
        newEmployee.Status = true;
        _context.Employee.Add(newEmployee);

        int rowsAffected = await _context.SaveChangesAsync();
        if (rowsAffected == 0)
        {
            return new AuthenticateResponse();
        }

        return _authMapper.ToAuthResponse(newEmployee, true);
    }

    public async Task LogoutAsync(int Id)
    {
        Employee? employee = await _context.Employee.SingleOrDefaultAsync(x => x.Id == Id);

        if (employee == null)
        {
            return;
        }

        employee.Status = false;
        _context.Employee.Update(employee);
        await _context.SaveChangesAsync();
    }
}