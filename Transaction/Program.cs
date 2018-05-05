using System;
using System.Collections.Generic;
using Transaction.model;
using System.Threading.Tasks;
using System.Threading;

namespace Transaction
{
    class Program
    {
        static void addOrderItem(OrderItem orderItem, int orderId)
        {
            using (OrderContext db = new OrderContext())
            {
                Order order = db.Orders.Find(orderId);
                if (order == null)
                {
                    order = new Order { };
                    db.Orders.Add(order);
                    db.SaveChanges();
                }
                Thread.Sleep(5000);
                order.Amount += orderItem.Amount;
                orderItem.Order = order;
                db.OrderItems.Add(orderItem);
                db.SaveChanges();
            }
            
        }
        static void Main(string[] args)
        {
            using (OrderContext db = new OrderContext())
            {
                OrderItem oi1 = new OrderItem { Title = "orange", Count = 11, Amount = 7 };
                OrderItem oi2 = new OrderItem { Title = "apple", Count = 12, Amount = 12 };

                //db.OrderItems.AddRange(new List<OrderItem> { oi1, oi2 });
                //db.SaveChanges();
                //Order order = db.Orders.Find(4);
                Task task1 = new Task(() => addOrderItem(oi1, 21));
                Task task2 = new Task(() => addOrderItem(oi2, 21));
                task1.Start();
                task2.Start();

                task1.Wait();
                task2.Wait();
                //addOrderItem(db, oi1, 18);
                //addOrderItem(db, oi2, 18);
                //db.Orders.Add(new Order ());

                Console.WriteLine("Объекты успешно сохранены");
                //db.OrderItems.RemoveRange(db.OrderItems);
                //db.Orders.RemoveRange(db.Orders);

                db.SaveChanges();
            }
            Console.Read();
        }
    }
}