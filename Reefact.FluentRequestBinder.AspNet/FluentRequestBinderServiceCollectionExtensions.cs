#region Usings declarations

using System;

using Microsoft.Extensions.DependencyInjection;

using Reefact.FluentRequestBinder.Configuration;

#endregion

namespace Reefact.FluentRequestBinder.AspNet {

    /// <summary>
    ///     Provides extension methods for configuring the fluent request binder.
    /// </summary>
    public static class FluentRequestBinderServiceCollectionExtensions {

        #region Statics members declarations

        /// <summary>
        ///     Add fluent request binder services to the application using default configuration.
        /// </summary>
        /// <param name="services">The services descriptor.</param>
        /// <returns>The services descriptor.</returns>
        /// <exception cref="ArgumentNullException">Parameter <paramref name="services" /> cannot be null.</exception>
        public static IServiceCollection AddFluentRequestBinder(this IServiceCollection services) {
            if (services is null) { throw new ArgumentNullException(nameof(services)); }

            services.AddSingleton<RequestBinder>(_ => new DefaultRequestBinder(ValidationOptions.Instance));

            return services;
        }

        /// <summary>
        ///     Add fluent request binder services to the application using a custom configuration.
        /// </summary>
        /// <param name="services">The services descriptor.</param>
        /// <param name="configure">The configuration options.</param>
        /// <returns>The services descriptor.</returns>
        /// <exception cref="ArgumentNullException">Parameter <paramref name="services" /> cannot be null.</exception>
        public static IServiceCollection AddFluentRequestBinder(this IServiceCollection services, Action<ValidationOptionsBuilder> configure) {
            if (services is null) { throw new ArgumentNullException(nameof(services)); }

            ValidationOptionsBuilder builder = new();
            configure(builder);
            ValidationOptions.Configure(builder);
            services.AddSingleton<RequestBinder>(_ => new DefaultRequestBinder(ValidationOptions.Instance));

            return services;
        }

        #endregion

    }

}