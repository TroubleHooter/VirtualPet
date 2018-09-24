using System;
using System.Collections.Generic;
using VirtualPet.Domain.Entities;

namespace VirtualPet.Repository
{
    public class PetsRepository : IRepository<Pet>
    {
        public Pet GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Pet entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(Pet entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Pet entity)
        {
            //Not needed for the example
            throw new NotImplementedException();
        }
    }
}
