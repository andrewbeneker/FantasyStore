﻿namespace FantasyStoreAPI.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
