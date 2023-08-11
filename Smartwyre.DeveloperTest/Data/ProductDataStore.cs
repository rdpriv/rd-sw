using Smartwyre.DeveloperTest.Models;

namespace Smartwyre.DeveloperTest.Data;

internal class ProductDataStore : IReadOnlyDataStore<Product>
{
    public Product GetById(string identifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return new Product();
    }
}
