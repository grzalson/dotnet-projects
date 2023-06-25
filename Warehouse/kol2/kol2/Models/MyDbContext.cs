using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection.Emit;

namespace WarehousesAPI.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Object> Objects { get; set; }
        public DbSet<ObjectType> ObjectTypes { get; set; }
        public DbSet<ObjectOwner> ObjectOwners { get; set; }
        public DbSet<Owner> Owners { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Warehouse>(e =>
            {
                e.HasKey(e => e.IdWarehouse);
                e.Property(e => e.Name).HasMaxLength(50).IsRequired();
                


            });

            modelBuilder.Entity<Object>(e =>
            {
                e.HasKey(e => e.IdObject);
                e.HasOne(e => e.Warehouse).WithMany(e => e.Objects).HasForeignKey(e => e.IdWarehouse).OnDelete(DeleteBehavior.ClientCascade);
                e.HasOne(e => e.ObjectType).WithMany(e => e.Objects).HasForeignKey(e => e.IdObjectType).OnDelete(DeleteBehavior.ClientCascade);
                e.Property(e => e.Height).IsRequired();
                e.Property(e => e.Width).IsRequired();
            });

            modelBuilder.Entity<ObjectType>(e =>
            {
                e.HasKey(e => e.IdObjectType);
                e.Property(e => e.Name).HasMaxLength(50).IsRequired();
                
            });
            modelBuilder.Entity<ObjectOwner>(e =>
            {
                e.HasKey(e => new { e.IdObject, e.IdOwner });
                e.HasOne(e => e.Object).WithMany(e => e.ObjectOwners).HasForeignKey(e => e.IdObject).OnDelete(DeleteBehavior.ClientCascade);    
                e.HasOne(e => e.Owner).WithMany(e => e.ObjectOwners).HasForeignKey(e => e.IdOwner).OnDelete(DeleteBehavior.ClientCascade);  

            });

            modelBuilder.Entity<Owner>(e =>
            {
                e.HasKey(e => e.IdOwner);
                e.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                e.Property(e => e.PhoneNumber).HasMaxLength(9).IsRequired();
            });

            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { IdWarehouse = 1, Name = "Magazyn A" },
                new Warehouse { IdWarehouse = 2, Name = "Magazyn B" }
            );
        
            
            modelBuilder.Entity<ObjectType>().HasData(
                new ObjectType { IdObjectType = 1, Name = "Obiekt A" },
                new ObjectType { IdObjectType = 2, Name = "Obiekt B" }
            );

            
            modelBuilder.Entity<Owner>().HasData(
                new Owner { IdOwner = 1, FirstName = "Jan", LastName = "Kowalski", PhoneNumber = "123456789" },
                new Owner { IdOwner = 2, FirstName = "Anna", LastName = "Nowak", PhoneNumber = "987654321" }
            );

            
            modelBuilder.Entity<Object>().HasData(
                new Object { IdObject = 1, IdWarehouse = 1, IdObjectType = 1, Height = 10, Width = 20 },
                new Object { IdObject = 2, IdWarehouse = 1, IdObjectType = 2, Height = 15, Width = 25 }
            );

            
            modelBuilder.Entity<ObjectOwner>().HasData(
                new ObjectOwner { IdObject = 1, IdOwner = 1 },
                new ObjectOwner { IdObject = 2, IdOwner = 2 }
            );


        }

    }
}
