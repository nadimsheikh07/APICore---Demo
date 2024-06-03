using Microsoft.EntityFrameworkCore;

namespace CoreApi.UserManagment.DataAccess
{
    public class UserManagemntDbContext : DbContext
    {

        public UserManagemntDbContext(DbContextOptions<UserManagemntDbContext> options) : base(options)
        {

        }


        //Data set for all DB tables

       
        //Using On Model Creating for linking of All data access / entity Configurations class
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);
        }

    }
}
