#region Usings declarations

using System;
using System.Reflection;

using Reefact.FluentRequestBinder.AspNet.Resources;

#endregion

namespace Reefact.FluentRequestBinder.AspNet {

    /// <summary>
    ///     Represents any fluent request bind for ASP.Net exception.
    /// </summary>
    public class FluentRequestBinderAspNetException : ApplicationException {

        #region Statics members declarations

        internal static FluentRequestBinderAspNetException PropertyDoesNotHaveJsonPropertyNameAttribute(PropertyInfo propertyInfo) {
            string propertyName              = propertyInfo.Name;
            string propertyDeclaringTypeName = propertyInfo.DeclaringType?.FullName ?? string.Empty;
            string message                   = string.Format(ExceptionMessage.FluentRequestBinderAspNet_PropertyHasNoJsonPropertyNameAttribute, propertyDeclaringTypeName, propertyName);

            return new FluentRequestBinderAspNetException(message);
        }

        #endregion

        #region Constructors declarations

        private FluentRequestBinderAspNetException(string message) : base(message) { }

        #endregion

    }

}