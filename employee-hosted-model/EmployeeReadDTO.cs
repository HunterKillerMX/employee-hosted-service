namespace employee_hosted_model;

public record EmployeeReadDTO(Guid Id, string? Name, string? Position, DateTime? HiringDate, double? Salary);
