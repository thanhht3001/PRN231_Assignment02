using BusinessObjects.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<User>("User");
modelBuilder.EntitySet<Book>("Book");
modelBuilder.EntitySet<Author>("Author");
modelBuilder.EntitySet<Publisher>("Publisher");

builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null)
    .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseODataBatching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
