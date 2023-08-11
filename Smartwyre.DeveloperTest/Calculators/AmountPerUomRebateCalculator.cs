using Smartwyre.DeveloperTest.Models;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    internal class AmountPerUomRebateCalculator : RebateCalculatorBase
    {
        public override IncentiveType IncentiveType => IncentiveType.AmountPerUom;

        protected override bool CanCalculate(Rebate rebate, Product product,
            CalculateRebateRequest request)
        {
            return base.CanCalculate(rebate, product, request)
                   && !(rebate.Amount == 0 || request.Volume == 0);
        }
        protected override decimal Calculate(Rebate rebate, 
            Product product, 
            CalculateRebateRequest request)
        { 
            return rebate.Amount * request.Volume;
        }
    }
}
