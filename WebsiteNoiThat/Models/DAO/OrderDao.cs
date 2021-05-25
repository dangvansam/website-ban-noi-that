using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
  public  class OrderDao
    {
       DBNoiThat db = null;
        public OrderDao()
        {
            db = new DBNoiThat();
        }
        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.OrderId;
        }
    }
}
