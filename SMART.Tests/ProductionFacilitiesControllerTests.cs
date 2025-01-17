using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using NuGet.Configuration;
using SMART.Controllers;
using SMART.Domain;
using Xunit;

namespace SMART.Tests
{
    public class ProductionFacilitiesControllerTests
    {
        private readonly DomainDbContext _dbContext;
        private readonly ProductionFacilitiesController _productionFacilitiesController;

        public ProductionFacilitiesControllerTests()
        {
            // create DomainDbContextMock
            var options = new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .Options;
            _dbContext = new DomainDbContext(options);

            _dbContext.ProductionFacilities.Add(
                new ProductionFacility()
                {
                    Id = 1,
                    Name = "Room 1",
                    StandardArea = 10.5,
                    Code = "AAABBBCCC1234567890",
                    Occupied = true,
                });
            _dbContext.ProductionFacilities.Add(
                new ProductionFacility()
                {
                    Id = 2,
                    Name = "Room 2",
                    StandardArea = 12.31,
                    Code = "BBBBBBB12",
                    Occupied = false
                });
            _dbContext.ProductionFacilities.Add(
                new ProductionFacility()
                {
                    Id = 3,
                    Name = "Room 3",
                    StandardArea = 14.2,
                    Code = "1234567890",
                    Occupied = true,
                });
            _dbContext.ProductionFacilities.Add(
                new ProductionFacility()
                {
                    Id = 4,
                    Name = "Room 4",
                    StandardArea = 200,
                    Code = "Lsingdlwnbgkj-12",
                    Occupied = false
                });
            _dbContext.SaveChanges();

            _productionFacilitiesController = new ProductionFacilitiesController(_dbContext);
        }

        [Fact]
        public async void GetProductionFacilities_ReturnsTheNumberOfFacilities()
        {
            //Arrange? or //Act?
            var result = (await _productionFacilitiesController.GetProductionFacilities()).Value.Count();
            //Assert?
            Assert.Equal(4, result);
        }
        //TODO verify if the test my look like that
    }
}