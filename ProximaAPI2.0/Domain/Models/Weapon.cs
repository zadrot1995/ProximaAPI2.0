using Domain.Abstract;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Weapon : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }
        public WeaponType WeaponType { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
