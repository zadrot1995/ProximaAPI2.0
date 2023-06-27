using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using System;

namespace API.Controllers
{
    public class UserController : Controller
    {
        private ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _context.Users.ToListAsync();
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel userModel)
        {
            if (userModel != null)
            {
                var newUser = new User
                {
                    Login = userModel.Login,
                    Password = userModel.Password,
                    Created = DateTime.UtcNow,
                    LastUpdate = DateTime.UtcNow,
                    UserRole = userModel.UserRole
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return BadRequest(userModel);
        }

        [HttpPost()]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return NotFound(id);
            }
            return BadRequest(id);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    var UserDTO = new UpdateUserModel
                    {
                        Id = user.Id,
                        Login = user.Login,
                        Password = user.Password,
                        UserRole = user.UserRole
                    };
                    return View(UserDTO);
                }
                return NotFound(id);
            }
            return BadRequest(id);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserModel user)
        {
            if (user != null)
            {
                var userToUpdate = _context.Users.Find(user.Id);
                if (userToUpdate != null)
                {
                    userToUpdate.Login = user.Login;
                    userToUpdate.Password = user.Password;
                    userToUpdate.UserRole = user.UserRole;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return NotFound(user.Id);
            }
            return BadRequest(user);
        }
    }
}
