namespace Business
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;

    public class TPVModel : DbContext
    {
        public TPVModel()
            : base("name=TPVParaTodosEntities")
        {
        }

        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new DrinkMap());
            modelBuilder.Configurations.Add(new FoodMap());
            modelBuilder.Configurations.Add(new MenuMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(a => a.Id);
            ToTable("Products");
        }
    }

    public class DrinkMap : EntityTypeConfiguration<Drink>
    {
        public DrinkMap()
        {
            ToTable("Drinks");
        }
    }

    public class FoodMap : EntityTypeConfiguration<Food>
    {
        public FoodMap()
        {
            ToTable("Foods");
        }
    }

    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            ToTable("Menus");
        }
    }
}