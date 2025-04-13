namespace FlySeach.CommandQueryDispatcher;

public interface ICommandHandler<in TCommand, TCommandResult> {
	Task<TCommandResult> Handle(TCommand command, CancellationToken calCancellationToken);
}
