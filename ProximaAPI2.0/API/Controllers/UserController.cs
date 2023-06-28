using Domain;
using Domain.DTOs;
using Domain.Enums;
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

        public async Task<IActionResult> Index(SearchUserModel model)
        {
            IQueryable<User> users = _context.Users.AsQueryable();
            ViewBag.Filter = model;

            if (model != null)
            {
                if (model.Login != null && !string.IsNullOrEmpty(model.Login))
                {
                    users = _context.Users.Where(user => user.Login.Contains(model.Login));
                }
                if (model.UsersRole != null && !string.IsNullOrEmpty(model.UsersRole))
                {
                    UserRole role;
                    Enum.TryParse<UserRole>(model.UsersRole, out role);
                    users = users.Where(user => user.UserRole == role);
                }

                switch (model.UserSortType)
                {
                    case "By login":
                        {
                            users = users.OrderBy(user => user.Login);
                            break;
                        }
                    case "By creating date (Aescending)":
                        {
                            users = users.OrderBy(user => user.Created);
                            break;
                        }
                    case "By creating date (Descending)":
                        {
                            users = users.OrderByDescending(user => user.Created);
                            break;
                        }
                }
                return View(await users.ToListAsync());
            }
            else
            {
                var result = await _context.Users.ToListAsync();
                return View(result);
            }
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
                    userToUpdate.LastUpdate = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return NotFound(user.Id);
            }
            return BadRequest(user);
        }
    }
}
