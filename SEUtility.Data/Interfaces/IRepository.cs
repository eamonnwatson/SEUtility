namespace SEUtility.Data.Interfaces;

internal interface IRepository<T> where T : class
{
    IReadOnlyList<T> GetAll();
    int Add(IEnumerable<T> entities);
}
