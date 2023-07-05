using Domain.DTOs;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;

namespace API.Controllers
{
    public class WeaponController : Controller
    {
        private ApplicationContext _context;

        public WeaponController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _context.Weapons.ToListAsync();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWeaponModel model)
        {
            if (model != null)
            {
                var newWeapon = new Weapon
                { 
                    Type = GameObjectType.Weapon,
                    Name = model.Name,
                    Description = model.Description,
                    Damage = model.Damage,
                    WeaponType = model.WeaponType,
                    Created = DateTime.UtcNow,
                    LastUpdate = DateTime.UtcNow,
                    ImplementationStage = WeaponImplementationStage.WaitForImplementation,
                    SourceLink = model.SourceLink
                };
                await _context.Weapons.AddAsync(newWeapon);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return BadRequest(model);
        }

        [HttpPost()]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                var weaponToDelete = await _context.Weapons.FindAsync(id);
                if (weaponToDelete != null)
                {
                    _context.Weapons.Remove(weaponToDelete);
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
                var weapon = _context.Weapons.Find(id);
                if (weapon != null)
                {
                    var weaponDTO = new UpdateWeaponModel
                    {
                        Id = weapon.Id,
                        Name = weapon.Name,
                        WeaponType= weapon.WeaponType,
                        Damage= weapon.Damage,
                        Description = weapon.Description,
                        SourceLink = weapon.SourceLink
                    };
                    return View(weaponDTO);
                }
                return NotFound(id);
            }
            return BadRequest(id);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateWeaponModel weapon)
        {
            if (weapon != null)
            {
                var weaponToUpdate = _context.Weapons.Find(weapon.Id);
                if (weaponToUpdate != null)
                {
                    weaponToUpdate.Name = weapon.Name;
                    weaponToUpdate.WeaponType = weapon.WeaponType;
                    weaponToUpdate.Damage = weapon.Damage;
                    weaponToUpdate.Description = weapon.Description;
                    weaponToUpdate.LastUpdate = DateTime.UtcNow;
                    weaponToUpdate.SourceLink = weapon.SourceLink;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return NotFound(weapon.Id);
            }
            return BadRequest(weapon);
        }
    }
}
