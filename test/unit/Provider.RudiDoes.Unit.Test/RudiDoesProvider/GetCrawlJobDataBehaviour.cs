using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using CluedIn.Core.Crawling;
using CluedIn.Core.Providers;
using CluedIn.Crawling.RudiDoes.Core;
using CluedIn.RelatedEntities;
using Xunit;

namespace CluedIn.Provider.RudiDoes.Unit.Test.RudiDoesProvider
{
    public class GetCrawlJobDataBehaviour : RudiDoesProviderTest
    {
        private readonly ProviderUpdateContext _context;

        public GetCrawlJobDataBehaviour()
        {
            _context = null;
        }

        [Theory]
        [InlineAutoData]
        public async Task ReturnsData(Dictionary<string, object> dictionary, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            Assert.NotNull(
                await Sut.GetCrawlJobData(_context, dictionary, organizationId, userId, providerDefinitionId));
        }

        [Theory]
        [InlineAutoData(RudiDoesConstants.KeyName.ApiKey, nameof(RudiDoesCrawlJobData.ApiKey))]
        public async Task InitializesProperties(string key, string propertyName, string sampleValue, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            var dictionary = new Dictionary<string, object>()
            {
                [key] = sampleValue
            };

            var sut = await Sut.GetCrawlJobData(_context, dictionary, organizationId, userId, providerDefinitionId);
            Assert.Equal(sampleValue, sut.GetType().GetProperty(propertyName).GetValue(sut));
        }

        [Theory]
        [InlineAutoData]
        public async Task RudiDoesCrawlJobDataReturned(Dictionary<string, object> dictionary, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            Assert.IsType<CluedIn.Crawling.RudiDoes.Core.RudiDoesCrawlJobData>(
                await Sut.GetCrawlJobData(_context, dictionary, organizationId, userId, providerDefinitionId));
        }

        [Fact]
        public void CheckForExpectedIRelatedEntitiesProviders()
        {
            var classes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetInterfaces().Contains(typeof(IRelatedEntitiesProvider)))
                .Where(type => type.AssemblyQualifiedName.Contains("RudiDoes"));

            int i = 0;
            foreach (var classItem in classes)
            {
                Assert.True("RudiDoesRelatedEntitiesProvider" == classItem.Name);
                i++;
            }

            Assert.True(i == 1);
        }
    }
}
