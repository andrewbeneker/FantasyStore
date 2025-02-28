namespace FantasyStoreAPI.DTOs
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int StockQuantity { get; set; }
        public int RestockThreshold { get; set; }

    }
}
