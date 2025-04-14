using FluentValidation;
using FlySeach.CommandQueryDispatcher;
using FlySearch.AggregateApi.AirwaysClient.HotAir;
using FlySearch.AggregateApi.AirwaysClient.RoyalAir;
using FlySearch.AggregateApi.Features.FindFlight;
using FlySearch.AggregateApi.Options;
using Refit;
using IAirlineApi = FlySearch.AggregateApi.Infrastructure.IAirlineApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.RegisterDispatcher();
builder.Services.RegisterCommands();
builder.Services.RegisterQueries();

builder.Services.AddSwaggerGen(opt => opt.CustomSchemaIds(type => type.FullName));

builder.Services.Configure<RoyalAirApiOptions>(builder.Configuration.GetSection("RoyalAirApi"));
builder.Services.AddRefitClient<IRoyalAirApi>()
	.ConfigureHttpClient(c => {
		c.BaseAddress = new Uri(builder.Configuration.GetSection("RoyalAirApi:BaseAddress").Value ?? throw new InvalidOperationException());
		c.Timeout = TimeSpan.FromSeconds(3);
	});
builder.Services.AddScoped<IAirlineApi, HotAirAirlineClient>();

builder.Services.Configure<HotAirApiOptions>(builder.Configuration.GetSection("HotAirApi"));
builder.Services.AddRefitClient<IHotAirApi>()
	.ConfigureHttpClient(c => {
		c.BaseAddress = new Uri(builder.Configuration.GetSection("HotAirApi:BaseAddress").Value ?? throw new InvalidOperationException());
		c.Timeout = TimeSpan.FromSeconds(3);
	});
builder.Services.AddScoped<IAirlineApi, RoyalAirAirlineClient>();

builder.Services.AddScoped<IAirlineApi[]>(serviceProvider =>
											  serviceProvider.GetServices<IAirlineApi>().ToArray());
builder.Services.AddScoped<IValidator<FindFlightRequest>, FindFlightRequestValidator>();
builder.Services.AddScoped<IFlightAggregator, FlightAggregator>();



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
