using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VirtualPet.Data.Repositories
{
    public abstract class Repository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        IEnumerable<T> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        //void Update(T entity);

        //void Insert(T entity);

        //void Delete(T entity);
    }
}
