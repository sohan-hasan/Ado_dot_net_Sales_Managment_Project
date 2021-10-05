using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Entities
{
  public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNo { get; set; }
        public string ShippingAddress { get; set; }
        public int ProductID { get; set; }
        public int StoreId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string ImageName { get; set; }
        public int SupplierID { get; set; }
        public byte[] ImageData { get; set; }
    }
}
