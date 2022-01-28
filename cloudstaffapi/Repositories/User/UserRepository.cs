using CloudStaffProject.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStaffProject.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public JsonResult CreateUser(string pin, string password)
        {
            try
            {
                var existing = _context.Users.Where(x => x.Pin.ToLower() == pin.ToLower()).FirstOrDefault();

                

                if (existing != null)
                {
                    return new JsonResult(new { successful = false });
                }

                var newUser = new Models.User.User
                {
                    UserId = Guid.NewGuid(),
                    Pin = pin,
                    Password = password,
                    Balance = 100000,
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                var isSuccessful = (_context.Users.Where(x => newUser.UserId == x.UserId).FirstOrDefault() != null);
                return new JsonResult(new { newUser, successful = isSuccessful });
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }
        }

        public JsonResult CreditBalance(Guid userId, int balanceToDebit)
        {
            var repo = _context.Users.AsQueryable()
               .Where(u => u.UserId == userId).FirstOrDefault();

            repo.Balance += balanceToDebit;

            _context.Users.Update(repo);
            _context.SaveChanges();

            return new JsonResult(repo);
        }

        public JsonResult DebitBalance(Guid userId, int balanceToDebit)
        {
            var repo = _context.Users.AsQueryable()
               .Where(u => u.UserId == userId).FirstOrDefault();

            repo.Balance -= balanceToDebit;

            _context.Users.Update(repo);
            _context.SaveChanges();

            return new JsonResult(repo);
        }

        public JsonResult GetBalance(Guid userId)
        {
            var repo = _context.Users.AsQueryable()
                .Where(u => u.UserId == userId).FirstOrDefault();

            return  new JsonResult(repo.Balance);
        }

        public JsonResult GetUsers()
        {
            try
            {
                var repo = _context.Users.AsQueryable().ToList();

                return new JsonResult(repo);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }
        }

        public JsonResult Login(string pin, string password)
        {
            try
            {
                var repo = _context.Users.AsQueryable()
                    .Where(x => x.Pin.ToLower() == pin.ToLower() && x.Password == password).FirstOrDefault();

                return new JsonResult(new { account = repo, successful = (repo != null) });
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }
        }

        public JsonResult Logout()
        {
            throw new NotImplementedException();
        }
    }
}
