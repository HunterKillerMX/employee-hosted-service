namespace employee_hosted_service;

public interface IBackgroundReportGenerator
{
    Task DoWork(CancellationToken cancellationToken);
}
