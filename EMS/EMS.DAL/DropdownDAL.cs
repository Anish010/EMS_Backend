using EMS.DB.Models;
using EMS.DAL.DTO;
using EMS.DAL.Interfaces;
using EMS.DB.Context;
using Serilog;
using Microsoft.EntityFrameworkCore;
namespace EMS.DAL;

public class DropdownDAL : IDropdownDAL
{
    private readonly EMSContext _context;
    public DropdownDAL(EMSContext context)
    {
        _context = context;
    }

    public async Task<List<Dropdown>?> GetLocationsListAsync()
    {
        var locations = await _context.Location.ToListAsync();
        var dropdownList = locations.Select(location => new Dropdown
        {
            Id = location.Id,
            Name = location.Name
        }).ToList();

        return dropdownList;
    }

    public async Task<List<Dropdown>?> GetDepartmentsListAsync()
    {
        var departments = await _context.Department
                                .Select(department => new Dropdown
                                {
                                    Id = department.Id,
                                    Name = department.Name
                                })
                                .ToListAsync();

        return departments;
    }

    public async Task<List<Dropdown>?> GetManagersListAsync()
    {
        var employeesQuery = await _context.Employee.ToListAsync();

        var managersQuery = employeesQuery.Where(e => e.IsManager);

        var dropdownItems = managersQuery.Select(e => new Dropdown
        {
            Id = e.Id,
            Name = e.FirstName + " " + e.LastName
        });

        var managers = dropdownItems.ToList();
        return managers;
    }

    public async Task<List<Dropdown>?> GetProjectsListAsync()
    {
        var projectsQuery = await _context.Project.ToListAsync();

        var dropdownItems = projectsQuery.Select(p => new Dropdown
        {
            Id = p.Id,
            Name = p.Name
        });

        var projects = dropdownItems.ToList();

        return projects;

    }
}
