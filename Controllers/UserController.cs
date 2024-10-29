using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
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
    public class UserController : ControllerBase
    {
        private readonly DataContext _datacontext;
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        private readonly IValidator<CreateUser> _uservalidator;
        public UserController(DataContext dataContext, IValidator<CreateUser> uservalidator
        , IUserRepositoryAsync userRepositoryAsync)
        {
            _datacontext = dataContext;
            _uservalidator = uservalidator;
            _userRepositoryAsync = userRepositoryAsync;
        }

        [SwaggerOperation(Summary = "Create user")]
        [HttpPost(Name = "createuser")]

        public async Task<IActionResult> createuser([FromBody] CreateUser cuser)
        {

            //example use fluentvalidation
            var validation = await _uservalidator.ValidateAsync(cuser);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var fulluser = new User()
            {
                IdCard = cuser.IdCard,
                FullName = cuser.FullName,
                Gender = cuser.Gender
            };

            _datacontext.User.Add(fulluser);
            await _datacontext.SaveChangesAsync();
            
            return Ok(new {status = "User created"});
        }

        [SwaggerOperation(Summary = "Get all user")]
        [HttpGet(Name = "alluser")]
        public async Task<IActionResult> getalluser()
        {
            var alluser = from m in _datacontext.User
                          where m.Status.Equals("Active")
                          select new { m.IdCard, m.FullName, m.Gender, m.Balance, m.Status, m.CreateDate };

            return Ok(await alluser.ToListAsync());
        }

        [SwaggerOperation(Summary = "Delete user")]
        [HttpDelete(Name = "deleteuser")]
        public async Task<IActionResult> deleteuser([FromForm] long idcard)
        {
            var user = await _userRepositoryAsync.GetUser(idcard);
            if (user != null)
            {
                if (user.Balance == 0.0m)
                {
                    user.Status = "Deactive";
                    _datacontext.User.Update(user);
                    await _datacontext.SaveChangesAsync();
                    return Ok(new { status = "User deleted" });
                }
                return BadRequest(new { status = "Balance must be equal 0" });
            }
            return BadRequest(new { status = "User not found!" });
        }
    }

    public class CreateUser()
    {
        public long IdCard { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
    }
}