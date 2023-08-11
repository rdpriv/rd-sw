using Moq;
using NUnit.Framework;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.NewFolder
{
    internal class RebateCalculatorFactoryShould
    {
        private IEnumerable<IRebateCalculator> _calculators;
        private Mock<IRebateCalculator> _rebateCalculator;
        private RebateCalculatorFactory _rebateCalculatorFactory;

        [SetUp]
        public void Setup()
        {
            _rebateCalculator = new Mock<IRebateCalculator>();
            _calculators = new List<IRebateCalculator> { _rebateCalculator.Object };
            _rebateCalculatorFactory = new RebateCalculatorFactory(_calculators);

            _rebateCalculator
                .Setup(x => x.IncentiveType)
                .Returns(IncentiveType.FixedRate);
        }

        [TestCase(IncentiveType.FixedRate)]
        [TestCase(IncentiveType.AmountPerUom | IncentiveType.FixedRate)]
        public void ReturnCalculatorWhenIncentiveTypeMatches(IncentiveType incentiveType)
        {
            var result = _rebateCalculatorFactory.Create(incentiveType);
            Assert.That(result, Is.EqualTo(_rebateCalculator.Object));
        }

        [Test]
        public void ThrowArgumentExceptionWhenIncentiveTypeDoesNotMatch()
        {
            Assert.Throws<ArgumentException>(() => _rebateCalculatorFactory.Create(IncentiveType.AmountPerUom));
        }
    }
}
