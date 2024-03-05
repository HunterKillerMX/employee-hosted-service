using employee_hosted_config;
using employee_hosted_model;

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace employee_hosted_infrastructure;

public class ApiConnector : IApiConnector
{
    
    private static string? _apiBaseUri;
    private string? _apiGetUri;
    private ILogger<IApiConnector> _logger;

    public ApiConnector(ILogger<IApiConnector> logger, EmployeeHostedServiceConfig config)
    {
        _logger = logger;
        _apiBaseUri = config.ApiBaseUri ?? "";
        _apiGetUri = config.ApiGetUri;
    }

    private HttpClient commonClient = new()
    {
        BaseAddress = new Uri(_apiBaseUri!),
        Timeout = new TimeSpan(0, 1, 0)
    };

    public async Task<List<EmployeeReadDTO>> getEmployees()
    {
        var response = new List<EmployeeReadDTO>();
        try
        {
            using HttpResponseMessage httpResponse = await commonClient.GetAsync(_apiGetUri);

            httpResponse.EnsureSuccessStatusCode();
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            response = JsonConvert.DeserializeObject<List<EmployeeReadDTO>>(jsonResponse) ?? new List<EmployeeReadDTO>();
        }
        catch(Exception ex)
        {
            _logger.LogError(@"Could not obtain response from {0}. Error Message: {1}", _apiGetUri, ex.Message);
        }
        return response;
    }
}
