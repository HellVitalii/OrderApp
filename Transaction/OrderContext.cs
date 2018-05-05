using System;
using System.Collections.Generic;
using System.Data.Entity;
using Transaction.model;

namespace Transaction
{
    class OrderContext : DbContext
    {
        public OrderContext()
            : base("DbConnection")
        { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<Photo> Photos { get; set; }
    }
}