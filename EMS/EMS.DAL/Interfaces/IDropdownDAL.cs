using System.Data.SqlClient;
using EMS.DB.Models;
using EMS.DAL.DTO;

namespace EMS.DAL.Interfaces;

public interface IDropdownDAL
{
    public Task<List<Dropdown>?> GetLocationsListAsync();
    public Task<List<Dropdown>?> GetDepartmentsListAsync();
    public Task<List<Dropdown>?> GetManagersListAsync();
    public Task<List<Dropdown>?> GetProjectsListAsync();
}