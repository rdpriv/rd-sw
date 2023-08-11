using Smartwyre.DeveloperTest.Models;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    public interface IRebateCalculator
    {
        CalculateRebateResult CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request);
        IncentiveType IncentiveType { get; }
    }
}
