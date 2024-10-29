using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentsApi.Data.Model
{
    public class Deposit
    {
        public string DepositID { get; set;} 
        public decimal DepositAmount { get; set; }
        public string DepositStatus { get; set;}
        public DateTime DepositDate { get;}
        public string DepositFlag { get; set; }

        public User User { get;}
        public long UserIdCard { get; set; }

        public Deposit()
        {

            //DepositID = Guid.NewGuid().ToString();
            //DepositDate = DateTime.UtcNow;
        }
    }
}