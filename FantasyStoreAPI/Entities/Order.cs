using System;
using System.Collections.Generic;

namespace FantasyStoreAPI.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int? ItemId { get; set; }

    public int Quantity { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual Inventory? Item { get; set; }
}
