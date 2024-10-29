using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentsApi.Data.Model
{
    public class Withdraw
    {
        public string WithdrawID { get; set; } 
        public decimal WithdrawAmount { get; set; }
        public string WithdrawStatus { get; set;}
        public DateTime WithdrawDate { get; }
        public string WithdrawFlag { get; set; }

        public User User { get; }
        public long UserIdCard { get; set; }

        public Withdraw()
        {
            //WithdrawID = Guid.NewGuid().ToString();
            //WithdrawDate = DateTime.UtcNow;
        }
    }
}