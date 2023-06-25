using WarehousesAPI.Models;
using WarehousesAPI.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Object = WarehousesAPI.Models.Object;

namespace WarehousesAPI.Services
{
    public interface IDbService
    {
        public Task<OwnersObjectsDTO> GetOwnersObjects(int id);
        public Task<OwnersObjectsDTO> GetOwnersObjectsSQL(int id);
        public Task<bool> DoesOwnerExist(int id);
        public Task<bool> DoesOwnerExistSQL(int id);
        public Task<bool> DoesObjectExist(int id);
        public Task<bool> DoesObjectExistSQL(int id);
        public Task AddObjectOwner(int IdOwner, int IdObject);
        public Task AddObjectOwnerSQL(int IdOwner, int IdObject);
        public Task<bool> DoesPairExist(int IdOwner, int IdObject);
        public Task<bool> DoesPairExistSQL(int IdOwner, int IdObject);
    }
    public class DbService : IDbService
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        public DbService(MyDbContext context, IConfiguration configuration)
        {
            _context = context; _configuration = configuration;
        }

        public async Task<bool> DoesOwnerExist(int id)
        {
            return await _context.Owners.AnyAsync(o => o.IdOwner == id);
        }   

        public async Task<OwnersObjectsDTO> GetOwnersObjects(int id)
        {
            var objects = await _context.Owners.Where(o => o.IdOwner == id)
                .Select(o => new OwnersObjectsDTO
                {
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    PhoneNumber = o.PhoneNumber,
                    OwnerObjects = o.ObjectOwners.Select(oo => new ObjectDTO
                    {
                        IdObject = oo.IdObject,
                        Height = oo.Object.Height,
                        Width = oo.Object.Width,
                        Type = oo.Object.ObjectType.Name,
                        WarehouseName = oo.Object.Warehouse.Name
                    })
                }).FirstOrDefaultAsync();
            return objects;
        }

        public async Task<OwnersObjectsDTO> GetOwnersObjectsSQL(int id)
        {
            var connectionString = _configuration.GetConnectionString("Default");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT o.Firstname, o.Lastname, o.PhoneNumber, " +
                                              "obj.IdObject, obj.Width, obj.Height, ot.Name, " +
                                              "w.Name FROM Owners o " +
                                              "JOIN ObjectOwners objo ON o.IdOwner = objo.IdOwner " +
                                              "JOIN Objects obj ON obj.IdObject = objo.IdObject " +
                                              "JOIN ObjectTypes ot ON ot.IdObjectType = obj.IdObjectType " +
                                              "JOIN Warehouses w ON w.IdWarehouse = obj.IdWarehouse WHERE o.IdOwner = @ownerId";
                        command.Parameters.AddWithValue("ownerId", id);

                        SqlDataReader reader = await command.ExecuteReaderAsync();



                        var objects = new List<ObjectDTO>();
                        var ownerObjects = new OwnersObjectsDTO();
                        var isFirstIteration = true;
                        while (await reader.ReadAsync())
                        {
                            if (isFirstIteration)
                            {
                                ownerObjects = new OwnersObjectsDTO()
                                {
                                    FirstName = reader.GetString(0),
                                    LastName = reader.GetString(1),
                                    PhoneNumber = reader.GetString(2),
                                    OwnerObjects = objects
                                };
                            }


                            objects.Add(new ObjectDTO
                            {
                                IdObject = reader.GetInt32(3),
                                Width = reader.GetDouble(4),
                                Height = reader.GetDouble(5),
                                Type = reader.GetString(6),
                                WarehouseName = reader.GetString(7)
                            });

                            isFirstIteration = false;
                        }
                        await reader.CloseAsync();
                        await connection.CloseAsync();

                        return ownerObjects;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
        public async Task<bool> DoesOwnerExistSQL(int id)
        {
            var connectionString = _configuration.GetConnectionString("Default");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT 1 FROM Owners WHERE IdOwner = @ownerId";
                        command.Parameters.AddWithValue("ownerId", id);

                        var record = await command.ExecuteScalarAsync();
                   
                        return record != null;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }

        public async Task<bool> DoesObjectExist(int id)
        {
            return await _context.Objects.AnyAsync(e => e.IdObject == id);
        }

        public async Task<bool> DoesObjectExistSQL(int id)
        {
            var connectionString = _configuration.GetConnectionString("Default");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT 1 FROM Objects WHERE IdObject = @id";
                        command.Parameters.AddWithValue("id", id);

                        var record = await command.ExecuteScalarAsync();

                        return record != null;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }

        public async Task AddObjectOwner(int IdOwner, int IdObject)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.ObjectOwners.Add(new ObjectOwner
                {
                    IdObject = IdObject,
                    IdOwner = IdOwner
                });
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception e) 
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }

        public async Task AddObjectOwnerSQL(int idOwner, int idObject)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var connectionString = _configuration.GetConnectionString("Default");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    try
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "INSERT INTO ObjectOwners (IdOwner, IdObject) values (@IdOwner,  @IdObject) ";
                            command.Parameters.AddWithValue("IdOwner", idOwner);
                            command.Parameters.AddWithValue("IdObject", idObject);

                            var record = await command.ExecuteScalarAsync();

                            
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DoesPairExist(int IdOwner, int IdObject)
        {
            return await _context.ObjectOwners.AnyAsync(e => e.IdOwner == IdOwner && e.IdObject == IdObject);
        }

        public async Task<bool> DoesPairExistSQL(int IdOwner, int IdObject)
        {
            var connectionString = _configuration.GetConnectionString("Default");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT 1 FROM ObjectOwners WHERE IdObject = @IdObject AND IdOwner = @IdOwner";
                        command.Parameters.AddWithValue("IdOwner", IdOwner);
                        command.Parameters.AddWithValue("IdObject", IdObject);
                        var record = await command.ExecuteScalarAsync();

                        return record != null;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
    }

}
