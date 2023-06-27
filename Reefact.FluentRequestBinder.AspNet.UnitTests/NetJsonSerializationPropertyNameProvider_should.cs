#region Usings declarations

using System;
using System.Reflection;
using System.Text.Json.Serialization;

using NFluent;

using Xunit;

#endregion

namespace Reefact.FluentRequestBinder.AspNet.UnitTests {

    public class NetJsonSerializationPropertyNameProvider_should {

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void provide_the_property_name_defined_in_property_JsonPropertyNameAttribute_when_defined(bool useStrictMode) {
            // Setup
            NetJsonSerializationPropertyNameProvider propertyNameProvider = new(useStrictMode);
            PropertyInfo?                            propertyInfo         = typeof(DemoteMember_v1).GetProperty(nameof(DemoteMember_v1.TeamId));
            // Exercise
            string name = propertyNameProvider.GetArgumentNameFrom(propertyInfo!);
            // Verify
            Check.That(name).IsEqualTo("team-id");
        }

        [Fact]
        public void throw_an_understandable_exception_when_trying_to_retrieve_the_property_name_of_a_property_having_no_JsonPropertyNameAttribute_defined() {
            // Setup
            NetJsonSerializationPropertyNameProvider propertyNameProvider = new();
            PropertyInfo?                            propertyInfo         = typeof(DemoteMember_v1).GetProperty(nameof(DemoteMember_v1.MemberUtCode));
            // Exercise & verify
            Check.ThatCode(() => propertyNameProvider.GetArgumentNameFrom(propertyInfo!))
                 .Throws<FluentRequestBinderAspNetException>()
                 .WithMessage("Property 'Reefact.FluentRequestBinder.AspNet.UnitTests.NetJsonSerializationPropertyNameProvider_should+DemoteMember_v1:MemberUtCode' has no JsonPropertyNameAttribute attribute.");
        }

        [Fact]
        public void provide_the_default_property_name_when_trying_to_retrieve_in_not_strict_mode_the_property_name_of_a_property_having_no_JsonPropertyNameAttribute_defined() {
            // Setup
            NetJsonSerializationPropertyNameProvider propertyNameProvider = new(false);
            PropertyInfo?                            propertyInfo         = typeof(DemoteMember_v1).GetProperty(nameof(DemoteMember_v1.MemberUtCode));
            // Exercise
            string name = propertyNameProvider.GetArgumentNameFrom(propertyInfo!);
            // Verify
            Check.That(name).IsEqualTo("MemberUtCode");
        }

        [Fact]
        public void throw_an_exception_when_trying_to_retrieve_the_name_of_a_null_property() {
            // Setup
            NetJsonSerializationPropertyNameProvider propertyNameProvider = new();
            // Exercise & verify
            Check.ThatCode(() => propertyNameProvider.GetArgumentNameFrom(null!))
                 .Throws<ArgumentNullException>();
        }

        #region Nested types declarations

        private sealed class DemoteMember_v1 {

            [JsonPropertyName("team-id")] public Guid TeamId { get; set; }

            public string? MemberUtCode { get; set; }

        }

        #endregion

    }

}