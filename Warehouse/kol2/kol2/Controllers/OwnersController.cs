using WarehousesAPI.Models;
using WarehousesAPI.Services;
using Microsoft.AspNetCore.Mvc;
namespace WarehousesAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IDbService _service;
        
        public OwnersController(IDbService dbService, MyDbContext context)
        {
            _context = context;
            _service = dbService;
        }

        [HttpGet("/{Id}")]
        public async Task<IActionResult> GetObjects(int Id)
        {
            
            if(!await _service.DoesOwnerExist(Id))
            {
                return NotFound("Client with given id does not exist.");
            }
            return Ok(await _service.GetOwnersObjects(Id));
            
        }
        [HttpGet("/{Id}/SQL")]
        public async Task<IActionResult> GetObjectsSQL(int Id)
        {

            if (!await _service.DoesOwnerExistSQL(Id))
            {
                return NotFound("Client with given id does not exist.");
            }
            return Ok(await _service.GetOwnersObjectsSQL(Id));

        }
        [HttpPost("/{IdOwner}/objects/{IdObject}")]
        public async Task<IActionResult> AssignObjectToOwner(int IdOwner, int IdObject)
        {
            if (!await _service.DoesOwnerExist(IdOwner))
            {
                return NotFound("Client with given id does not exist.");
            }

            if (!await _service.DoesObjectExist(IdObject))
            {
                return NotFound("Object with given id does not exist.");
            }

            if (await _service.DoesPairExist(IdOwner, IdObject))
            {
                return Conflict("Client already owns this object.");
            }

            try
            {
                await _service.AddObjectOwner(IdOwner, IdObject);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Ok();
        }
        [HttpPost("/{IdOwner}/objects/{IdObject}/SQL")]
        public async Task<IActionResult> AssignObjectToOwnerSQL(int IdOwner, int IdObject)
        {
            if (!await _service.DoesOwnerExistSQL(IdOwner))
            {
                return NotFound("Client with given id does not exist.");
            }

            if (!await _service.DoesObjectExistSQL(IdObject))
            {
                return NotFound("Object with given id does not exist.");
            }

            if (await _service.DoesPairExistSQL(IdOwner, IdObject))
            {
                return Conflict("Client already owns this object.");
            }
            try
            {
                await _service.AddObjectOwnerSQL(IdOwner, IdObject);
            } catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

    }
}
