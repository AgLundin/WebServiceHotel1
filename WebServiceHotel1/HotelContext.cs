namespace WebServiceHotel1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HotelContext : DbContext
    {
        public HotelContext()
            : base("name=HotelContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<DemoBooking> DemoBooking { get; set; }
        public virtual DbSet<DemoFacilities> DemoFacilities { get; set; }
        public virtual DbSet<DemoGuest> DemoGuest { get; set; }
        public virtual DbSet<DemoHotel> DemoHotel { get; set; }
        public virtual DbSet<DemoRoom> DemoRoom { get; set; }
        public virtual DbSet<FacilitiesDescription> FacilitiesDescription { get; set; }
        public virtual DbSet<Table> Table { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoGuest>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DemoGuest>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<DemoGuest>()
                .HasMany(e => e.DemoBooking)
                .WithRequired(e => e.DemoGuest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DemoHotel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DemoHotel>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<DemoHotel>()
                .HasMany(e => e.DemoFacilities)
                .WithRequired(e => e.DemoHotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DemoHotel>()
                .HasMany(e => e.DemoRoom)
                .WithRequired(e => e.DemoHotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DemoRoom>()
                .Property(e => e.Types)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DemoRoom>()
                .HasMany(e => e.DemoBooking)
                .WithRequired(e => e.DemoRoom)
                .HasForeignKey(e => new { e.Room_No, e.Hotel_No })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacilitiesDescription>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
