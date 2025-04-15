using FlySeach.CommandQueryDispatcher;
using FlySearch.AirwaysApi;
using FlySearch.AirwaysApi.Airways.HotAir;
using FlySearch.AirwaysApi.Airways.RoyalAir;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.RegisterDispatcher();
builder.Services.RegisterCommands();
builder.Services.RegisterQueries();

builder.Services.RegisterHotAir();
builder.Services.RegisterRoyalAir();

builder.Services.AddSwaggerGen(opt => opt.CustomSchemaIds(type => type.FullName));

var app = builder.Build();
// app.UseMiddleware<RandomDelayMiddleware>(); enable this to simulate a delay in the response

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.RegisterHotAirEndpoints();
app.RegisterRoyalAirEndpoints();

app.UseHttpsRedirection();
app.Run();
