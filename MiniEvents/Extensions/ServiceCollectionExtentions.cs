using Microsoft.Extensions.DependencyInjection;
using MiniEvents.Concretes;
using MiniEvents.Interfaces;
using System.Reflection;

namespace MiniEvents.Extensions {
    public static class ServiceCollectionExtentions {
        public static IServiceCollection AddEventHandlers(this IServiceCollection services, params Assembly[] assemblies) {
            if (assemblies.Length < 1) {
                throw new ArgumentNullException(nameof(assemblies));
            }

            foreach (var assembly in assemblies) {
                var handlers = assembly
                    .GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface)
                    .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                    .Where(x => x.Interface.IsGenericType && x.Interface.GetGenericTypeDefinition() == typeof(IEventHandler<>));

                foreach (var handler in handlers) {
                    services.AddScoped(handler.Interface, handler.Type);
                }
            }

            return services;
        }

        public static IServiceCollection AddEventPublisher(this IServiceCollection services) {
            services.AddScoped<IEventPublisher, EventPublisher>();
            return services;
        }

        public static IServiceCollection AddMiniEvents(this IServiceCollection services, params Assembly[] assemblies) {
            services.AddEventPublisher();
            services.AddEventHandlers(assemblies);
            return services;
        }
    }
}
