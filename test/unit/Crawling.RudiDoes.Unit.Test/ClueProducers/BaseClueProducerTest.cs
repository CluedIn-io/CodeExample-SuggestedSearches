using System;
using AutoFixture.Xunit2;
using CluedIn.Core.Data;
using CluedIn.Crawling;
using CluedIn.Crawling.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace Crawling.RudiDoes.Unit.Test.ClueProducers
{
    public abstract class BaseClueProducerTest<T>
    {
        protected readonly Mock<ILogger> _logger;
        protected readonly Mock<IClueFactory> _clueFactory;

        protected abstract BaseClueProducer<T> Sut { get; }
        protected abstract EntityType ExpectedEntityType { get; }

        protected BaseClueProducerTest()
        {
            _logger = new Mock<ILogger>();
            _clueFactory = new Mock<IClueFactory>();
            var entityCode = new Mock<IEntityCode>();

            _clueFactory.Setup(f =>
                f.Create(It.IsAny<EntityType>(), It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(new Clue(entityCode.Object, Guid.NewGuid()));
        }

        [Theory]
        [InlineAutoData]
        public void ClueHasId(T input) =>
            Sut.MakeClue(input, new Guid())
                .Id.ShouldNotBeSameAs(Guid.Empty);

        [Theory]
        [InlineAutoData]
        public void ClueHasName(T input) =>
            Sut.MakeClue(input, new Guid())
                .Data.EntityData.Name.ShouldNotBeEmpty();

        [Theory]
        [InlineAutoData]
        protected void ClueIsOfType(T input)
        {
            Sut.MakeClue(input, new Guid());
            _clueFactory.Verify(
                f => f.Create(ExpectedEntityType, It.IsAny<string>(), It.IsAny<Guid>())
                );
        }
    }
}
