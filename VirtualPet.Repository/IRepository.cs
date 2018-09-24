using System.Collections.Generic;

namespace VirtualPet.Repository
{
    public interface IRepository<T>
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        void Update(T entity);

        void Insert(T entity);

        void Delete(T entity);
    }
}
