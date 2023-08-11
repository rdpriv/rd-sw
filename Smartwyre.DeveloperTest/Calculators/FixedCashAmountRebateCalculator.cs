using Smartwyre.DeveloperTest.Models;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    internal class FixedCashAmountRebateCalculator : RebateCalculatorBase
    {
        public override IncentiveType IncentiveType => IncentiveType.FixedCashAmount;

        protected override bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return base.CanCalculate(rebate, product, request)
                     && rebate.Amount != 0;
        }

        protected override decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount;
        }
    }
}
