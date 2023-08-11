using System;
using System.Collections.Generic;
using System.Linq;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    internal class RebateCalculatorFactory : IRebateCalculatorFactory
    {
        private readonly IEnumerable<IRebateCalculator> _calculators;

        public RebateCalculatorFactory(IEnumerable<IRebateCalculator> calculators)
        {
            _calculators = calculators;
        }

        public IRebateCalculator Create(IncentiveType incentiveType)
        {
            return _calculators.FirstOrDefault(c => incentiveType.HasFlag(c.IncentiveType))
                ?? throw new ArgumentException($"No calculator found for incentive type {incentiveType}");
        }
    }
}
