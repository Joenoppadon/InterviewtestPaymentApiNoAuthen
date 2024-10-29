using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentsApi.Data;
using PaymentsApi.Data.Model;
using PaymentsApi.Data.Repository;
using Swashbuckle.AspNetCore.Annotations;

namespace PaymentsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        public DepositController(DataContext DataContext, IUserRepositoryAsync userRepositoryAsync)
        {
            _dataContext = DataContext;
            _userRepositoryAsync = userRepositoryAsync;
        }

        [SwaggerOperation("User deposit")]
        [HttpPost("userdeposit")]
        public async Task<IActionResult> userdeposit(long idcard, decimal amount, string flag = "Deposit")
        {
            var user = await _userRepositoryAsync.GetUser(idcard);
            if (user != null)
            {
                if (flag == "Deposit") //only validate on deposit type
                {
                    if (amount < decimal.Zero) return BadRequest(new { status = "Input amount went wrong" });

                    if (user.Gender == "Male")
                    {
                        if (amount < 100.0m) return BadRequest(new { status = "Gender Male deposit more than 100" });

                    }
                    else
                    {
                        if (amount < 200.0m) return BadRequest(new { status = "Gender Female deposit more than 200" });
                    }
                }
                
                var deposit = new Deposit()
                {
                    DepositID = Guid.NewGuid().ToString(),
                    DepositAmount = amount,
                    DepositStatus = "Succcess",
                    DepositFlag = flag,
                    UserIdCard = user.IdCard,
                };
                
                //user.Deposit.Add(deposit); //can add deposit from user model
                _dataContext.Deposit.Add(deposit);
                user.Balance = user.Balance + amount;
                _dataContext.User.Update(user);
                await _dataContext.SaveChangesAsync();
                return Ok(new Depositresult()
                {
                    status = "Deposit success",
                    resultid = deposit.DepositID.ToString(),
                });
            }
            return BadRequest(new { status = "User not found" });
        }

        [SwaggerOperation("User deposit report")]
        [HttpGet("depositreport")]
        public async Task<IActionResult> depositreport(long idcard)
        {
            //var depositreports = await _dataContext.User.Include(x => x.Deposit).ToListAsync(); //support eager loading

            var user = await _userRepositoryAsync.GetUser(idcard);
            if (user == null) return BadRequest(new { status = "User not found" });
            var depositreports = from re in _dataContext.Deposit
                                 where re.UserIdCard.Equals(user.IdCard) //&& re.DepositFlag.Equals("Deposit") //show deposit flag only
                                 select new
                                 {
                                     re.DepositID,
                                     re.DepositAmount,
                                     re.DepositDate,
                                     re.DepositStatus,
                                     re.DepositFlag
                                 };
            return Ok(await depositreports.OrderByDescending(m => m.DepositDate).ToListAsync());
        }


        [SwaggerOperation("User deposit detail")]
        [HttpGet("depositdetail")]
        public async Task<IActionResult> depositdetail(string DepositID)
        {
            var detail = from de in _dataContext.Deposit
                         join ur in _dataContext.User on de.UserIdCard equals ur.IdCard
                         where de.DepositID.Equals(DepositID)
                         select new
                         {
                             de.DepositID,
                             de.DepositAmount,
                             de.DepositDate,
                             de.DepositStatus,
                             de.DepositFlag,
                             ur.FullName
                         };
            return Ok(await detail.FirstOrDefaultAsync());
        }


    }




}