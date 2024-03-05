namespace employee_hosted_config;

public class EmployeeHostedServiceConfig
{
    public EmployeeHostedServiceConfig()
    {
        ApiBaseUri = "";
        ApiGetUri = "";
        DropboxAccessToken = "";
        DropboxFolder = "";
        ReportDestinations = "";
    }
    public string ApiBaseUri { get; set; }
    public string ApiGetUri { get; set; }
    public string DropboxAccessToken { get; set; }
    public string DropboxFolder { get; set; }
    public string ReportDestinations { get; set; }
}
