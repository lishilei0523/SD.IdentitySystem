using System.Collections.Generic;

namespace SD.IdentitySystem.AspNetCore.Tests.Models
{
    public class Order
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
    }
}
