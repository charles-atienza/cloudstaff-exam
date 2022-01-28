using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStaffProject.Repositories.User
{
    public interface IUserRepository
    {
        JsonResult GetUsers();
        JsonResult CreateUser(string pin, string password);
        JsonResult GetBalance(Guid userId);
        JsonResult CreditBalance(Guid userId, int balanceToDebit);
        JsonResult DebitBalance(Guid userId, int balanceToDebit);
        JsonResult Login(string pin, string password);
        JsonResult Logout();
    }
}