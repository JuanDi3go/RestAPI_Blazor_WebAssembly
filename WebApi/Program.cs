using Application;
using Shared;
using Persistence;
using WebApi.Extensions;
using Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services from other layers
#region Services from other layers
builder.Services.AddIdentityInfraestructure(builder.Configuration);
builder.Services.AddSharedLayerServices();
builder.Services.AddApplicationLayerServices();
builder.Services.AddPersistenceLayerServices(builder.Configuration);
builder.Services.AddApiVersioningExtension();

#endregion





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.ErrorHandligMiddleware();
app.MapControllers();

app.Run();
