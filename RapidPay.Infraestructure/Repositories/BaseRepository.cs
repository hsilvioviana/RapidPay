using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Interfaces;
using RapidPay.Domain.Models;

namespace RapidPay.Infraestructure.Repositories
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel: BaseModel, new()
    {
        protected readonly DbContext Db;
        protected readonly DbSet<TModel> DbSet;

        public BaseRepository(DbContext context)
        {
            Db = context;
            DbSet = Db.Set<TModel>();
        }

        public virtual async Task<List<TModel>> SearchAsync()
        { 
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TModel> FindAsync(Guid id)
        {
            return await DbSet.FindAsync(id);        
        }

        public virtual async Task CreateAsync(TModel model)
        {
            DbSet.Add(model);
            await SaveChangesAsync();
        }

        public virtual async Task CreateManyAsync(List<TModel> models)
        {
            DbSet.AddRange(models);
            await SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            Db.ChangeTracker.Clear();
            DbSet.Update(model);
            await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            Db.ChangeTracker.Clear();
            DbSet.Remove(new TModel { Id = id });
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Db?.Dispose();
        }
    }
}
