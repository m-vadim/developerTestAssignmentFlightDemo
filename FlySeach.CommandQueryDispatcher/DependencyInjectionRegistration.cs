using Microsoft.Extensions.DependencyInjection;

namespace FlySeach.CommandQueryDispatcher;

public static class DependencyInjectionRegistration {
	public static IServiceCollection RegisterDispatcher(this IServiceCollection services) {
		services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
		services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

		return services;
	}

	public static IServiceCollection RegisterQueries(this IServiceCollection services) {
		IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(a => a.GetTypes())
			.Where(a => a.IsClass && !a.IsAbstract && a.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

		foreach (var type in types) {
			IEnumerable<Type> interfaces = type.GetInterfaces()
				.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));

			foreach (var interfaceType in interfaces) {
				services.AddSingleton(interfaceType, type);
			}
		}

		return services;
	}

	public static IServiceCollection RegisterCommands(this IServiceCollection services) {
		IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(a => a.GetTypes())
			.Where(a => a is { IsClass: true, IsAbstract: false } && a.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)));

		foreach (var type in types) {
			IEnumerable<Type> interfaces = type.GetInterfaces()
				.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));

			foreach (var interfaceType in interfaces) {
				services.AddSingleton(interfaceType, type);
			}
		}

		return services;
	}
}
