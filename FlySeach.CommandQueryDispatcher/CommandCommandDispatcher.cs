using Microsoft.Extensions.DependencyInjection;

namespace FlySeach.CommandQueryDispatcher;

public sealed class CommandDispatcher : ICommandDispatcher {
	private readonly IServiceProvider _serviceProvider;

	public CommandDispatcher(IServiceProvider serviceProvider) {
		_serviceProvider = serviceProvider;
	}

	public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken = default) {
		var hander = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
		return hander.Handle(command, cancellationToken);
	}
}
