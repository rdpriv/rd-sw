using Smartwyre.DeveloperTest.Models;

namespace Smartwyre.DeveloperTest.Data;

internal class RebateDataStore : IReadWriteDataStore<Rebate>
{
    public Rebate GetById(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return new Rebate();
    }

    public void Update(Rebate account)
    {
        // Update account in database, code removed for brevity
    }
}
