using NUnit.Framework;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Models;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.Calculators
{
    internal class FixedRateRebateCalculatorShould : RebateCalculatorTestBase
    {
        protected override RebateCalculatorBase Calculator
            => new FixedRateRebateCalculator();

        [TestCase(IncentiveType.FixedRate)]
        [TestCase(IncentiveType.FixedRate | IncentiveType.AmountPerUom)]
        public void HaveCorrectSupportedIncentiveType(IncentiveType incentiveType)
        {
            var product = _product;
            product.SupportedIncentives = incentiveType;

            var result = Calculator.CalculateRebate(_rebate, product, _request);

            Assert.That(result.Success, Is.True);
        }

        [TestCase(IncentiveType.AmountPerUom)]
        [TestCase(IncentiveType.FixedCashAmount)]
        public void ReturnFalse_WhenIncorrectIncentiveType(IncentiveType incentiveType)
        {
            var product = _product;
            product.SupportedIncentives = incentiveType;

            var result = Calculator.CalculateRebate(_rebate, product, _request);

            Assert.That(result.Success, Is.False);
        }

        [TestCase(10, 10, 10, 1000)]
        [TestCase(1, 5, 10, 50)]
        public void Return_PriceTimesVolumeTimesPercentage(decimal price, decimal volume, decimal percentage, decimal expected)
        {
            var rebate = _rebate;
            rebate.Percentage = percentage;
            var request = _request;
            request.Volume = volume;
            var product = _product;
            product.Price = price;

            var result = Calculator.CalculateRebate(rebate, product, request);
            Assert.That(result.RebateAmount, Is.EqualTo(expected));
        }

        [TestCase(10, 10, 0)]
        [TestCase(10, 0, 10)]
        [TestCase(0, 10, 10)]
        public void ReturnFalse_WhenElementIsZero(decimal price, decimal volume, decimal percentage)
        {
            var rebate = _rebate;
            rebate.Percentage = percentage;
            var request = _request;
            request.Volume = volume;
            var product = _product;
            product.Price = price;
            var result = Calculator.CalculateRebate(rebate, product, request);
            Assert.That(result.Success, Is.False);
        }

        public static IEnumerable<CalculatorTestCase> CanCalculate = new[]
        {
            new CalculatorTestCase()
            {
                Rebate = _rebate,
                Product = _product,
                Request = _request,
                ExpectedRebateAmount = 10000
            }
        };

        public static IEnumerable<CalculatorTestCase> CannotCalculate = new[]
        {
            new CalculatorTestCase()
            {
                Rebate = _rebate,
                Product = new Product()
                {
                    SupportedIncentives = IncentiveType.AmountPerUom
                },
                Request = _request,
                ExpectedRebateAmount = 0
            }
        };

        private static Product _product => new Product()
        {
            Id = 1,
            Identifier = "Test",
            Price = 100,
            Uom = "Test",
            SupportedIncentives = IncentiveType.FixedRate
        };

        private static Rebate _rebate => new Rebate()
        {
            Identifier = "Test",
            Incentive = IncentiveType.FixedRate,
            Amount = 10,
            Percentage = 10
        };

        private static CalculateRebateRequest _request => new CalculateRebateRequest()
        {
            ProductIdentifier = "Test",
            RebateIdentifier = "Test",
            Volume = 10
        };
    }
}