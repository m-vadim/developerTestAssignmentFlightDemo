using Microsoft.Extensions.DependencyInjection;

namespace FlySeach.CommandQueryDispatcher;

public sealed class QueryDispatcher : IQueryDispatcher {
	private readonly IServiceProvider _serviceProvider;

	public QueryDispatcher(IServiceProvider serviceProvider) {
		_serviceProvider = serviceProvider;
	}

	public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken = default) {
		var hander = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
		return hander.Handle(query, cancellationToken);
	}
}
