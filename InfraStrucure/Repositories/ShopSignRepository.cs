using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfraStrucure.Repositories
{
    public class ShopSignRepositoryAsync : IShopSignRepositoryAsync
    {
        public async static Task<IEnumerable<ShopSign>> ReadAllItems()
        {
            ShopSignRepositoryAsync repository = new ShopSignRepositoryAsync();
            return await repository.ListAllAsync();
        }

        public Task<ShopSign> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShopSign>> ListAllAsync()
        {
            Task<IEnumerable<ShopSign>> shops = Task<IEnumerable<ShopSign>>.Factory.StartNew(() =>
            {
                List<ShopSign> signs = new List<ShopSign>()
                {
                    new ShopSign()
                    {
                         Id = "K",
                         Name = "KASANOVA"
                    },
                    new ShopSign()
                    {
                         Id = "K+",
                         Name = "KASANOVA+"
                    },
                    new ShopSign()
                    {
                         Id = "COI",
                         Name = "COIMPORT"
                    },
                    new ShopSign()
                    {
                         Id = "ODK",
                         Name = "L'Outlet"
                    },
                    new ShopSign()
                    {
                         Id = "IT",
                         Name = "Italian Factory"
                    },
                    new ShopSign()
                    {
                         Id = "CSA",
                         Name = "Casa sull'albero"
                    },
                    new ShopSign()
                    {
                         Id = "KIK",
                         Name = "Le Kikke"
                    },
                    new ShopSign()
                    {
                         Id = "EC",
                         Name = "E-Commerce"
                    }
                };
                return signs;
            });
            return shops;
        }

        public Task<IEnumerable<ShopSign>> ListAsync(dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public Task<ShopSign> AddAsync(ShopSign entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ShopSign entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ShopSign entity)
        {
            throw new NotImplementedException();
        }
    }
}
