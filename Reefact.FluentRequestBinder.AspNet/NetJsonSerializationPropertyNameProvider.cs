#region Usings declarations

using System;
using System.Reflection;
using System.Text.Json.Serialization;

#endregion

namespace Reefact.FluentRequestBinder.AspNet {

    public sealed class NetJsonSerializationPropertyNameProvider : PropertyNameProvider {

        #region Fields declarations

        private readonly bool _strict;

        #endregion

        #region Constructors declarations

        public NetJsonSerializationPropertyNameProvider(bool strict = true) {
            _strict = strict;
        }

        #endregion

        /// <inheritdoc />
        public string GetName(PropertyInfo propertyInfo) {
            if (propertyInfo is null) { throw new ArgumentNullException(nameof(propertyInfo)); }

            JsonPropertyNameAttribute? jsonPropertyAttribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            if (jsonPropertyAttribute != null) { return jsonPropertyAttribute.Name; }
            if (!_strict) { return propertyInfo.Name; }

            throw FluentRequestBinderAspNetException.PropertyDoesNotHaveJsonPropertyNameAttribute(propertyInfo);
        }

    }

}