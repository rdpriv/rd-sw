using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Models;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IReadWriteDataStore<Rebate> _rebateDataStore;
    private readonly IReadOnlyDataStore<Product> _productDataStore;
    private readonly IRebateCalculatorFactory _rebateCalculatorFactory;

    public RebateService(IReadWriteDataStore<Rebate> rebateDataStore, 
        IReadOnlyDataStore<Product> productDataStore, IRebateCalculatorFactory rebateCalculatorFactory)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _rebateCalculatorFactory = rebateCalculatorFactory;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = _rebateDataStore.GetById(request.RebateIdentifier);
        var product = _productDataStore.GetById(request.ProductIdentifier);

        var result = _rebateCalculatorFactory.Create(rebate.Incentive)
            .CalculateRebate(rebate, product, request);

        if (!result.Success) return result;
        
        rebate.Amount = result.RebateAmount;
        _rebateDataStore.Update(rebate);

        return result;
    }
}
