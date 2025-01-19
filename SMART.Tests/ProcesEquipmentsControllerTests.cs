using Microsoft.EntityFrameworkCore;
using SMART.Controllers;
using SMART.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART.Tests
{
    internal class ProcessEquipmentsControllerTests
    {
        private readonly List<ProcessEquipment> _processEquipmentList;

        public ProcessEquipmentsControllerTests()
        {
            _processEquipmentList = new List<ProcessEquipment>();
            _processEquipmentList.Add(new ProcessEquipment()
            {
                Name = "Lorem ipsum",
                Code = "swhdfuoirhgfweorgh",
                Area = 100
            });
            _processEquipmentList.Add(new ProcessEquipment()
            {
                Name = "lwsbnhjbfnv",
                Code = "owehfgwegro",
                Area = 120,
            });
            _processEquipmentList.Add(new ProcessEquipment()
            {
                Name = "lwnfgwngfwerg",
                Code = "wefjrwejwigtj",
                Area = 12
            });
            _processEquipmentList.Add(new ProcessEquipment()
            {
                Name = " sdfgrg rgerg grrgreg rr  ",
                Code = "wpfjwpgrfjprweg",
                Area = 24
            });

        }

        public async Task GetProcessEquipments_ReturnsCountOfEquipment()
        {
            var options = new DbContextOptionsBuilder<DomainDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            DomainDbContext dbContext = new DomainDbContext(options);
            dbContext.ProcessEquipments.AddRange(_processEquipmentList);
            dbContext.SaveChanges();
            var processEquipmentsController = new ProcessEquipmentsController(dbContext);

            var result = (await processEquipmentsController.GetProcessEquipments()).Value.Count();

            Assert.Equal(_processEquipmentList.Count(), result);
        }
    }
}
