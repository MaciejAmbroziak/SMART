namespace SMART.Domain
{
    public class EquipmentContract
    {
        public int Id { get; set; }
        public ProcessEquipment ProcessEquipment { get; set; }
        public ProductionFacility ProductionFacility { get; set; }
        public int EquipmentUnits { get; set; }

    }
}
