using NUnit.Framework;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Models;

namespace Smartwyre.DeveloperTest.Tests.Calculators
{
    internal abstract class RebateCalculatorTestBase
    {
        protected abstract RebateCalculatorBase Calculator { get; }
        protected static IEnumerable<CalculatorTestCase> CanCalculate;
        protected static IEnumerable<CalculatorTestCase> CannotCalculate;

        [TestCaseSource(nameof(CanCalculate))]
        public virtual void ReturnSuccessWhenCanCalculate(CalculatorTestCase input)
        {
            var result = Calculator.CalculateRebate(input.Rebate, input.Product, input.Request);
            Assert.That(result.Success, Is.True);
            Assert.That(result.RebateAmount, Is.EqualTo(input.ExpectedRebateAmount));
        }

        [TestCaseSource(nameof(CannotCalculate))]
        public virtual void ReturnFailureWhenCannotCalculate(CalculatorTestCase input)
        {
            var result = Calculator.CalculateRebate(input.Rebate, input.Product, input.Request);

            Assert.That(result.Success, Is.False);
            Assert.That(result.RebateAmount, Is.EqualTo(input.ExpectedRebateAmount));
        }
    }

    internal class CalculatorTestCase
    {
        public Rebate Rebate { get; set; }
        public Product Product { get; set; }
        public CalculateRebateRequest Request { get; set; }
        public decimal ExpectedRebateAmount { get; set; }
    }
}
