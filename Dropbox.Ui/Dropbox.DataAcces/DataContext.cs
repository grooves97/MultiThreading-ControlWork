using System;
using System.Data.Entity;
using System.Linq;
using Dropbox.Models;

namespace Dropbox.DataAcces
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}