namespace RapidPay.Domain.Interfaces
{
    public interface IBaseRepository<TModel> : IDisposable
    {
        Task<List<TModel>> SearchAsync();
        Task<TModel> FindAsync(Guid id);        
        Task CreateAsync(TModel model);
        Task CreateManyAsync(List<TModel> models);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(Guid id);
    }
}
