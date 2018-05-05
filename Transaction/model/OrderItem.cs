using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.model
{
    public class OrderItem
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

}
}
