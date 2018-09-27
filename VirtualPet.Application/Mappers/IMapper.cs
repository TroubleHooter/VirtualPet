using System;
namespace VirtualPet.Application.Mappers
{
     public interface IMapper<in TFrom, out TTo>
     {
         TTo Map(TFrom from);
     }
}
