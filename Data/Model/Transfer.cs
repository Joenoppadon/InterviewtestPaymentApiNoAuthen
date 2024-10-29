using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentsApi.Data.Model
{
    public class Transfer
    {
        public string TransferID { get; }
        public string TransferFlag { get; set; } //flag status (deposit/withdraw)
        public string TransferFlagID { get; set; } //fk deposit or withdraw
        public decimal TransferAmount { get; set; }
        public DateTime TransferDate { get; }

        public long UserIdCard { get; set; }  
        public User User { get;}

        public Transfer()
        {
            TransferID = Guid.NewGuid().ToString();

        }
    }
}