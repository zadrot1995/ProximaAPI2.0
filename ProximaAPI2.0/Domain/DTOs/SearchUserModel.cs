using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class SearchUserModel
    {
        public string Login { get; set; }
        public string UsersRole { get; set; }
        public string UserSortType { get; set; }
    }
}
