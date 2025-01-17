namespace SMART.Domain
{
    public interface IProductionFacility
    {
        string Code { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        bool Occupied { get; set; }
        double StandardArea { get; set; }
    }
}