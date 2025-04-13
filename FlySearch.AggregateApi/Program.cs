using FlySeach.CommandQueryDispatcher;
using FlySearch.AggregateApi.Features.FindFlight;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.RegisterDispatcher();
builder.Services.RegisterCommands();
builder.Services.RegisterQueries();

builder.Services.AddSwaggerGen(opt => opt.CustomSchemaIds(type => type.FullName));

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
