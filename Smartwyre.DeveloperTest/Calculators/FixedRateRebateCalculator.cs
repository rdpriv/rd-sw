using Smartwyre.DeveloperTest.Models;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    internal class FixedRateRebateCalculator : RebateCalculatorBase
    {
        public override IncentiveType IncentiveType => IncentiveType.FixedRate;

        protected override bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return base.CanCalculate(rebate, product, request)
                     && !(rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0);
        }

        protected override decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.Price * rebate.Percentage * request.Volume;
        }
    }
}
