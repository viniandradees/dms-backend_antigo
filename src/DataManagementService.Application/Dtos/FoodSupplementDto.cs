using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class FoodSupplementDto
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int SupplementId { get; set; }
        public decimal Quantity { get; set; }
        public int MeasurementUnitId { get; set; }


        public Food Food { get; set; }
        public Supplement Supplement { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
    }
}