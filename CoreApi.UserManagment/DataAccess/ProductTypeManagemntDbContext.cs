using Microsoft.EntityFrameworkCore;

namespace CoreApi.UserManagment.DataAccess
{
    public class ProductTypeManagemntDbContext : DbContext
    {

        public ProductTypeManagemntDbContext(DbContextOptions<ProductTypeManagemntDbContext> options) : base(options)
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
