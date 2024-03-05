using employee_hosted_model;

namespace employee_hosted_infrastructure;

public interface ICsvGenerator
{
    Task<byte[]> GenerateCsvFromApi(List<EmployeeReadDTO> employeeReadDTOs);
}
