using System;
using System.Collections.Generic;

namespace FantasyStoreAPI.Entities;

public partial class Inventory
{
    public int Id { get; set; }

    public string ItemName { get; set; } = null!;

    public int StockQuantity { get; set; }

    public int RestockThreshold { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
