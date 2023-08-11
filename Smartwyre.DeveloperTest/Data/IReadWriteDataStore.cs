namespace Smartwyre.DeveloperTest.Data
{
    public interface IReadWriteDataStore<T> : IReadOnlyDataStore<T>
    {
        void Update(T item);
    }
}
