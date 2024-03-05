using employee_hosted_infrastructure;
using employee_hosted_config;

namespace employee_hosted_service;

public class BackgroundReportGenerator : IBackgroundReportGenerator
{
    private string[] _reportDestinations;
    private IApiConnector _apiConnector;
    private ICsvGenerator _csvGenerator;
    private IDropboxSaver _dropboxSaver;

    public BackgroundReportGenerator(IApiConnector apiConnector, ICsvGenerator csvGenerator, IDropboxSaver dropboxSaver, EmployeeHostedServiceConfig config)
    {
        _apiConnector = apiConnector;
        _csvGenerator = csvGenerator;
        _dropboxSaver = dropboxSaver;
        _reportDestinations = config.ReportDestinations.Split(',');
    }
    public async Task DoWork(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            //Get employees list
            var employeeReadDTOs = await _apiConnector.getEmployees();
            var csvFileBytes = await _csvGenerator.GenerateCsvFromApi(employeeReadDTOs);

            if (_reportDestinations.Where(x => x == "Dropbox").Any())
            {
                if (!await _dropboxSaver.SaveToDropbox(csvFileBytes))
                {
                    // TODO: Notify sysadmin something is wrong.
                }
            }
            // TODO: Implement other file destinations, like Google Drive, OneDrive, AWS S3.
            // TODO: Also implement other functionalities, like sending via email or Whatsapp.

            await Task.Delay(new TimeSpan(1, 0, 0), cancellationToken);
        }
    }
}
