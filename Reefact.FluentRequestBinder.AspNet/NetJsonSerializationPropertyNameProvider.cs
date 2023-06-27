#region Usings declarations

using System;
using System.Reflection;
using System.Text.Json.Serialization;

#endregion

namespace Reefact.FluentRequestBinder.AspNet {

    /// <summary>
    ///     This class is used to provide the name of arguments as declared in the contract via
    ///     the <see cref="JsonPropertyNameAttribute" /> attributes.
    /// </summary>
    public sealed class NetJsonSerializationPropertyNameProvider : ArgumentNameProvider {

        #region Constructors declarations

        /// <summary>
        ///     Instantiate a ne <see cref="NetJsonSerializationPropertyNameProvider" /> instance.
        /// </summary>
        /// <param name="strict">
        ///     Indicates if an exception must be thrown if arguments are not decorated by a
        ///     <see cref="JsonPropertyNameAttribute" /> attribute (<c>true</c>)
        ///     or if, in this case, the name of the class property should be used (<c>false</c>).
        /// </param>
        public NetJsonSerializationPropertyNameProvider(bool strict = true) {
            IsStrict = strict;
        }

        #endregion

        /// <summary>
        ///     Indicates if an exception must be thrown if arguments are not decorated by a
        ///     <see cref="JsonPropertyNameAttribute" /> attribute (<c>true</c>)
        ///     or if, in this case, the name of the class property should be used (<c>false</c>).
        /// </summary>
        public bool IsStrict { get; }

        /// <inheritdoc />
        public string GetArgumentNameFrom(PropertyInfo propertyInfo) {
            if (propertyInfo is null) { throw new ArgumentNullException(nameof(propertyInfo)); }

            JsonPropertyNameAttribute? jsonPropertyAttribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            if (jsonPropertyAttribute != null) { return jsonPropertyAttribute.Name; }
            if (!IsStrict) { return propertyInfo.Name; }

            throw FluentRequestBinderAspNetException.PropertyDoesNotHaveJsonPropertyNameAttribute(propertyInfo);
        }

    }

}