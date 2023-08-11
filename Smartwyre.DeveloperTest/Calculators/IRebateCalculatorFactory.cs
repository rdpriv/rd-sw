using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    public interface IRebateCalculatorFactory
    {
        public IRebateCalculator Create(IncentiveType incentiveType);
    }
}
