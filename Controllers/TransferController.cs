using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using PaymentsApi.Data;
using PaymentsApi.Data.Model;
using PaymentsApi.Data.Repository;
using Swashbuckle.AspNetCore.Annotations;

namespace PaymentsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        public TransferController(DataContext DataContext, IUserRepositoryAsync userRepositoryAsync)
        {
            _dataContext = DataContext;
            _userRepositoryAsync = userRepositoryAsync;
        }

        [SwaggerOperation("User transer")]
        [HttpPost("usertransfer")]
        
        public async Task<IActionResult> usertransfer(long transferfrom, long transferto, decimal amount) //use flag ,Flag flag
        {
            var deposit = new DepositController(_dataContext, _userRepositoryAsync);
            var withdraw = new WithdrawController(_dataContext, _userRepositoryAsync);

            var user = await _userRepositoryAsync.GetUser(transferfrom);
            //validation data
            if (user == null) return BadRequest(new { status = "From transfer user not found" });
            if (await _userRepositoryAsync.GetUser(transferto) == null)
                return BadRequest(new { status = "To transfer user not found" });
            if (amount > user.Balance || amount <= 0) return BadRequest(new { status = "Input amount went wrong" });

            #region validation
            var withdrawresult = await withdraw.userwithdraw(transferfrom, amount, "Transfer"); //transfer by
            var depositresult = await deposit.userdeposit(transferto, amount, "Transfer"); //transfer to

            var withdrawvalue = (Withdrawresult)((OkObjectResult)withdrawresult).Value; //support async IActionResult only
            var depositvalue = (Depositresult)((OkObjectResult)depositresult).Value;
            //log for transfer by
            var tarnsferby = new Transfer()
            {
                TransferFlag = "Withdraw", //query from withdraw table
                TransferFlagID = withdrawvalue.resultid.ToString(), //fk withdraw
                TransferAmount = amount,
                UserIdCard = transferto //tranfer to 
            };

            //log for transfer to
            var tarnsferto = new Transfer()
            {
                TransferFlag = "Deposit", //query from withdraw table
                TransferFlagID = depositvalue.resultid.ToString(), //fk withdraw
                TransferAmount = amount,
                UserIdCard = transferfrom //tranfer from
            };
            #endregion
            _dataContext.Transfer.AddRange(tarnsferby, tarnsferto);
            await _dataContext.SaveChangesAsync();
            return Ok(new {status = "Transfer Success"});
        }

        [SwaggerOperation("User transfer report")]
        [HttpGet("usertransferreport")]
        public async Task<IActionResult> transferreport(long IdCard)
        {

            var user = await _userRepositoryAsync.GetUser(IdCard);
            if (user == null) return BadRequest(new { status = "User not found" });
            var transreport = from wth in _dataContext.Withdraw
                                join trn in _dataContext.Transfer on wth.WithdrawID equals trn.TransferFlagID
                                join usr in _dataContext.User on trn.UserIdCard equals usr.IdCard
                                where wth.UserIdCard.Equals(IdCard) && wth.WithdrawFlag.Equals("Transfer")
                                select new {
                                    trn.TransferID,
                                    transfertoname = usr.FullName,
                                    transtouserid = trn.UserIdCard,
                                    trn.TransferAmount,
                                    trn.TransferDate
                                };
            return Ok(await transreport.OrderBy(m => m.TransferDate).ToListAsync());

        }

        

    }
    
}