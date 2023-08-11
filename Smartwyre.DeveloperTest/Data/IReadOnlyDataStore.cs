namespace Smartwyre.DeveloperTest.Data
{
    public interface IReadOnlyDataStore<T>
    {
        T GetById(string identifier);
    }
}
