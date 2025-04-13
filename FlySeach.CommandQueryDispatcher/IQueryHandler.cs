namespace FlySeach.CommandQueryDispatcher;

public interface IQueryHandler<in TQuery, TQueryResult> {
	Task<TQueryResult> Handle(TQuery command, CancellationToken calCancellationToken);
}
