using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeSalesSystem.Entities
{
    public class Order
    {
        public int ShoeId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public int QuantityOrdered { get; set; }
    }  
}
