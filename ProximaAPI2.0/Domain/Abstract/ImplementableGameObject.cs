using Domain.Abstract;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public abstract class ImplementableGameObject : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? SourceLink { get; set; }
        public WeaponImplementationStage ImplementationStage { get; set; }
        public GameObjectType Type { get; set; }
    }
}
