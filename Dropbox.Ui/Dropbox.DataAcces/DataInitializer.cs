using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Models;
using Dropbox.Services;
using DevOne.Security.Cryptography.BCrypt;

namespace Dropbox.DataAcces
{
    public class DataInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override async void Seed(DataContext context)
        {
            context.Users.Add(new User
            {
                Login = "admin",
                Password = await EncryptionService.HashPassword("admin")
            });
            base.Seed(context);
        }
    }
}
