using System.Net;
using Refit;

namespace FlySearch.AggregateApi.UnitTests;

public static class MockApiException {
	public static ApiException CreateApiException(HttpMethod method,  HttpStatusCode statusCode, string content) {
		return ApiException.Create(new HttpRequestMessage(), method, new HttpResponseMessage(statusCode), new RefitSettings()).Result;
	}
}
