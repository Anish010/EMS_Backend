using EMS.DB.Context;
using EMS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using EMS.DAL.DTO;
using EMS.DB.Models;

namespace EMS.DAL;

public class EmployeeDAL : IEmployeeDAL
{
    private readonly EMSContext _context;
    private readonly IEmployeeMapper _mapper;

    public EmployeeDAL(EMSContext context, IEmployeeMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> InsertAsync(EmployeeDto employee)
    {
        var newEmployee = _mapper.ToEmployeeModel(employee);
        _context.Employee.Add(newEmployee);
        await _context.SaveChangesAsync();
        return newEmployee.Id;
    }

    public async Task<List<EmployeeDto>> RetrieveAllAsync(EmployeeFilters? filters)
    {
        var employees = await _context.Employee
            .Skip((filters!.PageNumber - 1) * filters.PageSize)
            .Take(filters.PageSize)
            .Include(e => e.Location)
            .Include(e => e.Role)
            .Include(e => e.Department)
            .Include(e => e.Project)
            .Include(e => e.Mode)
            .ToListAsync();

        return _mapper.ToEmployeeDto(employees);
    }

    public async Task<EmployeeDto?> RetrieveByIdAsync(int? id)
    {
        var employee = await _context.Employee
                       .Include(e => e.Location)
                       .Include(e => e.Role)
                       .Include(e => e.Department)
                       .Include(e => e.Project)
                       .Include(e => e.Mode)
                       .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null)
        {
            return null;
        }

        return _mapper.ToEmployeeDto(employee);
    }

    public async Task<int> UpdateAsync(int id, UpdateEmployeeDto employee)
    {
        var existingEmployee = await _context.Employee.FindAsync(id);
        if (existingEmployee == null)
        {
            return 0;
        }

        existingEmployee.FirstName = !string.IsNullOrEmpty(employee.FirstName) ? employee.FirstName : existingEmployee.FirstName;
        existingEmployee.LastName = !string.IsNullOrEmpty(employee.LastName) ? employee.LastName : existingEmployee.LastName;
        existingEmployee.Dob = employee.Dob ?? existingEmployee.Dob;
        existingEmployee.Email = !string.IsNullOrEmpty(employee.Email) ? employee.Email : existingEmployee.Email;
        existingEmployee.MobileNumber = !string.IsNullOrEmpty(employee.MobileNumber) ? employee.MobileNumber : existingEmployee.MobileNumber;
        existingEmployee.JoiningDate = employee.JoiningDate ?? existingEmployee.JoiningDate;
        existingEmployee.LocationId = employee.LocationId ?? existingEmployee.LocationId;
        existingEmployee.RoleId = employee.RoleId ?? existingEmployee.RoleId;
        existingEmployee.DepartmentId = employee.DepartmentId ?? existingEmployee.DepartmentId;
        existingEmployee.ManagerId = employee.ManagerId ?? existingEmployee.ManagerId;
        existingEmployee.ProjectId = employee.ProjectId ?? existingEmployee.ProjectId;
        existingEmployee.ProfileImagePath = employee.ProfileImagePath ?? existingEmployee.ProfileImagePath;

        int rowsAffected = await _context.SaveChangesAsync();

        return rowsAffected;
    }

    public async Task<int> DeleteAsync(IEnumerable<int> ids)
    {
        var employeesToDelete = await _context.Employee.Where(e => ids.Contains(e.Id)).ToListAsync();

        if (employeesToDelete == null || !employeesToDelete.Any())
        {
            return 0;
        }

        _context.Employee.RemoveRange(employeesToDelete);
        int rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected;
    }


    public async Task<List<EmployeeDto>?> FilterAsync(EmployeeFilters? filters)
    {
        if (filters == null)
        {
            return null;
        }

        var employees = _context.Employee
            .Include(e => e.Location)
            .Include(e => e.Role)
            .Include(e => e.Department)
            .Include(e => e.Project)
            .Include(e => e.Mode)
            .AsQueryable();

        // Apply filters

        if (filters.Locations != null && filters.Locations.Count != 0)
        {
            employees = employees.Where(e => e.LocationId.HasValue && filters.Locations.Contains(e.LocationId.Value));
        }

        if (filters.Departments != null && filters.Departments.Count != 0)
        {
            employees = employees.Where(e => e.DepartmentId.HasValue && filters.Departments.Contains(e.DepartmentId.Value));
        }

        if (filters.Status != null && filters.Status.Count != 0)
        {
            var statusBooleans = filters.Status.Select(s => s == 1).ToList();
            employees = employees.Where(e => statusBooleans.Contains(e.Status));
        }

        if (!string.IsNullOrWhiteSpace(filters.Search))
        {
            employees = employees.Where(e =>
                (e.FirstName != null && e.FirstName.Contains(filters.Search)) ||
                (e.LastName != null && e.LastName.Contains(filters.Search)) ||
                (e.UID != null && e.UID.Contains(filters.Search)) ||
                (e.Role!.Name != null && e.Role.Name.Contains(filters.Search)) ||
                (e.Department!.Name != null && e.Department.Name.Contains(filters.Search)) ||
                (e.Location!.Name != null && e.Location.Name.Contains(filters.Search)) ||
                e.Id.ToString().Contains(filters.Search));
        }

        if (filters.Alphabet != null && filters.Alphabet.Count != 0)
        {
            employees = employees.Where(e =>
                !string.IsNullOrEmpty(e.FirstName) &&
                filters.Alphabet.Any(alphabet =>
                    e.FirstName.Substring(0, 1).ToUpper() == alphabet.ToString().ToUpper()));
        }

        // Apply pagination
        employees = employees
            .Skip((filters.PageNumber - 1) * filters.PageSize)
            .Take(filters.PageSize);

        var result = await employees.ToListAsync();
        return _mapper.ToEmployeeDto(result);
    }



    public async Task<int> CountAsync()
    {
        int count = await _context.Employee.CountAsync();
        return count;
    }

    public async Task<List<EmployeeDto>?> RetrieveByRoleIdAsync(int? id)
    {
        var employees = await _context.Employee
         .Include(e => e.Location)
         .Include(e => e.Role)
         .Include(e => e.Department)
         .Include(e => e.Project)
         .Include(e => e.Mode)
         .Where(e => e.RoleId == id)
         .ToListAsync();

        if (employees == null)
        {
            return null;
        }

        return _mapper.ToEmployeeDto(employees);
    }

    public async Task<List<EmployeeDto>?> RetrieveByDepartmentIdAsync(int? id)
    {
        var employees = await _context.Employee
           .Include(e => e.Location)
           .Include(e => e.Role)
           .Include(e => e.Department)
           .Include(e => e.Project)
           .Include(e => e.Mode)
           .Where(e => e.DepartmentId == id)
           .ToListAsync();

        if (employees == null)
        {
            return null;
        }

        return _mapper.ToEmployeeDto(employees);
    }

    public async Task<string?> UpdateEmployeeModeAsync(int? Id, int? modeStatusId)
    {
        Employee? employee = await _context.Employee
            .Include(e => e.Mode)
            .FirstOrDefaultAsync(e => e.Id == Id);

        if (employee == null)
        {
            return null;
        }

        // Update mode status
        employee.ModeStatusId = modeStatusId;
        _context.Employee.Update(employee);
        await _context.SaveChangesAsync();

        string? modeName = await _context.Employee
            .Where(e => e.Id == Id)
            .Select(e => e.Mode!.Name)
            .SingleOrDefaultAsync();

        return modeName;
    }

    public async Task<List<DepartmentEmployeeDto>> RetrieveGroupedByDepartmentsAsync()
    {
        // Retrieve all departments
        var departments = await _context.Department.ToListAsync();

        // Retrieve all employees and include department information
        var employees = await _context.Employee
            .Include(e => e.Department)
            .ToListAsync();

        // Group employees by department and calculate the count
        var groupedEmployees = employees
            .GroupBy(e => e.Department)
            .Select(g => new DepartmentEmployeeDto
            {
                DepartmentId = g.Key!.Id,
                DepartmentName = g.Key.Name,
                EmployeesCount = g.Count()
            })
            .ToList();

        // Ensure all departments are included, even those with zero employees
        var result = departments.Select(d => new DepartmentEmployeeDto
        {
            DepartmentId = d.Id,
            DepartmentName = d.Name,
            EmployeesCount = groupedEmployees.FirstOrDefault(ge => ge.DepartmentId == d.Id)?.EmployeesCount ?? 0
        }).ToList();

        return result;
    }
}