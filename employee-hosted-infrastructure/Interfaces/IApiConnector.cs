using employee_hosted_model;
namespace employee_hosted_infrastructure;

public interface IApiConnector
{
    Task<List<EmployeeReadDTO>> getEmployees();
}
