using MechanicKol.Models;
using MechanicKol.Models.DTOs;
using MechanicKol.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MechanicKol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IMechanicsService _service;
        public MechanicsController(MyDbContext context, IMechanicsService service)
        {
            _context = context;
            _service = service;
        }
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetMechanics(int id)
        {
            if (!await _service.DoesMechanicExist(id))
            {
                return NotFound("Mechanic with given id does not exist.");
            }
            return Ok(await _service.GetMechanicsCars(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostNewCar(CarPostDTO carPostDTO)
        {
            if (!await _service.DoesMechanicExist(carPostDTO.IdMechanic)) 
            {
                return NotFound("Mechanic with given id does not exist.");
            }

            if (await _service.DoesCarExist(carPostDTO.RegistrationPlate))
            {
                return Conflict("Car with given registration plate already exists.");
            }

            if (carPostDTO.ProductionYear > DateTime.Now)
            {
                return BadRequest("Cannot add a car with production date from the future.");
            }

            try
            {
                await _service.AddNewCar(carPostDTO);
                return Created("","");
            } catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
