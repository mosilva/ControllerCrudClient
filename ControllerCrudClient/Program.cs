using ControllerCrudClient.Core.Interface;
using ControllerCrudClient.Core.Service;
using ControllerCrudClient.Filters;
using ControllerCrudClient.Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

//Add global filters

builder.Services.AddMvc(options =>
    options.Filters.Add<GeneralExceptionFilter>()
    );


// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//}
//);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mapeamento Containers

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

//Mapeamento filtros com inje��o de dep�ndencia

builder.Services.AddScoped<ActionFilterValidationInserctionCpf>();
builder.Services.AddScoped<ActionFilterCheckUpdateNome>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
