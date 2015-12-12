using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;

namespace Bll.Interface
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime DateAdded { get; set; }
        public int RoleId { get; set; }

    }
}
