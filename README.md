# <b>Employee Management System</b>

## <b>Overview</b>

The <b>Employee Management System</b> is a command-line application designed to manage employee records efficiently. It provides various commands to perform operations such as adding, viewing, deleting, updating, searching, filtering, and obtaining counts of employee records.

## <b>Installation</b>

To run the application, ensure you have the .NET SDK installed on your machine. Then, follow these steps:

- Clone the repository to your local machine.
- Navigate to the project directory i.e <b>'EMS'</b> directory.
- Run <b>`dotnet build`</b> to build the application.

## <b>Usage</b>

### Commands

- `--add-emp` : Add a new employee to the system.
- `--show-emp` : Display the list of employees.
- `--delete-emp` : Delete an employee from the system. (Input: Employee Number)
- `--update-emp` : Update details of an existing employee.
- `--search-emp` : Search for details of a specific employee.
- `--filter-emp` : Filter the list of employees based on specific criteria.
- `--count-emp` : Get the count of employees in the system.
- `--add-role` : Add a new role to the system.
- `--show-role` : Display the list of roles.

## <b>Environment Variables</b>

The application uses an <b>\`appsettings.json\`</b> file to store configuration details. You need to specify the connection string to the database server using <b>\`ConnectionString\`</b> environment variable in the <b>\`appsettings.json\`</b> file.

Example <b>\`appsettings.json\`</b> :

```json
{
 "ConnectionString" : "Server=myServerAddress;Database=myDataBase;Integrated Security=true;TrustServerCertificate=true"
}

```

## <b>Running the Application</b>

- Ensure the <b>\`appsettings.json\`</b> file is correctly configured with the path to the JSON file containing employee data.
- Open a command prompt or terminal.
- Navigate to the directory where the application is located.
- To run directly from the built executable, go to bin\Debug\net8.0 in the project directory, open a command terminal, and run <b>`EMS.exe <command>`</b>.
- Alternatively, you can run the application using <b>`dotnet run <command>`</b> in the project directory.
- Run the desired command from the list of available commands mentioned above.

## <b>Contributing</b>

Contributions are welcome! If you have any ideas for improvements or feature requests, feel free to open an issue or submit a pull request.
