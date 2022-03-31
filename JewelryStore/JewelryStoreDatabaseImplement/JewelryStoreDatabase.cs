using JewelryStoreDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;


namespace JewelryStoreDatabaseImplement
{
    public class JewelryStoreDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-1UCRGBF3\SQLEXPRESS;Initial Catalog=JewelryStoreDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Component> Components { set; get; }

        public virtual DbSet<Jewel> Jewels { set; get; }

        public virtual DbSet<JewelComponent> JewelComponents { set; get; }

        public virtual DbSet<Order> Orders { set; get; }

        public virtual DbSet<Client> Clients { set; get; }
    }
}
