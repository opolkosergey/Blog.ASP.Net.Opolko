using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.CompilerServices;

namespace DalToWeb.Repositories
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("DefaultConnection")
        {
            
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }

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

    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

    public class Profile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}