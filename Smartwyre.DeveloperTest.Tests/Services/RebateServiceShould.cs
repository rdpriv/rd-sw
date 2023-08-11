using Moq;
using NUnit.Framework;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Models;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.Services
{
    internal class RebateServiceShould
    {
        private Mock<IReadWriteDataStore<Rebate>> _rebateDataStore;
        private Mock<IReadOnlyDataStore<Product>> _productDataStore;
        private Mock<IRebateCalculatorFactory> _rebateCalculatorFactory;
        private Mock<IRebateCalculator> _rebateCalculator;
        private RebateService _rebateService;

        [SetUp]
        public void Setup()
        {
            _rebateDataStore = new Mock<IReadWriteDataStore<Rebate>>();
            _productDataStore = new Mock<IReadOnlyDataStore<Product>>();
            _rebateCalculatorFactory = new Mock<IRebateCalculatorFactory>();
            _rebateCalculator = new Mock<IRebateCalculator>();
            _rebateCalculatorFactory.Setup(x => x.Create(It.IsAny<IncentiveType>())).Returns(_rebateCalculator.Object);

            _rebateDataStore.Setup(x => x.GetById(It.IsAny<string>())).Returns(_rebate);
            _productDataStore.Setup(x => x.GetById(It.IsAny<string>())).Returns(Product);
            _rebateCalculator.Setup(x => x.CalculateRebate(It.IsAny<Rebate>(), It.IsAny<Product>(), It.IsAny<CalculateRebateRequest>()))
                .Returns(new CalculateRebateResult { Success = true });

            _rebateService = new RebateService(_rebateDataStore.Object, _productDataStore.Object, _rebateCalculatorFactory.Object);
        }

        [Test]
        public void UpdateRebateAmountAndReturnTrue()
        {
            var request = new CalculateRebateRequest
            {
                ProductIdentifier = "Test Product",
                RebateIdentifier = "Test Rebate",
                Volume = 1000
            };
            _rebateCalculator.Setup(x => x.CalculateRebate(It.IsAny<Rebate>(), It.IsAny<Product>(), It.IsAny<CalculateRebateRequest>()))
                .Returns(new CalculateRebateResult { Success = true, RebateAmount = 10 });

            var result = _rebateService.Calculate(request);
            Assert.That(result.Success, Is.True);
            _rebateDataStore.Verify(x => x.Update(It.Is<Rebate>(r => r.Amount == 10)), Times.Once);
        }

        private Rebate _rebate => new Rebate
        {
            Identifier = "Test Rebate",
            Incentive = IncentiveType.FixedRate,
            Amount = 0.1m
        };

        private Product Product => new Product
        {
            Id = 1,
            Identifier = "Test Product",
            SupportedIncentives = IncentiveType.FixedRate | IncentiveType.AmountPerUom,
            Uom = "Test UOM",
            Price = 10
        };
    }
}
