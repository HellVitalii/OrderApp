using System;
using System.Collections.Generic;
using Transaction.model;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Data;

namespace Transaction
{
    class Program
    {
        static void addOrderItem(OrderItem orderItem, int orderId)
        {
            using (OrderContext db = new OrderContext())
            {
                using (var transaction = db.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                {
                    try
                    {
                        
                        Order order = db.Orders.Find(orderId);
                        if (order == null)
                        {
                            return;
                        }
                        orderItem.Order = order;
                        db.OrderItems.Add(orderItem);
                        Thread.Sleep(5000);
                        Console.WriteLine("OrderAmountOld: " + order.Amount);
                        order.Amount += orderItem.Amount;
                        Console.WriteLine("OrderAmountNew: " + order.Amount);
                        db.SaveChanges();
                        transaction.Commit();                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception {0}", ex);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            using (OrderContext db = new OrderContext())
            {
                Order order = new Order();
                db.Orders.Add(order);
                db.SaveChanges();
                Console.WriteLine(order.Id);

                OrderItem oi1 = new OrderItem { Title = "orange", Count = 11, Amount = 7 };
                OrderItem oi2 = new OrderItem { Title = "apple", Count = 12, Amount = 12 };

                Task task1 = new Task(() => addOrderItem(oi1, order.Id));
                Task task2 = new Task(() => addOrderItem(oi2, order.Id));
                task1.Start();
                task2.Start();

                task1.Wait();
                task2.Wait();

                db.SaveChanges();
            }
            Console.Read();
        }
    }
}