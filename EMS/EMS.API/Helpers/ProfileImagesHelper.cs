using EMS.DAL.DTO;
using System.Configuration;

namespace EMS.API.Helpers;

public class ProfileImagesHelper
{
    private readonly Serilog.ILogger _logger;
    private readonly string ImageUploadDirectory;
    private readonly string folderPath;


    public ProfileImagesHelper(Serilog.ILogger logger, IConfiguration configuration)
    {
        _logger = logger;
        folderPath = Path.Combine(configuration.GetValue<string>("AppSettings:ImageUploadDirectory") ?? "StaticFiles", "Images");
        ImageUploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
        if (!Directory.Exists(ImageUploadDirectory))
        {
            Directory.CreateDirectory(ImageUploadDirectory);
        }

    }
    public async Task LoadProfileImages(List<EmployeeDto> employees)
    {
        foreach (var employee in employees)
        {
            await LoadProfileImages(employee);
        }
    }

    public async Task LoadProfileImages(EmployeeDto employee)
    {
        if (!string.IsNullOrEmpty(employee.ProfileImagePath))
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), employee.ProfileImagePath);

                if (System.IO.File.Exists(fullPath))
                {
                    using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            employee.ProfileImageData = memoryStream.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error loading profile image for employee {employee.Id}: {ex.Message}");
            }
        }
    }

    public async Task<string?> HandleProfileImageUpload(IFormFile? profileImage, int employeeId)
    {
        if (profileImage == null || profileImage.Length == 0)
        {
            return null;
        }

        var uniqueFileName = $"{employeeId}_ProfileImage{Path.GetExtension(profileImage.FileName)}";
        var fullPath = Path.Combine(this.ImageUploadDirectory, uniqueFileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await profileImage.CopyToAsync(stream);
        }

        var relativePath = Path.Combine(this.folderPath, uniqueFileName);
        _logger.Information("File Path : " + relativePath);

        return relativePath;
    }

}