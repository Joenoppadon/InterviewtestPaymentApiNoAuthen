using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PaymentsApi.Data;
using PaymentsApi.Data.Model;
using PaymentsApi.Data.Repository;
using Swashbuckle.AspNetCore.Annotations;

namespace PaymentsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WithdrawController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        public WithdrawController(DataContext DataContext, IUserRepositoryAsync userRepositoryAsync)
        {
            _dataContext = DataContext;
            _userRepositoryAsync = userRepositoryAsync;
        }
        
        [SwaggerOperation("User withdraw")]
        [HttpPost("userwithdraw")]
        public async Task<IActionResult> userwithdraw(long idcard, decimal amount,string flag = "Withdraw")
        {
            var user = await _userRepositoryAsync.GetUser(idcard);
            if (user != null)
            {
                if (amount > user.Balance || amount < 0.0m) 
                    return BadRequest(new{status = "Withdraw amount went wrong"});
                
                var withdraw = new Withdraw(){
                    WithdrawID = Guid.NewGuid().ToString(),
                    WithdrawAmount = amount,
                    WithdrawStatus = "Success",
                    WithdrawFlag = flag,
                    UserIdCard = user.IdCard
                };

                _dataContext.Withdraw.Add(withdraw);
                user.Balance = user.Balance - amount;
                _dataContext.User.Update(user);
                await _dataContext.SaveChangesAsync();
                return Ok(new Withdrawresult(){
                    status = "Success",
                    resultid = withdraw.WithdrawID.ToString()
                });

            }
            return BadRequest(new { status = "User not found" });
        }

        [SwaggerOperation("User withdraw report")]
        [HttpGet("withdrawreport")]
        public async Task<IActionResult> withdrawreport(long idcard)
        {
            
            var user = await _userRepositoryAsync.GetUser(idcard);
            if (user == null) return BadRequest(new { status = "User not found" });
            var withdrawreports = from re in _dataContext.Withdraw
                                 where re.UserIdCard.Equals(user.IdCard) //&& re.DepositFlag.Equals("Deposit") //show deposit flag only
                                 select new
                                 {
                                     re.WithdrawID,
                                     re.WithdrawAmount,
                                     re.WithdrawDate,
                                     re.WithdrawStatus,
                                     re.WithdrawFlag
                                 };
            return Ok(await withdrawreports.ToListAsync());
        }

        [SwaggerOperation("User withdraw detail")]
        [HttpGet("withdrawdetail")]
        public async Task<IActionResult> withdrawdetail(string WithdrawID)
        {
            var detail = from de in _dataContext.Withdraw
                         join ur in _dataContext.User on de.UserIdCard equals ur.IdCard
                         where de.WithdrawID.Equals(WithdrawID)
                         select new
                         {
                             de.WithdrawID,
                             de.WithdrawAmount,
                             de.WithdrawDate,
                             de.WithdrawStatus,
                             de.WithdrawFlag,
                             ur.FullName
                         };
            return Ok(await detail.FirstOrDefaultAsync());
        }

    }
}