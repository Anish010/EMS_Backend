namespace EMS.API.Helpers;
public enum ErrorCodes
{
    INTERNAL_SERVER_ERROR ,
    EMPLOYEES_NOT_FOUND,
    EMPLOYEE_NOT_FOUND,
    FAILED_TO_ADD_EMPLOYEE,
    FAILED_TO_UPDATE_EMPLOYEE,
    FAILED_TO_DELETE_EMPLOYEE,
    FAILED_TO_SEARCH_EMPLOYEE,
    FAILED_TO_FILTER_EMPLOYEES,
    FAILED_TO_COUNT_EMPLOYEES,
    FAILED_TO_AUTHENTICATE,
    UNAUTHORIZED,
    FAILED_TO_REGISTER,
    FAILED_TO_ADD_ROLE,
    ROLES_NOT_FOUND,
    FAILED_TO_GET_ROLES,
    DEPARTMENTS_NOT_FOUND,
    LOCATIONS_NOT_FOUND,
    MANAGERS_NOT_FOUND,
    PROJECTS_NOT_FOUND,
    FAILED_TO_LOGOUT,
    EMPLOYEE_ID_IS_REQUIRED,
    INVALID_CREDENTIALS
}
