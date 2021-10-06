using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }

        /* EF Relation */
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}