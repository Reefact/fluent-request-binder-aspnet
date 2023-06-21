#region Usings declarations

using System;

using Microsoft.Extensions.DependencyInjection;

using Reefact.FluentRequestBinder.Configuration;

#endregion

namespace Reefact.FluentRequestBinder.AspNet {

    public static class FluentRequestBinderServiceCollectionExtensions {

        #region Statics members declarations

        public static IServiceCollection AddFluentRequestBinder(this IServiceCollection services) {
            if (services is null) { throw new ArgumentNullException(nameof(services)); }

            services.AddSingleton<RequestBinder>(_ => new DefaultRequestBinder(ValidationOptions.Instance));

            return services;
        }

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