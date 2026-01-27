using GestionBornesCollecte.Api.Data;
using GestionBornesCollecte.Api.Dtos;
using GestionBornesCollecte.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionBornesCollecte.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BennesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BennesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/Bennes

        [HttpGet(Name = "GetBennes")]
        public IActionResult GetAll()
        {
            var bennes = _context.Bennes.ToList();
            return Ok(bennes);
        }


        // POST: api/Bennes

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBenneDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var benne = new Benne
            {
                Nom = dto.Nom,
                Localisation = dto.Localisation,
                CapaciteMax = dto.CapaciteMax
            };

            _context.Bennes.Add(benne);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAll),
                new { id = benne.Id },
                benne
            );
        }
    }
}
