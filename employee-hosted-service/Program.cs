using employee_hosted_service;
using employee_hosted_infrastructure;
using employee_hosted_config;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var section = builder.Configuration.GetSection("EmployeeHostedServiceConfig");
var employeeHostedConfig = section.Get<EmployeeHostedServiceConfig>();

builder.Services.AddSingleton(employeeHostedConfig ?? new EmployeeHostedServiceConfig());
builder.Services.AddSingleton<IApiConnector, ApiConnector>();
builder.Services.AddTransient<ICsvGenerator, CsvGenerator>();
builder.Services.AddTransient<IDropboxSaver, DropboxSaver>();

var host = builder.Build();
host.Run();
