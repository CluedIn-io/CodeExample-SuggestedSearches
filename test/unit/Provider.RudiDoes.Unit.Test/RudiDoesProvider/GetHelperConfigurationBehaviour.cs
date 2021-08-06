using System;
using System.Linq;
using AutoFixture.Xunit2;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.RudiDoes.Core;
using Shouldly;
using Xunit;

namespace CluedIn.Provider.RudiDoes.Unit.Test.RudiDoesProvider
{
    public class GetHelperConfigurationBehaviour : RudiDoesProviderTest
    {
        private readonly CrawlJobData _jobData;

        public GetHelperConfigurationBehaviour()
        {
            _jobData = new RudiDoesCrawlJobData();
        }

        [Fact]
        public void Throws_ArgumentNullException_With_Null_CrawlJobData_Parameter()
        {
            var ex = Assert.Throws<AggregateException>(
                () => Sut.GetHelperConfiguration(null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid())
                    .Wait());

            ((ArgumentNullException)ex.InnerExceptions.Single())
                .ParamName
                .ShouldBe("jobData");
        }

        [Theory]
        [InlineAutoData]
        public void Returns_ValidDictionary_Instance(Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            Sut.GetHelperConfiguration(null, _jobData, organizationId, userId, providerDefinitionId)
                .Result
                .ShouldNotBeNull();
        }

        // TODO Add test for throws arg exception for incorrect data param


        [Theory]
        [InlineAutoData("ApiKey", "ApiKey", "some-value")]
        // TODO add data for other properties that need populating
        // Fill in the values for expected results ....
        public void Returns_Expected_Data(string key, string propertyName, object expectedValue, Guid organizationId, Guid userId, Guid providerDefinitionId) // TODO add additional parameters to populate CrawlJobData instance
        {
            var property = _jobData.GetType().GetProperty(propertyName);
            property?.SetValue(_jobData, expectedValue);

            var result = Sut.GetHelperConfiguration(null, _jobData, organizationId, userId, providerDefinitionId)
                            .Result;

            result
                .ContainsKey(key)
                .ShouldBeTrue(
                    $"{key} not found in results");

            result[key]
                .ShouldBe(expectedValue);
        }
    }
}
