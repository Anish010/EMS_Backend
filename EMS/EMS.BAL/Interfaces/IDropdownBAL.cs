using EMS.DAL.DTO;
using EMS.DB.Models;

namespace EMS.BAL.Interfaces;

public interface IDropdownBAL
{
    public Task<List<Dropdown>?> GetLocationsAsync();
    public Task<List<Dropdown>?> GetDepartmentsAsync();
    public Task<List<Dropdown>?> GetManagersAsync();
    public Task<List<Dropdown>?> GetProjectsAsync();
}