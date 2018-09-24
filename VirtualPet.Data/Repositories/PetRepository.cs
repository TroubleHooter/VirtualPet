using Microsoft.EntityFrameworkCore;
using VirtualPet.Domain.Entities;

namespace VirtualPet.Data.Repositories
{
    public class PetRepository : Repository<Pet>
    {
        public PetRepository(DbContext context) : base(context)
        {

        }

        public void GetPetByUserId(int userId)
        {
            dbSet.Find(userId);
        }
    }
}
