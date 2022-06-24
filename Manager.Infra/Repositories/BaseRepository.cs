using Microsoft.EntityFrameworkCore;
using Manager.Infra.Interfaces;
using Manager.Infra.Context;
using Manager.Domain.Entities;

namespace Manager.Infra.Repositories
{
    public class BaseRepository<T>: IBaseRepository<T> where T : Base
    {
        private readonly ManagerContext managerContext;
        public BaseRepository(ManagerContext context)
        {
            managerContext = context;  
        }
        public virtual async Task<T> Create(T obj)
        {
            managerContext.Add(obj);
            await managerContext.SaveChangesAsync();
            return obj;
        }

        public virtual async Task<T> Update(T obj)
        {
            managerContext.Entry(obj).State = EntityState.Modified;
            await managerContext.SaveChangesAsync();
            return obj;
        }

        public virtual async Task Remove(long id)
        {
            var obj = await Get(id);
            managerContext.Remove(obj);
            await managerContext.SaveChangesAsync();
        }

        public virtual async Task<T> Get(long id)
        {
            var obj = await managerContext.Set<T>()
                                          .AsNoTracking()
                                          .Where(x => x.Id == id)
                                          .FirstOrDefaultAsync();
                                          
            if(obj == null)
                new Exception("Dado não contrado");
            return obj;
        }

        public virtual async Task<List<T>> Get()
        {
            return await managerContext.Set<T>()
                                        .AsNoTracking()
                                        .ToListAsync();
        }
    }
}