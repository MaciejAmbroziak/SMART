using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMART.Controllers;
using SMART.Domain;

namespace SMART.Tests
{
    public class ProductionFacilitiesControllerTests
    {
        private readonly DomainDbContext _dbContext;
        private readonly ProductionFacilitiesController _productionFacilitiesController;

        public ProductionFacilitiesControllerTests()
        {
            var options = new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase($"InMemoryDb {Guid.NewGuid()}")
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
                    Code = "BBBBBBB12 SS",
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
            _dbContext.ProductionFacilities.Add(
                new ProductionFacility()
                {
                    Id = 5,
                    Name = "Room 5",
                    StandardArea = 200,
                    Code = "ABC  123456",
                    Occupied = false
                });
            _dbContext.ProductionFacilities.Add(
                new ProductionFacility()
                {
                    Id = 6,
                    Name = "Room 6",
                    StandardArea = 201,
                    Code = "ABC 123456",
                    Occupied = false
                });


            _dbContext.SaveChanges();

            _productionFacilitiesController = new ProductionFacilitiesController(_dbContext);
        }

        [Fact]
        public async Task GetProductionFacilities_ReturnsTheNumberOfFacilities()
        {
            //Act
            var result = (await _productionFacilitiesController.GetProductionFacilities()).Value.Count();
            //Assert
            Assert.Equal(6, result);
        }
        [Fact]
        public async Task GetProductionFacilitiesTask_DoesNotReturnNull()
        {
            //Act
            var result = await _productionFacilitiesController.GetProductionFacilities();
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetProductionFacilitiesTaskActionResult_DoesNotReturnNull()
        {
            //Act
            var result = (await _productionFacilitiesController.GetProductionFacilities()).Value;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetProductionFacilities_ReturnsProperResult()
        {
            //Act
            var result = (await _productionFacilitiesController.GetProductionFacilities()).Value.First();
            
            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Room 1", result.Name);
            Assert.Equal(10.5, result.StandardArea, 0.001);
            Assert.Equal("AAABBBCCC1234567890", result.Code);
            Assert.Equal(true, result.Occupied);
        }

        [Fact]
        public async Task GetProductionFacilityTask_DoesNotReturnNull()
        {
            var result = await _productionFacilitiesController.GetProductionFacility(1);
            
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetProductionFacilityTaskActionResult_DoesNotReturnNull()
        {
            var result = (await _productionFacilitiesController.GetProductionFacility(1)).Value;
        
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1, "Room 1", 10.5, "AAABBBCCC1234567890", true)]
        [InlineData(2, "Room 2", 12.31, "BBBBBBB12 SS", false)]
        [InlineData(3, "Room 3", 14.2, "1234567890", true)]
        public async Task GetProductionFacilityTaskActionResult_DoesReturnProperValues(int id, string name, double area, string code, bool occupied)
        {
            var result = (await _productionFacilitiesController.GetProductionFacility(id)).Value;

            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(area, result.StandardArea, 0.001);
            Assert.Equal(code, result.Code); 
            Assert.Equal(occupied, result.Occupied);
        }

        [Fact]
        public async Task GetProductionFacility_IfDataNotExist_ReturnsNotFound()
        {
            var result = (await _productionFacilitiesController.GetProductionFacility(99)).Result;

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PutProductionFacility_ReturnsBadRequestForDifferentId()
        {
            var productionFacility = new ProductionFacility()
            {
                Id = 2,
                Name = "Room 1",
                StandardArea = 10.5,
                Code = "AAABBBCCC1234567890",
                Occupied = true,
            };
            var result = (await _productionFacilitiesController.PutProductionFacility(1, productionFacility));

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData("BBBBBBB12")]
        [InlineData("BBBBBBB12KK")]
        [InlineData("1234567890")]
        [InlineData("BBBB BBB12")]
        [InlineData("1234567890 ")]
        [InlineData(" BBBBBBB12")]
        [InlineData(" 123 4567890")]
        [InlineData(" 123 456 7890 ")]
        [InlineData(" 123 4567890 KK")]
        [InlineData(" 123 456 7890 KK")]
        public async Task PutProductionFacility_ReturnsBadRequestForExistingCode(string code)
        {
            var productionFacility = new ProductionFacility()
            {
                Id = 1,
                Name = "Room 1",
                StandardArea = 10.5,
                Code = code,
                Occupied = true,
            };
            var result = (await _productionFacilitiesController.PutProductionFacility(1, productionFacility));

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task PutProductionFacility_ChangesDbCorrectly(int id)
        {
            //Arange
            var productionFacility = _dbContext.ProductionFacilities.Where(a => a.Id == id).First();
            productionFacility.Name = "Abrakadabra";
            productionFacility.Code = "Lorem ipsum";
            productionFacility.Occupied = true;
            productionFacility.StandardArea = 1.2;

            //Act
            await _productionFacilitiesController.PutProductionFacility(productionFacility.Id, productionFacility);
            var result = _dbContext.ProductionFacilities.Where(_a => _a.Id == id).First();

            //Assert
            Assert.Equal(productionFacility, result);
        }

        //TODO Despite many attempts I do not know how to simulate throwing DbUpdateConcurrencyException
    }
}