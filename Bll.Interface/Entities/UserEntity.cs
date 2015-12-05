using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interface
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
    }
}
