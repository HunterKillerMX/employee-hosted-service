using employee_hosted_config;

using Microsoft.Extensions.Logging;
using Dropbox.Api;
using System.Globalization;
using Dropbox.Api.Files;

namespace employee_hosted_infrastructure;

public class DropboxSaver : IDropboxSaver
{
    private string _dbxToken;
    private string _dbxFolder;
    private ILogger<IDropboxSaver> _logger;
    public DropboxSaver(ILogger<IDropboxSaver> logger, EmployeeHostedServiceConfig config)
    {
        _logger = logger;
        _dbxToken = config.DropboxAccessToken;
        _dbxFolder = config.DropboxFolder;
    }
    public async Task<bool> SaveToDropbox(byte[] csvFileBytes)
    {
        bool response = true;
        try
        {
            using(var dbx = new DropboxClient(_dbxToken))
            {
                using(var mem = new MemoryStream(csvFileBytes))
                {
                    var updated = await dbx.Files.UploadAsync(
                        _dbxFolder + "/" + "SSDEmployeesReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss", CultureInfo.InvariantCulture) + ".csv",
                        WriteMode.Overwrite.Instance,
                        body: mem
                    );
                }
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(@"Could not save file to Dropbox. Error message: {0}", ex.Message);
            response = false;
        }
        return response;
    }
}
