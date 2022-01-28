using CloudStaffProject.Models.User;
using CloudStaffProject.Repositories.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStaffProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _service;

        public UserController(IUserRepository _controllerRepository)
        {
            _service = _controllerRepository;
        }
        
        [HttpGet]
        public JsonResult GetUsers()
        {
            return _service.GetUsers();
        }
            [HttpPost]
        public JsonResult CreateUser([FromBody] CreateUserParams userInfo)
        {
            return _service.CreateUser(userInfo.Pin, userInfo.Password);
        }

        [HttpPost]
        public JsonResult CreditBalance([FromBody] BalanceModification modInfo)
        {
            return _service.CreditBalance(modInfo.UserId, modInfo.Amount);
        }
        [HttpPost]
        public JsonResult DebitBalance([FromBody] BalanceModification modInfo)
        {
            return _service.DebitBalance(modInfo.UserId, modInfo.Amount);
        }

        [HttpGet]
        public JsonResult GetBalance([FromHeader] Guid userId)
        {
            return _service.GetBalance(userId);
        }

        [HttpPost]
        public JsonResult Login([FromBody] LoginParams userInfo)
        {
            return _service.Login(userInfo.Pin, userInfo.Password);
        }

        [HttpPost]
        public JsonResult Logout()
        {
            return _service.Logout();
        }
    }
}


public struct CreateUserParams
{
    public string Pin { get; set; }
    public string Password { get; set; }
}

public struct LoginParams
{
    public string Pin { get; set; }
    public string Password { get; set; }
}

public struct BalanceModification
{
    public Guid UserId { get; set; }
    public int Amount { get; set; }
}