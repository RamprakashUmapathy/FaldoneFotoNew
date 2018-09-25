using ApplicationCore.Entity;
using InfraStrucure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStrucure.Repositories
{
    public interface IFavouriteRepositoryAsync : IAsyncRepository<Favourite, string, string>
    {
    }
}
