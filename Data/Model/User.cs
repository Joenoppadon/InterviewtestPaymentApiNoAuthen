using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentsApi.Data.Model
{
    public class User
    {
        public long IdCard { get; set; }
        public string FullName { get; set; } 
        public string Gender { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set;}
        public DateTime CreateDate { get;}

        //virtual for support eager loading in fueture
        public virtual ICollection<Deposit> Deposit { get; set;}
        public virtual ICollection<Withdraw> Withdraw { get; set;}
        public virtual ICollection<Transfer> Transfer { get; set;}
        public User()
        {  
            Deposit = new List<Deposit>();
            Withdraw = new List<Withdraw>();
            Transfer = new List<Transfer>();
            Balance = 0.0m;
            Status = "Active";
            
        }
       
    }
}