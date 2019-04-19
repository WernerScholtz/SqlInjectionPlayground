using System.Collections.Generic;
using System.ComponentModel;
using SqlInjectionDemo.Application;

namespace SqlInjectionDemo.Web.Public.Models
{
    public class UserSearchInputModel
    {
        [DisplayName("Search by Name")]
        public string SearchName { get; set; }
        [DisplayName("Do a safe search?")]
        public bool IsSafe { get; set; }

        public List<User> Users { get; set; }

        public UserSearchInputModel()
        {
        }

        public UserSearchInputModel(List<User> users)
        {
            SetUsers(users);
        }

        public void SetUsers(List<User> users)
        {
            Users = users;
        }
    }
}
