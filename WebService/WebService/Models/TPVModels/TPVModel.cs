namespace WebService.Models.TPVModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TPVModel : DbContext
    {
        public TPVModel()
            : base("name=TPVConnection")
        {
        }

        public virtual DbSet<Drink> Drink { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Table> Table { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Drink>()
                .Property(e => e.price)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Drink>()
                .Property(e => e.commentary)
                .IsFixedLength();

            modelBuilder.Entity<Drink>()
                .Property(e => e.typeBottle)
                .IsFixedLength();

            modelBuilder.Entity<Drink>()
                .HasMany(e => e.Menu)
                .WithMany(e => e.Drink)
                .Map(m => m.ToTable("Menu_has_Drink").MapLeftKey("drink_id").MapRightKey("menu_id"));

            modelBuilder.Entity<Drink>()
                .HasMany(e => e.Order)
                .WithMany(e => e.Drink)
                .Map(m => m.ToTable("Order_has_Drink").MapLeftKey("drink_id").MapRightKey("order_id"));

            modelBuilder.Entity<Food>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Food>()
                .Property(e => e.price)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Food>()
                .Property(e => e.partOfMenu)
                .IsFixedLength();

            modelBuilder.Entity<Food>()
                .Property(e => e.familyDish)
                .IsFixedLength();

            modelBuilder.Entity<Food>()
                .Property(e => e.commentary)
                .IsFixedLength();

            modelBuilder.Entity<Food>()
                .HasMany(e => e.Menu)
                .WithMany(e => e.Food)
                .Map(m => m.ToTable("Menu_has_Food").MapLeftKey("food_id").MapRightKey("menu_id"));

            modelBuilder.Entity<Food>()
                .HasMany(e => e.Order)
                .WithMany(e => e.Food)
                .Map(m => m.ToTable("Order_has_Food").MapLeftKey("food_id").MapRightKey("order_id"));

            modelBuilder.Entity<Menu>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Menu>()
                .Property(e => e.price)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Menu>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Order)
                .WithMany(e => e.Menu)
                .Map(m => m.ToTable("Order_has_Menu").MapLeftKey("menu_id").MapRightKey("order_id"));

            modelBuilder.Entity<Order>()
                .Property(e => e.total)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Table>()
                .Property(e => e.location)
                .IsFixedLength();

            modelBuilder.Entity<Table>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Table)
                .HasForeignKey(e => e.table_id)
                .WillCascadeOnDelete(false);
        }
    }
}
