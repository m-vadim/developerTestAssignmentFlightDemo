namespace FlySeach.CommandQueryDispatcher;

public interface IQueryDispatcher {
	Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery command, CancellationToken cancellationToken = default);
}
