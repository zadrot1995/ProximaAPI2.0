using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public static class FilterUserHelper
    {
        public static bool IsNullOrEmpty(SearchUserModel model)
        {
            if(model == null || 
                (string.IsNullOrEmpty(model.Login) && string.IsNullOrEmpty(model.UsersRole) && string.IsNullOrEmpty(model.UserSortType)))
            {
                return true;
            }
            else { return false; }
        }
    }
}
