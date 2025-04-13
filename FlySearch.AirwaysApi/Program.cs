using FluentValidation;
using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi.Airways.HotAir.FindFlight;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.RegisterDispatcher();
builder.Services.RegisterCommands();
builder.Services.RegisterQueries();

builder.Services.AddScoped<IValidator<FindFlightRequest>, FindFlightRequestValidator>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.AddFindFlightEndpoint();

app.UseHttpsRedirection();
app.Run();
