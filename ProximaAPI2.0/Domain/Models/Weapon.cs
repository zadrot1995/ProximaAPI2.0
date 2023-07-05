using Domain.Abstract;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Weapon : ImplementableGameObject
    {
        public int Damage { get; set; }
        public WeaponType WeaponType { get; set; }
        public IEnumerable<User>? Users { get; set; }
    }
}
