using Microsoft.EntityFrameworkCore;

namespace MechanicKol.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<MechanicCar> MechanicCars { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Specialization> Specializations { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mechanic>(e =>
            {
                e.HasKey(e => e.IdMechanic);
                e.Property(e => e.FirstName).HasMaxLength(255).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(255).IsRequired();
                e.Property(e => e.Nickname).HasMaxLength(255).IsRequired(false);
                e.HasOne(e => e.Specialization).WithMany(e => e.Mechanics).HasForeignKey(e => e.IdSpecialization).OnDelete(DeleteBehavior.ClientCascade);
      

            });

            modelBuilder.Entity<Specialization>(e =>
            {
                e.HasKey(e => e.IdSpecialization);
                e.Property(e => e.Name).HasMaxLength(255).IsRequired();
                
            });

            modelBuilder.Entity<MechanicCar>(e =>
            {
                e.HasKey(e => new {e.IdMechanic, e.IdCar});
                e.HasOne(e => e.Mechanic).WithMany(e => e.MechanicCars).HasForeignKey(e => e.IdMechanic).OnDelete(DeleteBehavior.ClientCascade);
                e.HasOne(e => e.Car).WithMany(e => e.MechanicCars).HasForeignKey(e => e.IdCar).OnDelete(DeleteBehavior.ClientCascade);


            });
            modelBuilder.Entity<Car>(e =>
            {
                e.HasKey(e => e.IdCar);
                e.HasOne(e => e.Make).WithMany(e => e.Cars).HasForeignKey(e => e.IdMake).OnDelete(DeleteBehavior.ClientCascade);
                e.Property(e => e.RegistrationPlate).HasMaxLength(255).IsRequired();
                e.Property(e => e.ProductionYear).HasColumnType("date").IsRequired();

            });

            modelBuilder.Entity<Make>(e =>
            {
                e.HasKey(e => e.IdMake);
                e.Property(e => e.Name).HasMaxLength(255).IsRequired();
                
            });
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { IdSpecialization = 1, Name = "Electronics" },
                new Specialization { IdSpecialization = 2, Name = "Engine" },
                new Specialization { IdSpecialization = 3, Name = "Bodywork" }
            );

            modelBuilder.Entity<Make>().HasData(
                new Make { IdMake = 1, Name = "Toyota" },
                new Make { IdMake = 2, Name = "Honda" },
                new Make { IdMake = 3, Name = "BMW" }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car { IdCar = 1, IdMake = 1, RegistrationPlate = "ABC123", ProductionYear = new DateTime(2019, 1, 1) },
                new Car { IdCar = 2, IdMake = 2, RegistrationPlate = "XYZ789", ProductionYear = new DateTime(2020, 5, 15) },
                new Car { IdCar = 3, IdMake = 3, RegistrationPlate = "DEF456", ProductionYear = new DateTime(2018, 10, 10) }
            );

            modelBuilder.Entity<Mechanic>().HasData(
                new Mechanic { IdMechanic = 1, FirstName = "John", LastName = "Doe", Nickname = "JD", IdSpecialization = 1 },
                new Mechanic { IdMechanic = 2, FirstName = "Jane", LastName = "Smith", Nickname = "JS", IdSpecialization = 2 },
                new Mechanic { IdMechanic = 3, FirstName = "Robert", LastName = "Johnson", IdSpecialization = 3 }
            );

            modelBuilder.Entity<MechanicCar>().HasData(
                new MechanicCar { IdMechanic = 1, IdCar = 1 },
                new MechanicCar { IdMechanic = 1, IdCar = 2 },
                new MechanicCar { IdMechanic = 2, IdCar = 3 }
            );




        }

    }
}
