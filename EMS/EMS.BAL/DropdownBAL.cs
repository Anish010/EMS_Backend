using EMS.BAL.Interfaces;
using EMS.DB.Models;
using EMS.DAL.DTO;
using EMS.DAL.Interfaces;
using Serilog;

namespace EMS.BAL;

public class DropdownBAL : IDropdownBAL
{
    private readonly IDropdownDAL _dropdownDAL;
    private readonly ILogger _logger;

    public DropdownBAL(IDropdownDAL dropdownDAL, ILogger logger)
    {
        _logger = logger;
        _dropdownDAL = dropdownDAL;
    }

    public async Task<List<Dropdown>?> GetDepartmentsAsync()
    {
        try
        {
            var departments = await _dropdownDAL.GetDepartmentsListAsync() ?? [];
            if (departments == null)
            {
                return [];
            }
            return departments;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error fetching departments: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Dropdown>?> GetLocationsAsync()
    {
        try
        {
            List<Dropdown> locations = await _dropdownDAL.GetLocationsListAsync() ?? [];
            return locations;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error fetching locations: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Dropdown>?> GetManagersAsync()
    {
        try
        {
            List<Dropdown> managers = await _dropdownDAL.GetManagersListAsync() ?? [];
            return managers;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error fetching managers: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Dropdown>?> GetProjectsAsync()
    {
        try
        {
            List<Dropdown> projects = await _dropdownDAL.GetProjectsListAsync() ?? [];
            return projects;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error fetching projects: {ex.Message}");
            throw;
        }
    }

    private static Dictionary<int, string> ConvertListToDictionary<T>(List<T> items)
    {
        if (items == null)
        {
            return [];
        }

        var dictionary = new Dictionary<int, string>();
        foreach (var item in items)
        {
            var idProperty = typeof(T).GetProperty("Id");
            var nameProperty = typeof(T).GetProperty("Name");

            if (idProperty != null && nameProperty != null)
            {
                var idValue = idProperty.GetValue(item);
                var nameValue = nameProperty.GetValue(item);

                if (idValue != null && nameValue != null && idValue is int && nameValue is string)
                {
                    dictionary[(int)idValue] = (string)nameValue;
                }
            }
        }
        return dictionary;
    }
}