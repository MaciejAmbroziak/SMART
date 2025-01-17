using Microsoft.EntityFrameworkCore;

namespace SMART.Domain
{
    public interface IDomainDbContext
    {
        DbSet<EquipmentContract> EquipmentContracts { get; set; }
        DbSet<ProcessEquipment> ProcessEquipments { get; set; }
        DbSet<ProductionFacility> ProductionFacilities { get; set; }
    }
}