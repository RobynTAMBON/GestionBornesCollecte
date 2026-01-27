using GestionBornesCollecte.Api.Data;
using GestionBornesCollecte.Api.Dtos;
using GestionBornesCollecte.Api.Models;
using GestionBornesCollecte.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("GetBennes")]
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
                nameof(GetById),
                new { id = benne.Id },
                benne
            );
        }


        // GET: api/Bennes/overview

        [HttpGet("overview")]
        public async Task<IActionResult> GetOverview([FromServices] IBenneService benneService)
        {
            var overview = await benneService.GetOverviewAsync();
            return Ok(overview);
        }


        // GET: api/Bennes/{id}

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BenneDetailDto>> GetById(int id)
        {
            var benne = await _context.Bennes
                .Where(b => b.Id == id)
                .Select(b => new BenneDetailDto
                {
                    Id = b.Id,
                    Nom = b.Nom,
                    Localisation = b.Localisation,
                    CapaciteMax = b.CapaciteMax,
                    DerniereMesure = b.Mesures
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => (DateTime?)m.Timestamp)
                        .FirstOrDefault(),
                    NiveauRemplissage = b.Mesures
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => (int?)m.NiveauRemplissage)
                        .FirstOrDefault(),
                    BatterieVolt = b.Mesures
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => (float?)m.BatterieVolt)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (benne == null)
                return NotFound();

            return Ok(benne);
        }


        // PUT: api/Bennes/{id}

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateBenneDto dto)
        {
            var benne = await _context.Bennes.FindAsync(id);
            if (benne == null)
                return NotFound();

            if (dto.Nom != null)
                benne.Nom = dto.Nom;

            if (dto.Localisation != null)
                benne.Localisation = dto.Localisation;

            if (dto.CapaciteMax.HasValue)
                benne.CapaciteMax = dto.CapaciteMax.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
