using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PaymentsApi.Controllers;

namespace PaymentsApi.Data.Model.Validation
{
    public class UserValidation : AbstractValidator<CreateUser>
    {
        private DataContext _dataContext;
        //private readonly IUserRepositoryAsync _userRepositoryAsync;
        public UserValidation(DataContext DataContext)
        {
            _dataContext = DataContext;
            RuleFor(m => m.IdCard).NotEmpty().MustAsync(
                async (idcard,ct) => (
                    await _dataContext.User.AllAsync(m => m.IdCard != idcard)
            )
            ).WithMessage("Error! exist user in database"); //check exist user
            RuleFor(m => m.IdCard).Must(m => m.ToString().Length <= 13).WithMessage("Id card number less than 13 digit");
            RuleFor(m => m.FullName).NotNull().NotEmpty().WithMessage("Full name not null or empty");
            RuleFor(m => m.Gender).NotNull().NotEmpty().WithMessage("Gender not null or empty");
            RuleFor(m => m.Gender).Must(m => m.ToString().Contains("Male") || m.ToString().Contains("Female"))
                .WithMessage("Gender must be Male or Female only");
        }

    }
}