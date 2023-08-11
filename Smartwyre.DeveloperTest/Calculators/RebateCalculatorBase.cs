using Smartwyre.DeveloperTest.Models;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    internal abstract class RebateCalculatorBase : IRebateCalculator
    {
        public abstract IncentiveType IncentiveType { get; }

        public CalculateRebateResult CalculateRebate(Rebate rebate, 
            Product product, 
            CalculateRebateRequest request)
        {
            var result = new CalculateRebateResult();
            if (CanCalculate(rebate, product, request))
            {
                result.RebateAmount = Calculate(rebate, product, request);
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }
            return result;
        }

        protected virtual bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate != null 
                   && product != null 
                   && product.SupportedIncentives.HasFlag(IncentiveType);
        }

        protected abstract decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}