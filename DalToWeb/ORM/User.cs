using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.ORM
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public DateTime CreationDate { get; set; }

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
