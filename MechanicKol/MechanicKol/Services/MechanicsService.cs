using MechanicKol.Models;
using MechanicKol.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace MechanicKol.Services
{
    public interface IMechanicsService
    {
        public Task<bool> DoesMechanicExist(int id);
        public Task<MechanicsCarsDTO> GetMechanicsCars(int id);
        public Task AddNewCar(CarPostDTO car);
        public Task<bool> DoesCarExist(string RegistrationPlate);
    }
    public class MechanicsService : IMechanicsService
    {
        private readonly MyDbContext _context;
        public MechanicsService(MyDbContext context)
        {
            _context = context;
        }

        public async Task AddNewCar(CarPostDTO carPost)
        {
            var make = await _context.Makes.FirstOrDefaultAsync(e => e.Name == carPost.Make);
            
            
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if(make == null) 
                {
                    make = new Make { Name = carPost.Make};
                    _context.Makes.Add(make);
                    await _context.SaveChangesAsync();
                }
                var newCar = new Car
                {
                    RegistrationPlate = carPost.RegistrationPlate,
                    ProductionYear = carPost.ProductionYear,
                    IdMake = make.IdMake
                };
                _context.Cars.Add(newCar);
                await _context.SaveChangesAsync();

                _context.MechanicCars.Add(new MechanicCar
                {
                    IdCar = newCar.IdCar,
                    IdMechanic = carPost.IdMechanic,
                });

                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Transaction could not be completed: {ex.Message}.");
            }
        }

        public async Task<bool> DoesCarExist(string RegistrationPlate)
        {
            return await _context.Cars.AnyAsync(e => e.RegistrationPlate == RegistrationPlate);
        }

        public Task<bool> DoesMechanicExist(int id)
        {
            return _context.Mechanics.AnyAsync(e => e.IdMechanic == id);
        }

        public Task<MechanicsCarsDTO> GetMechanicsCars(int id)
        {
            return _context.Mechanics.Select(e => new MechanicsCarsDTO
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Nickname = e.Nickname,
                Specialization = e.Specialization.Name,
                Cars = e.MechanicCars.Select(e => new CarDTO
                {
                    IdCar = e.IdCar,
                    RegistrationPlate = e.Car.RegistrationPlate,
                    ProductionYear = e.Car.ProductionYear,
                    Make = e.Car.Make.Name
                }).ToList()
            }).FirstOrDefaultAsync();
        }
    }
}
