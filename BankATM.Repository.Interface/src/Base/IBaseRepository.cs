namespace BankATM.Repository.Interface.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task SaveAsync();
    }
}
