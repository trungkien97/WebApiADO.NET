using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MISA.DEMO.ADO.Models
{
    public class Customer
    {
        private Guid _customerID;
        public Guid CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }
        public string CustomerCode { get; set; }
        private int _sex;
        public string SexName;
        public int Sex
        {
            get
            {
                return _sex;
            }
            set
            {
                _sex = value;
                switch (value)
                {
                    case 0:
                        SexName = "Nữ";
                        break;
                    case 1:
                        SexName = "Nam";
                        break;
                    default:
                        SexName = "Không xác định";
                        break;
                }
            }
        }

        
        public string Address { get; set; }
        public string Mobile { get; set; }
        public DateTime? Birthday { get; set; }
        public string CompanyTax { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool StopFollow { get; set; }
        public object FullName { get; set; }
        public object CreatedBy { get; set; }
        public int GroupCustomerID { get; set; }
        public string GroupCustomerName { get; set; }
    }
}