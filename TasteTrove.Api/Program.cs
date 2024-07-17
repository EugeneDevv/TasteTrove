using Microsoft.AspNetCore.Mvc.Infrastructure;
using TasteTrove.Api.Common.Errors;
using TasteTrove.Application;
using TasteTrove.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication()
    .AddInfrastructure(builder.Configuration);
    
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, TasteTroveProblemDetailsFactory>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

