using Microsoft.EntityFrameworkCore;
using SMART.Domain;
using System.Configuration;
List<ProductionFacility> productionFacilityList = new List<ProductionFacility>();
List<ProcessEquipment> processEquipmentList = new List<ProcessEquipment>();
Random random = new Random();
var options = new DbContextOptionsBuilder<DomainDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;Database=SMART;Trusted_Connection=True;TrustServerCertificate=true;").Options;
using (DomainDbContext _context = new DomainDbContext(options))
{
    for (int i = 0; i < 2000; i++)
    {
        productionFacilityList.Add(new ProductionFacility()
        {
            Name = $"Room {i}",
            Code = Guid.NewGuid().ToString(),
            Occupied = random.NextDouble() >= 0.5,
            StandardArea = random.NextDouble() * 100,
        });
    }
    for (int i = 0; i< 10000; i++)
    {
        processEquipmentList.Add(new ProcessEquipment()
        {
            Area = random.NextDouble() * 50,
            Code = Guid.NewGuid().ToString(),
            Name = $"Process equipment {i}"
        });
    }
    _context.ProductionFacilities.AddRange(productionFacilityList);
    _context.ProcessEquipments.AddRange(processEquipmentList);
    _context.SaveChanges();
}
