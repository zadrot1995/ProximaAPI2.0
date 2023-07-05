using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CreateWeaponModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }
        public WeaponType WeaponType { get; set; }
        public string? SourceLink { get; set; }
    }
}
