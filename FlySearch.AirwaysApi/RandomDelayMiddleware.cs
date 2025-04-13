namespace FlySearch.AirwaysApi;

internal sealed class RandomDelayMiddleware {
	private readonly RequestDelegate _next;
	private readonly Random _random;

	public RandomDelayMiddleware(RequestDelegate next) {
		_next = next;
		_random = new Random(DateTime.Now.Millisecond);
	}

	public async Task InvokeAsync(HttpContext context) {
		var percentile = _random.Next(0, 100);
		if (percentile < 80) {
			// 80% of the time, do nothing.
			await Task.Delay(0);
		} else if (percentile < 90) {
			// 10% of the time, delay for 1 second.
			await Task.Delay(1000);
		} else {
			// 10% of the time, delay for 5 seconds.
			await Task.Delay(5000);
		}

		// Call the next delegate/middleware in the pipeline.
		await _next(context);
	}
}
