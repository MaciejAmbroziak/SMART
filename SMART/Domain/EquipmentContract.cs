using SMART.Controllers;

namespace SMART.Domain
{
    public class EquipmentContract
    {
        public int Id { get; set; }
        public IEnumerable<ProcessEquipment> ProcessEquipment { get; set; }
        public ProductionFacility ProductionFacility { get; set; }
        public int EquipmentUnits
        {
            get { return ProcessEquipment.Count(); }
            private set { EquipmentUnits = value; }
        }
    }
}