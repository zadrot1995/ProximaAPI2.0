﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CreateUserModel
    {
        public string Login { get; set; }
        public UserRole UserRole { get; set; }
        public string Password { get; set; }
    }
}
