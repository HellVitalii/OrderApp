using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.model
{
    public class Order
    {
        public int Id { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public decimal Amount { get; set; } // Сумма по всем Items – Items.Sum(x => x. Amount)

        public Order()
        {
            Items = new List<OrderItem>();
        }
    }
}
