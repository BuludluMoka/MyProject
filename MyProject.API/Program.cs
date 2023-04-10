using MyProject.Application;
using MyProject.Infrastructure;
using MyProject.Persistenc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InjectPersistenceServices(builder.Configuration);
builder.Services.InjectInfrastructureServices(builder.Configuration);
builder.Services.InjectApplicationService();

builder.Services.AddControllers()/*.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateInvoiceValidator>())*/;
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
