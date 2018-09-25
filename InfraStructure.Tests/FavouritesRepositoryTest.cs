using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entity;
using InfraStrucure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfraStructure.Tests
{
    [TestClass]
    public class FavouritesRepositoryTest
    {

        [TestMethod]
        public async Task GetByIdAsync()
        {
            // Arrange
            FavouriteRepositoryAsync repository = new FavouriteRepositoryAsync();

            // Act
            Favourite results = await repository.GetByIdAsync("umapathy.ramprakash@kasanova.it", "PRIMAVERA");

            // Assert
            Assert.IsNotNull(results);
        }


        [TestMethod]
        public async Task ListAsync()
        {
            // Arrange
            FavouriteRepositoryAsync repository = new FavouriteRepositoryAsync();

            // Act
            IEnumerable<Favourite> results = await repository.ListAsync(new { UserName = "umapathy.ramprakash@kasanova.it" });

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }
    }
}
