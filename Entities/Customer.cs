using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeSalesSystem.Entities
{
    public class Customer
    {
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        // 其他可能需要的属性
        // ...

        public Customer(string name, string contactInfo)
        {
            Name = name;
            ContactInfo = contactInfo;
        }
        
    }
}
