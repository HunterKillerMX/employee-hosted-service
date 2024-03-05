using employee_hosted_model;

using System.IO;
using Microsoft.Extensions.Logging;
using CsvHelper;
using System.Globalization;

namespace employee_hosted_infrastructure;

public class CsvGenerator : ICsvGenerator
{
    private ILogger<ICsvGenerator> _logger;
    public CsvGenerator(ILogger<ICsvGenerator> logger)
    {
        _logger = logger;
    }

    public async Task<byte[]> GenerateCsvFromApi(List<EmployeeReadDTO> employeeReadDTOs)
    {
        using (var response = new MemoryStream())
        {
            try
            {
                using StreamWriter streamWriter = new(response);
                using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    await csv.WriteRecordsAsync(employeeReadDTOs);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(@"Could not turn API response into CSV file. Error message: {0}", ex.Message);
            }
            return response.ToArray();
        }
        
    }
}
